using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    //asignar numero de objetos a crear en el pool para cada prefab
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size = 10;
    }

    public List<Pool> pools;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (Pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.size; i++)
            {
                GameObject obj = Instantiate(p.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(p.prefab, objectPool);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            return null;
        }

        GameObject obj = poolDictionary[prefab].Dequeue();
        obj.SetActive(true);
        poolDictionary[prefab].Enqueue(obj);
        return obj;
    }
}