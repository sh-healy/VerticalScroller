using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour {

    /// <summary>
    /// pool of enemy objects
    /// </summary>
    [HideInInspector] public List<GameObject> objectPool;

    /// <summary>
    /// amount of enemys in pool
    /// </summary>
    [SerializeField] private int poolAmount;

    /// <summary>
    /// enemy object
    /// </summary>
    [SerializeField] private GameObject objectInPool;


    // Use this for initialization
    protected virtual void Start () {

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject poolObj = Instantiate(objectInPool);
            objectPool.Add(poolObj);
        }

    }

    /// <summary>
    /// Gets inactive object from pool
    /// </summary>
    /// <returns>returns the first object in the pool that isnt already active</returns>
    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
                return objectPool[i];

        }

        return null;
    }
}
