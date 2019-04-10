using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : PoolSpawner {

    public static PlayerBulletController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

    }
}
