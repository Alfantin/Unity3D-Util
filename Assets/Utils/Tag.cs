using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tag : IEnumerable {

    private string Name;
    private object Value;

    private delegate object ReadDelegate(BinaryReader reader);
    private static ReadDelegate[] readers = {
        (reader) => null,
        (reader) => reader.ReadBoolean(),
        (reader) => reader.ReadByte(),
        (reader) => reader.ReadInt32(),
        (reader) => reader.ReadSingle(),
        (reader) => reader.ReadString(),
        (reader) => new Vector2(reader.ReadSingle(), reader.ReadSingle()),
        (reader) => new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
        (reader) => new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
        (reader) => new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
        (reader) => new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
        (reader) => {
            var elementId = reader.ReadByte();
            var elementType = types[elementId];
            var elementReader = readers[elementId];
            var length = reader.ReadInt32();
            var array = Array.CreateInstance(elementType, length);
            for (var i = 0; i < length; i++){
                array.SetValue(elementReader(reader), i);
            }
            return array;
        },
        (reader) => {
            var dic = new Dictionary<string, object>();
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++) {
                var key = reader.ReadString();
                var id = reader.ReadByte();
                var valueReader = readers[id];
                dic[key] = valueReader(reader);
            }
            return new Tag(){
                Value = dic
            };
        },
    };

    private delegate void WriteDelegate(BinaryWriter writer, object value);
    private static WriteDelegate[] writers = {
        (writer, value) => { },
        (writer, value) => writer.Write((bool)value),
        (writer, value) => writer.Write((byte)value),
        (writer, value) => writer.Write((int)value),
        (writer, value) => writer.Write((float)value),
        (writer, value) => writer.Write((string)value),
        (writer, value) => {
            var val = (Vector2)value;
            writer.Write(val.x);
            writer.Write(val.y);
        },
        (writer, value) => {
            var val = (Vector3)value;
            writer.Write(val.x);
            writer.Write(val.y);
            writer.Write(val.z);
        },
        (writer, value) => {
            var val = (Vector4)value;
            writer.Write(val.x);
            writer.Write(val.y);
            writer.Write(val.z);
            writer.Write(val.w);
        },
        (writer, value) => {
            var val = (Quaternion)value;
            writer.Write(val.x);
            writer.Write(val.y);
            writer.Write(val.z);
            writer.Write(val.w);
        },
        (writer, value) => {
            var val = (Color)value;
            writer.Write(val.r);
            writer.Write(val.g);
            writer.Write(val.b);
            writer.Write(val.a);
        },
        (writer, value) => {
            var elementType = value.GetType().GetElementType();
            var elementId = ids[elementType];
            var elementWriter = writers[elementId];
            writer.Write(elementId);
            var array = (Array)value;
            var count = array.Length;
            writer.Write(count);
            for (var i = 0; i < count; i++) {
                elementWriter(writer, array.GetValue(0));
            }
        },
        (writer, value) => {
            var node = (Tag)value;
            var dic = (Dictionary<string, object>)node.Value;
            writer.Write(dic.Count);
            foreach (var i in dic) {
                writer.Write(i.Key);
                var id = getId(i.Value);
                writer.Write(id);
                writers[id](writer, i.Value);
            }
        },

    };

    private static Dictionary<Type, byte> ids = new Dictionary<Type, byte>() {
        {typeof(bool), 1},
        {typeof(byte), 2},
        {typeof(int), 3},
        {typeof(float), 4},
        {typeof(string), 5},
        {typeof(Vector2), 6},
        {typeof(Vector3), 7},
        {typeof(Vector4), 8},
        {typeof(Quaternion), 9},
        {typeof(Color), 10},
        {typeof(Array), 11},
        {typeof(Tag), 12}
    };

    private static Type[] types = {
        null,
        typeof(bool),
        typeof(byte),
        typeof(int),
        typeof(float),
        typeof(string),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Vector4),
        typeof(Quaternion),
        typeof(Color),
        typeof(Array),
        typeof(Tag),
    };

    //======================================
    //CONSTRUCT
    //======================================

    public Tag() {
    }

    public Tag(params Tag[] childs) {
        if (childs.Length > 0) {
            var dic = new Dictionary<string, object>();
            foreach (var i in childs) {
                if (i.Name == null) {
                    throw new ArgumentException("Children need name");
                }
                dic[i.Name] = i.Value;
            }
            Value = dic;
        }
    }

    public Tag(string name, bool value) { Name = name; Value = (object)value; }
    public Tag(string name, byte value) { Name = name; Value = (object)value; }
    public Tag(string name, int value) { Name = name; Value = (object)value; }
    public Tag(string name, float value) { Name = name; Value = (object)value; }
    public Tag(string name, string value) { Name = name; Value = (object)value; }
    public Tag(string name, Vector2 value) { Name = name; Value = (object)value; }
    public Tag(string name, Vector3 value) { Name = name; Value = (object)value; }
    public Tag(string name, Vector4 value) { Name = name; Value = (object)value; }
    public Tag(string name, Quaternion value) { Name = name; Value = (object)value; }
    public Tag(string name, Color value) { Name = name; Value = (object)value; }

    public Tag(string name, params bool[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params byte[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params int[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params float[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params string[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Vector2[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Vector3[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Vector4[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Quaternion[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Color[] value) { Name = name; Value = (object)value; }
    public Tag(string name, params Tag[] childs) { Name = name; Value = detectNodeOrArray(childs); }

    //======================================
    //GET DICTIONARY VALUE
    //======================================

    public object Get(string name) {
        if (Value is Dictionary<string, object>) {
            var dic = (Dictionary<string, object>)Value;
            dic.TryGetValue(name, out object val);
            return val;
        }
        return null;
    }

    public bool GetBool(string name) => (bool)Get(name);
    public byte GetByte(string name) => (byte)Get(name);
    public int GetInt(string name) => (int)Get(name);
    public float GetFloat(string name) => (float)Get(name);
    public string GetString(string name) => (string)Get(name);
    public Vector2 GetVector2(string name) => (Vector2)Get(name);
    public Vector3 GetVector3(string name) => (Vector3)Get(name);
    public Quaternion GetQuaternion(string name) => (Quaternion)Get(name);
    public Color GetColor(string name) => (Color)Get(name);
    public Tag GetTag(string name) => (Tag)Get(name);

    public bool[] GetBoolArray(string name) => (bool[])Get(name);
    public byte[] GetByteArray(string name) => (byte[])Get(name);
    public int[] GetIntArray(string name) => (int[])Get(name);
    public float[] GetFloatArray(string name) => (float[])Get(name);
    public string[] GetStringArray(string name) => (string[])Get(name);
    public Vector2[] GetVector2Array(string name) => (Vector2[])Get(name);
    public Vector3[] GetVector3Array(string name) => (Vector3[])Get(name);
    public Quaternion[] GetQuaternionArray(string name) => (Quaternion[])Get(name);
    public Color[] GetColorArray(string name) => (Color[])Get(name);
    public Tag[] GetTagArray(string name) => (Tag[])Get(name);

    //======================================
    //SET DICTIONARY VALUE
    //======================================

    private void Set(string name, object value) {
        var dic = (Dictionary<string, object>)Value;
        if (dic == null) {
            dic = new Dictionary<string, object>();
            Value = dic;
        }
        dic[name] = value;
    }

    public void Set(string name, bool value) => Set(name, (object)value);
    public void Set(string name, byte value) => Set(name, (object)value);
    public void Set(string name, int value) => Set(name, (object)value);
    public void Set(string name, float value) => Set(name, (object)value);
    public void Set(string name, string value) => Set(name, (object)value);
    public void Set(string name, Vector2 value) => Set(name, (object)value);
    public void Set(string name, Vector3 value) => Set(name, (object)value);
    public void Set(string name, Vector4 value) => Set(name, (object)value);
    public void Set(string name, Quaternion value) => Set(name, (object)value);
    public void Set(string name, Color value) => Set(name, (object)value);

    public void Set(string name, params bool[] value) => Set(name, (object)value);
    public void Set(string name, params byte[] value) => Set(name, (object)value);
    public void Set(string name, params int[] value) => Set(name, (object)value);
    public void Set(string name, params float[] value) => Set(name, (object)value);
    public void Set(string name, params string[] value) => Set(name, (object)value);
    public void Set(string name, params Vector2[] value) => Set(name, (object)value);
    public void Set(string name, params Vector3[] value) => Set(name, (object)value);
    public void Set(string name, params Vector4[] value) => Set(name, (object)value);
    public void Set(string name, params Quaternion[] value) => Set(name, (object)value);
    public void Set(string name, params Color[] value) => Set(name, (object)value);
    public void Set(string name, params Tag[] childs) => Set(name, detectNodeOrArray(childs));


    //======================================
    //IO
    //======================================

    public void Save(BinaryWriter writer) {
        var id = getId(this);
        writer.Write(id);
        writers[id](writer, this);
    }

    public void Save(Stream stream) {
        using (var writer = new BinaryWriter(stream)) {
            Save(writer);
        }
    }

    public void Save(string filePath) {
        using (var stream = File.OpenWrite(filePath)) {
            Save(stream);
        }
    }

    public static Tag Read(BinaryReader reader) {
        var id = reader.ReadByte();
        var valueReader = readers[id];
        return (Tag)valueReader(reader);
    }

    public static Tag Read(Stream stream) {
        using (var reader = new BinaryReader(stream)) {
            return Read(reader);
        }
    }

    public static Tag Read(string filePath) {
        using (var stream = File.OpenRead(filePath)) {
            return Read(stream);
        }
    }

    //======================================
    //UTIL
    //======================================

    public bool Remove(string name) {
        if (Value is Dictionary<string, object>) {
            var dic = (Dictionary<string, object>)Value;
            return dic.Remove(name);
        }
        return false;
    }

    public int Count() {
        if (Value is Dictionary<string, object>) {
            var dic = (Dictionary<string, object>)Value;
            return dic.Count;
        }
        return 0;
    }

    private static object detectNodeOrArray(Tag[] childs) {
        if (childs.Length > 0) {
            var isArray = childs[0].Name == null;
            foreach (var i in childs) {
                if (isArray != (i.Name == null)) {
                    throw new ArgumentException("Confusing children types");
                }
            }
            if (isArray) {
                return childs;
            }
            else {
                return new Tag(childs);
            }
        }
        return null;
    }

    private static byte getId(object value) {
        if (value != null) {
            if (value is Array) {
                return 11;
            }
            else {
                return ids[value.GetType()];
            }
        }
        return 0;
    }

    public IEnumerator GetEnumerator() {
        if (Value is Dictionary<string, object>) {
            var dic = (Dictionary<string, object>)Value;
            foreach (var i in dic) {
                yield return i;
            }
        }
        else if (Value is Array) {
            foreach (var i in (Array)Value) {
                yield return i;
            }
        }
    }

}

/*
 * 
Tag is simple and flexible binary data format inspired from Minecraft's NBT format.
I created it for my personal unity projects to save game, level, settings etc and network pockets.
Take care i did not test it enough!

SUPPORTED DATA TYPES

    bool, byte, int, float, string, Vector2, Vector3, Vector4, Quaternion, Color,
    bool[], byte[], int[], float[], string[], Vector2[], Vector3[], Vector4[], Quaternion[], Color[],


CONSTRUCTOR

    var data = new Tag(
        new Tag("map", "lake"),
        new Tag("settings",
            new Tag("speed", 10),
            new Tag("time", 20),
            new Tag("private", true)
        ),
        new Tag("players",
            new Tag(
                new Tag("name", "alfantin"),
                new Tag("team", 0)
            ),
            new Tag(
                new Tag("name", "mike"),
                new Tag("team", 1)
            )
        ),
        new Tag("teams",
            Color.red,
            Color.blue
        )
    );

TO SAVE & LOAD

    data.Save("test.Tag");
    data = Tag.Read("test.Tag");
    
TO SET

    data.Set("name", value);
    
TO GET

    data.GetBool("name");
    data.GetByte("name");
    data.GetInt("name");
    data.GetFloat("name");
    data.GetString("name");
    data.GetVector2("name");
    data.GetVector3("name");
    data.GetQuaternion("name");
    data.GetColor("name");
    data.GetTag("name");
    data.GetBoolArray("name");
    data.GetByteArray("name");
    data.GetIntArray("name");
    data.GetFloatArray("name");
    data.GetStringArray("name");
    data.GetVector2Array("name");
    data.GetVector3Array("name");
    data.GetQuaternionArray("name");
    data.GetColorArray("name");
    data.GetTagArray("name");
    
    //EXAMPLE
    data.GetTagArray("players")[0].GetString("name");

*/