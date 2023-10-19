using UnityEngine;
using System.Collections.Generic;

public class ObjectPool {

    private int Capacity = 100;
    private Dictionary<string, Stack<GameObject>> Pools = new Dictionary<string, Stack<GameObject>>();

    public ObjectPool(int capacity = 100) {
        Capacity = capacity;
    }

    public GameObject Instance(GameObject prefab) {
        return Instance(prefab, Vector3.zero, Quaternion.identity);
    }

    public GameObject Instance(GameObject prefab, Vector3 position) {
        return Instance(prefab, position, Quaternion.identity);
    }

    public GameObject Instance(GameObject prefab, Vector3 position, Quaternion rotation) {

        //request pool
        var pool = RequestPool(prefab.name);

        //pop object from pool
        GameObject instance = null;
        if (pool.Count > 0) {
            instance = pool.Pop();
        }

        //new instance
        else {
            instance = GameObject.Instantiate(prefab);
        }

        //return
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.name = prefab.name;
        instance.SetActive(true);

        return instance;
    }

    public void Destroy(GameObject instance) {

        //request pool
        var pool = RequestPool(instance.name);

        //reserve
        if (pool.Count < Capacity) {
            instance.SetActive(false);
            pool.Push(instance);
        }

        //destroy
        else {
            GameObject.Destroy(instance);
        }

    }

    public void Clear() {

        //destroy objects
        foreach (var i in Pools) {
            var pool = i.Value;
            while (pool.Count > 0) {
                GameObject.Destroy(pool.Pop());
            }
        }

        //clear pools
        Pools.Clear();

    }

    //get pool per object name
    private Stack<GameObject> RequestPool(string name) {
        Stack<GameObject> pool;
        if (!Pools.TryGetValue(name, out pool)) {
            pool = new Stack<GameObject>(Capacity);
            Pools.Add(name, pool);
        }
        return pool;
    }

}
