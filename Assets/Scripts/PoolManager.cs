using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoBehaviour
{
    // Serializable struct, her bir havuzda depolanan nesnelerin bilgilerini tutar.
    [Serializable]
    public struct Pool
    {
        public string ObjectName; // Nesne ad�
        public Queue<GameObject> PooledObjects; // Havuzda depolanan nesnelerin listesi
        public GameObject objectPrefabs; // Nesnenin prefab�
        public GameObject ParentObject; // Nesnenin atanaca�� parent nesnesi
        public int poolsize; // Havuzun boyutu
    }

    [SerializeField] public Pool[] pools = null; // Havuzlar�n bir listesi

    private void Start()
    {
        // Havuzlar�n olu�turulmas�
        for (int i = 0; i < pools.Length; i++)
        {
            // Havuzun boyutu kadar nesne yarat�l�r ve havuzda depolan�r
            pools[i].PooledObjects = new Queue<GameObject>();
            for (int k = 0; k < pools[i].poolsize; k++)
            {
                GameObject obj = GameManager.Instance.spawnScript.Spawn(pools[i].objectPrefabs, pools[i].ParentObject);
                obj.SetActive(false);
                pools[i].PooledObjects.Enqueue(obj);
            }
        }
    }

    // Belirtilen nesne tipinde bir nesne d�nd�r�r
    public GameObject GetPoolObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        // Nesneler havuzda kalmam��sa havuzun boyutu art�r�l�r
        if (pools[objectType].PooledObjects.Count == 0)
        {
            AddSizePool(5f, objectType);
        }
        GameObject obj = pools[objectType].PooledObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    // Havuza bir nesne geri ekler
    public void SetPoolObject(GameObject poolObject, int objectType)
    {
        if (objectType >= pools.Length)
        {
            return;
        }
        pools[objectType].PooledObjects.Enqueue(poolObject);
        poolObject.SetActive(false);
    }

    // Havuzun boyutunu art�r�r
    public void AddSizePool(float amount, int objectType)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = GameManager.Instance.spawnScript.Spawn(pools[objectType].objectPrefabs, pools[objectType].ParentObject);
            obj.SetActive(false);
            pools[objectType].PooledObjects.Enqueue(obj);
        }
    }
}