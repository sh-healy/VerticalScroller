using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : PoolSpawner {

    public static EnemyBulletController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

    }

}
