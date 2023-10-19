using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Util {

    //===============================================
    //MATH
    //===============================================

    public static float Fract(float v) {
        return v - Mathf.Floor(v);
    }

    //===============================================
    //VECTOR3
    //===============================================

    public static Vector3 SetX(this Vector3 self, float x) {
        self.x = x;
        return self;
    }

    public static Vector3 SetY(this Vector3 self, float y) {
        self.y = y;
        return self;
    }

    public static Vector3 SetZ(this Vector3 self, float z) {
        self.z = z;
        return self;
    }

    public static Vector3 AddX(this Vector3 self, float x) {
        self.x += x;
        return self;
    }

    public static Vector3 AddY(this Vector3 self, float y) {
        self.y += y;
        return self;
    }

    public static Vector3 AddZ(this Vector3 self, float z) {
        self.z += z;
        return self;
    }

    //===============================================
    //COLOR
    //===============================================

    public static Color SetR(this Color self, float r) {
        self.r = r;
        return self;
    }

    public static Color SetG(this Color self, float g) {
        self.g = g;
        return self;
    }

    public static Color SetB(this Color self, float b) {
        self.b = b;
        return self;
    }

    public static Color SetA(this Color self, float a) {
        self.a = a;
        return self;
    }

    //===============================================
    //COROUTINE
    //===============================================

    private class CoroutineComponent : MonoBehaviour { }

    private static CoroutineComponent coroutineObject;

    private static CoroutineComponent CoroutineObject {
        get {
            if (coroutineObject == null) {
                var go = new GameObject("Coroutine");
                go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.HideAndDontSave;
                Object.DontDestroyOnLoad(go);
                coroutineObject = go.AddComponent<CoroutineComponent>();
            }
            return coroutineObject;
        }
    }

    public static void StopAllCoroutines() {
        CoroutineObject.StopAllCoroutines();
    }

    public static void StopCoroutines(Coroutine coroutine) {
        CoroutineObject.StopCoroutine(coroutine);
    }

    public static Coroutine StartCoroutine(IEnumerator routine) {
        return CoroutineObject.StartCoroutine(routine);
    }

    public static Coroutine WaitForSeconds(float seconds, UnityAction onEnd) {
        IEnumerator coroutine() {
            yield return new WaitForSeconds(seconds);
            onEnd();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Lerp(float start, float target, float duration, UnityAction<float> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            var elapsed = 0f;
            while (elapsed < duration) {
                onUpdate.Invoke(Mathf.Lerp(start, target, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
            onUpdate.Invoke(target);
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Lerp(this Vector2 start, Vector2 target, float duration, UnityAction<Vector2> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            var elapsed = 0f;
            while (elapsed < duration) {
                onUpdate.Invoke(Vector2.Lerp(start, target, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
            onUpdate.Invoke(target);
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Lerp(this Vector3 start, Vector3 target, float duration, UnityAction<Vector3> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            var elapsed = 0f;
            while (elapsed < duration) {
                onUpdate.Invoke(Vector3.Lerp(start, target, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
            onUpdate.Invoke(target);
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Lerp(this Quaternion start, Quaternion target, float duration, UnityAction<Quaternion> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            var elapsed = 0f;
            while (elapsed < duration) {
                onUpdate.Invoke(Quaternion.Lerp(start, target, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
            onUpdate.Invoke(target);
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Lerp(this Color start, Color target, float duration, UnityAction<Color> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            var elapsed = 0f;
            while (elapsed < duration) {
                onUpdate.Invoke(Color.Lerp(start, target, elapsed / duration));
                elapsed += Time.deltaTime;
                yield return null;
            }
            onUpdate.Invoke(target);
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine MoveTowards(this float start, float target, float speed, UnityAction<float> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            while (start != target) {
                start = Mathf.MoveTowards(start, target, speed * Time.deltaTime);
                onUpdate.Invoke(start);
                yield return null;
            }
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine MoveTowards(this Vector3 start, Vector3 target, float speed, UnityAction<Vector3> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            while (start != target) {
                start = Vector3.MoveTowards(start, target, speed * Time.deltaTime);
                onUpdate.Invoke(start);
                yield return null;
            }
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine RotateTowards(this Quaternion start, Quaternion target, float speed, UnityAction<Quaternion> onUpdate, UnityAction onEnd = null) {
        IEnumerator coroutine() {
            while (start != target) {
                start = Quaternion.RotateTowards(start, target, speed * Time.deltaTime);
                onUpdate.Invoke(start);
                yield return null;
            }
            onEnd?.Invoke();
        }
        return StartCoroutine(coroutine());
    }

    //===============================================
    //MATH
    //===============================================

    public static int Remap(this int value, int fromLow, int fromHigh, int toLow, int toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    public static float Remap(this float value, float fromLow, float fromHigh, float toLow, float toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    public static double Remap(this double value, double fromLow, double fromHigh, double toLow, double toHigh) {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }

    public static float Remap(this float value, float toLow, float toHigh) {
        return value * (toHigh - toLow) + toLow;
    }

    //===============================================
    //RANDOM
    //===============================================

    public static T RandomChoice<T>(this T[] self) {
        return self.Length == 0 ? default(T) : self[Random.Range(0, self.Length)];
    }

    public static T RandomChoice<T>(this List<T> self) {
        return self.Count == 0 ? default(T) : self[Random.Range(0, self.Count)];
    }

    public static float Noise(float x) {
        return Fract(Mathf.Sin(x * 91.3458f) * 47453.5453f);
    }

    public static float Noise(float x, float y) {
        return Fract(Mathf.Sin(Vector2.Dot(new Vector2(x, y), new Vector2(12.9898f, 78.233f))) * 43758.5453f);
    }

    public static float Noise(float x, float y, float z) {
        var r = Noise(z);
        return Noise(x + r, y + r);
    }

    public static float Noise(Vector2 pos) {
        return Noise(pos.x, pos.y);
    }

    public static float Noise(Vector3 pos) {
        return Noise(pos.x, pos.y, pos.z);
    }

    //===============================================
    //CAMERA
    //===============================================

    public static GameObject MousePick(this Camera self) {
        if (Physics.Raycast(self.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            return hit.collider.gameObject;
        }
        return null;
    }

    public static Coroutine LerpAspect(this Camera self, float to, float duration) {
        return Lerp(self.aspect, to, duration, t => self.aspect = t);
    }

    public static Coroutine LerpBackgroundColor(this Camera self, Color to, float duration) {
        return Lerp(self.backgroundColor, to, duration, t => self.backgroundColor = t);
    }

    public static Coroutine LerpFarClipPlane(this Camera self, float to, float duration) {
        return Lerp(self.farClipPlane, to, duration, t => self.farClipPlane = t);
    }

    public static Coroutine LerpNearClipPlane(this Camera self, float to, float duration) {
        return Lerp(self.nearClipPlane, to, duration, t => self.nearClipPlane = t);
    }

    public static Coroutine LerpFieldOfView(this Camera self, float to, float duration) {
        return Lerp(self.fieldOfView, to, duration, t => self.fieldOfView = t);
    }

    public static Coroutine LerpOrthographicSize(this Camera self, float to, float duration) {
        return Lerp(self.orthographicSize, to, duration, t => self.orthographicSize = t);
    }

    //===============================================
    //COMPONENT
    //===============================================

    public static void Destroy(this Component self) {
        Object.Destroy(self);
    }

    public static Coroutine Destroy(this Component self, float seconds) {
        return WaitForSeconds(seconds, () => Object.Destroy(self));
    }

    public static void Toggle(this Behaviour self) {
        self.enabled = !self.enabled;
    }

    public static Coroutine Toggle(this Behaviour self, float seconds) {
        return WaitForSeconds(seconds, () => self.Toggle());
    }

    public static bool HasComponent<T>(this Component self) where T : Component {
        return self.GetComponent<T>() != null;
    }

    public static void RemoveComponent<T>(this Component self) where T : Component {
        var component = self.GetComponent<T>();
        if (component != null) Object.Destroy(component);
    }

    //===============================================
    //GAMEOBJECT
    //===============================================

    public static GameObject Instantiate(this GameObject self, Vector3 pos) {
        return Object.Instantiate(self, pos, self.transform.rotation);
    }

    public static GameObject Instantiate(this GameObject self, Vector3 pos, Quaternion rot) {
        return Object.Instantiate(self, pos, rot);
    }

    public static void Destroy(this GameObject self) {
        Object.Destroy(self);
    }

    public static void Toggle(this GameObject self) {
        self.SetActive(!self.activeSelf);
    }

    public static bool IsVisible(this GameObject self) {
        var renderer = self.GetComponent<Renderer>();
        return renderer != null && renderer.isVisible;
    }

    public static GameObject GetRoot(this GameObject self) {
        return self.transform.root.gameObject;
    }

    public static bool HasComponent<T>(this GameObject self) where T : Component {
        return self.GetComponent<T>() != null;
    }

    public static void RemoveComponent<T>(this GameObject self) where T : Component {
        var component = self.GetComponent<T>();
        if (component != null) Object.Destroy(component);
    }

    public static Coroutine Blink(this GameObject self, float waitTime) {
        var renderers = self.GetComponentsInChildren<Renderer>();
        var wait = new WaitForSeconds(waitTime);
        IEnumerator coroutine() {

            foreach (var i in renderers) i.gameObject.Toggle();
            yield return wait;

            foreach (var i in renderers) i.gameObject.Toggle();
            yield return wait;

        }
        return StartCoroutine(coroutine());
    }

    public static Coroutine Flicker(this GameObject self, float waitTime, int amount) {
        var renderers = self.GetComponentsInChildren<Renderer>();
        var wait = new WaitForSeconds(waitTime);
        IEnumerator coroutine() {
            for (var j = 0; j < amount; j++) {

                foreach (var i in renderers) i.gameObject.Toggle();
                yield return wait;

                foreach (var i in renderers) i.gameObject.Toggle();
                yield return wait;

            }
        }
        return StartCoroutine(coroutine());
    }

    public static T FindClosestObjectOfType<T>(Vector3 pos) where T : Component {
        var min = float.PositiveInfinity;
        var ret = default(T);
        foreach (var i in Object.FindObjectsOfType<T>()) {
            var dist = i.transform.GetDistance(pos);
            if (dist <= min) {
                min = dist;
                ret = i;
            }
        }
        return ret;
    }

    //===============================================
    //LIGHT
    //===============================================

    public static Coroutine LerpColor(this Light self, Color to, float duration) {
        return Lerp(self.color, to, duration, t => self.color = t);
    }

    public static Coroutine LerpIntensity(this Light self, float to, float duration) {
        return Lerp(self.intensity, to, duration, t => self.intensity = t);
    }

    //===============================================
    //MATERIAL
    //===============================================

    public static Coroutine LerpColor(this Material self, Color to, string property, float duration) {
        return Lerp(self.GetColor(property), to, duration, t => self.SetColor(property, t));
    }

    public static Coroutine LerpFloat(this Material self, float to, string property, float duration) {
        return Lerp(self.GetFloat(property), to, duration, t => self.SetFloat(property, t));
    }

    public static Coroutine LerpVector3(this Material self, Vector3 to, string property, float duration) {
        return Lerp((Vector3)self.GetVector(property), to, duration, t => self.SetVector(property, t));
    }

    public static Coroutine LerpOffset(this Material self, Vector2 to, string property, float duration) {
        return Lerp(self.GetTextureOffset(property), to, duration, t => self.SetTextureOffset(property, t));
    }

    public static Coroutine LerpTiling(this Material self, Vector2 to, string property, float duration) {
        return Lerp(self.GetTextureScale(property), to, duration, t => self.SetTextureScale(property, t));
    }

    //===============================================
    //RIGIDBODY
    //===============================================

    public static void AddRandomForce(this Rigidbody self, float force) {
        self.AddForce(Random.insideUnitSphere * force, ForceMode.Impulse);
    }

    public static void AddJumpForce(this Rigidbody2D self, float targetJumpHeight) {
        self.velocity += new Vector2(
            0f,
            Mathf.Sqrt(2f * targetJumpHeight * Physics2D.gravity.y)
        );
    }

    public static void AddJumpForce(this Rigidbody self, float targetJumpHeight) {
        self.velocity += new Vector3(
            0f,
            Mathf.Sqrt(2f * targetJumpHeight * Physics.gravity.y),
            0f
        );
    }

    //===============================================
    //AUDIO
    //===============================================

    private static AudioSource[] SfxSources;
    private static int SfxPointer = 0;

    public static void Play(this AudioClip clip, Vector3 pos = default(Vector3)) {

        if (SfxSources == null) {
            SfxPointer = 0;
            SfxSources = new AudioSource[32];
            for (var i = 0; i < SfxSources.Length; i++) {

                var go = new GameObject();
                go.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.HideAndDontSave;
                Object.DontDestroyOnLoad(go);

                var sfxSource = go.AddComponent<AudioSource>();
                sfxSource.dopplerLevel = 0f;
                sfxSource.spread = 0f;
                sfxSource.minDistance = 1f;
                sfxSource.maxDistance = 16f;
                sfxSource.rolloffMode = AudioRolloffMode.Linear;
                SfxSources[i] = sfxSource;

            }
        }

        if (clip) {

            var sfxSource = SfxSources[SfxPointer];
            if (sfxSource.isPlaying) {
                return;
            }

            if (++SfxPointer >= SfxSources.Length) {
                SfxPointer = 0;
            }

            if (pos == default(Vector3)) {
                sfxSource.spatialBlend = 0f;
            }
            else {
                sfxSource.spatialBlend = 1f;
                sfxSource.transform.position = pos;
            }

            sfxSource.pitch = 1f + Random.Range(-0.1f, +0.1f);
            sfxSource.clip = clip;
            sfxSource.Play();

        }
    }

    //===============================================
    //TRANSFORM
    //===============================================

    public static void LookAtYAxis(this Transform self, Vector3 pos) {
        var dir = pos - self.position;
        dir.y = 0f;
        self.rotation = Quaternion.LookRotation(dir);
    }

    public static void LookAtYAxis(this Transform self, Component other) {
        LookAtYAxis(self, other.transform.position);
    }

    public static void LookAtYAxis(this Transform self, GameObject other) {
        LookAtYAxis(self, other.transform.position);
    }

    public static void AddPos(this Transform self, Vector3 pos) {
        self.position += pos;
    }

    public static void AddPos(this Transform self, float x, float y, float z) {
        self.position += new Vector3(x, y, z);
    }

    public static void AddPosX(this Transform self, float x) {
        self.position = self.position.AddX(x);
    }

    public static void AddPosY(this Transform self, float y) {
        self.position = self.position.AddY(y);
    }

    public static void AddPosZ(this Transform self, float z) {
        self.position = self.position.AddZ(z);
    }

    public static void SetPos(this Transform self, Vector3 position) {
        self.position = position;
    }

    public static void SetPos(this Transform self, float x, float y, float z) {
        self.position.Set(x, y, z);
    }

    public static void SetPosX(this Transform self, float x) {
        self.position = self.position.SetX(x);
    }

    public static void SetPosY(this Transform self, float y) {
        self.position = self.position.SetY(y);
    }

    public static void SetPosZ(this Transform self, float z) {
        self.position = self.position.SetZ(z);
    }

    public static Coroutine LerpPos(this Transform self, Vector3 position, float duration) {
        return Lerp(self.position, position, duration, i => self.position = i);
    }

    public static Coroutine LerpPosX(this Transform self, float position, float duration) {
        return Lerp(self.position.x, position, duration, self.SetPosX);
    }

    public static Coroutine LerpPosY(this Transform self, float position, float duration) {
        return Lerp(self.position.y, position, duration, self.SetPosY);
    }

    public static Coroutine LerpPosZ(this Transform self, float position, float duration) {
        return Lerp(self.position.z, position, duration, self.SetPosZ);
    }

    public static Coroutine LerpRot(this Transform self, Vector3 eulerAngles, float duration) {
        return Lerp(self.eulerAngles, eulerAngles, duration, i => self.eulerAngles = i);
    }

    public static Coroutine LerpRotX(this Transform self, float angle, float duration) {
        return Lerp(self.eulerAngles, self.eulerAngles.SetX(angle), duration, i => self.eulerAngles = i);
    }

    public static Coroutine LerpRotY(this Transform self, float angle, float duration) {
        return Lerp(self.eulerAngles, self.eulerAngles.SetY(angle), duration, i => self.eulerAngles = i);
    }

    public static Coroutine LerpRotZ(this Transform self, float angle, float duration) {
        return Lerp(self.eulerAngles, self.eulerAngles.SetZ(angle), duration, i => self.eulerAngles = i);
    }

    public static Coroutine MoveTowards(this Transform self, Vector3 position, float speed) {
        return MoveTowards(self.position, position, speed, i => self.position = i);
    }

    public static Coroutine MoveTowardsX(this Transform self, float position, float speed) {
        return MoveTowards(self.position.x, position, speed, self.SetPosX);
    }

    public static Coroutine MoveTowardsY(this Transform self, float position, float speed) {
        return MoveTowards(self.position.y, position, speed, self.SetPosY);
    }

    public static Coroutine MoveTowardsZ(this Transform self, float position, float speed) {
        return MoveTowards(self.position.z, position, speed, self.SetPosZ);
    }

    public static Coroutine RotateTowards(this Transform self, Quaternion rotation, float speed) {
        return RotateTowards(self.rotation, rotation, speed, i => self.rotation = i);
    }

    public static Coroutine RotateTowards(this Transform self, Vector3 euler, float speed) {
        return RotateTowards(self.rotation, Quaternion.Euler(euler), speed, i => self.rotation = i);
    }

    public static float GetDistance(this Transform self, Vector3 position) {
        return Vector3.Distance(self.position, position);
    }

    public static float GetDistance(this Transform self, float x, float y, float z) {
        return Vector3.Distance(self.position, new Vector3(x, y, z));
    }

    public static float GetDistance(this Transform self, Transform to) {
        return Vector3.Distance(self.position, to.position);
    }

    public static Coroutine Shake(this Transform self, float duration = 0.05f, float power = 0.05f) {
        IEnumerator coroutine() {
            var pos = self.position;
            while (duration > 0f) {
                duration -= Time.unscaledDeltaTime;
                self.position = pos + Random.insideUnitSphere * power;
                yield return null;
            }
        }
        return StartCoroutine(coroutine());
    }

}