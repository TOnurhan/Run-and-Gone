using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObject
{
    public int objectAmount;
    public GameObject objectPrefab;
}

public class ObjectPool : MonoBehaviour
{
    public List<PoolObject> objectPoolList;
    private List<GameObject> pooledObjectList;
    GameObject spawnedObject;
    int index;

    private void Awake()
    {
        pooledObjectList = new List<GameObject>();

        for(int i = 0; i < objectPoolList.Count; i++)
        {
            for(int j = 0; j < objectPoolList[i].objectAmount;j++)
            {
                GameObject spawnedObject = Instantiate(objectPoolList[i].objectPrefab);
                spawnedObject.SetActive(false);
                spawnedObject.transform.parent = transform;
                spawnedObject.transform.position = transform.position;
                pooledObjectList.Add(spawnedObject);
            }
        }
    }

    public void SpawnPooledObject(Vector3 spawnPosition)
    {
        spawnedObject = GetPooledObject();
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = spawnPosition;
    }

    private GameObject GetPooledObject()
    {
        while (true)
        {
            index = Random.Range(0, pooledObjectList.Count);
            if (!pooledObjectList[index].activeInHierarchy)
            {
                return pooledObjectList[index];
            }
        }
    }
}
