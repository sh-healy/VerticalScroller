using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : PoolSpawner {

    public static EnemyBulletController Instance { get; private set; }

    #region Monobehaviour methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

    }

    protected override void Start()
    {
        base.Start();
        GameMang.Instance.GameFinished += GameEnd;
    }

    #endregion Monobehaviour methods

    /// <summary>
    /// Invoked when player hit
    /// </summary>
    private void GameEnd()
    {
        ReturnAllObjectsToPool();
    }

}
