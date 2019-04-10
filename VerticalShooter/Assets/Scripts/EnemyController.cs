using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PoolSpawner {

    /// <summary>
    /// List of enemies that are currently active in the scene
    /// </summary>
    [HideInInspector] public Tree activeEnemies = new Tree();

    /// <summary>
    /// players position
    /// </summary>
    [SerializeField] private Transform playerPos;

    /// <summary>
    /// Amount of enemies to spawn in front of player
    /// </summary>
    [SerializeField] private int spawnAmount;

    /// <summary>
    /// The amount of space between each enemy
    /// </summary>
    [SerializeField] private int gapBetweenEnemies=2;

    /// <summary>
    /// The amount of space between enemy and player
    /// </summary>
    [SerializeField] private int gapBetweenPlayerAndEnemy = 5;

    public Transform PlayerPos
    {
        get
        {
            return playerPos;
        }
    }

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < objectPool.Count; i++)
        {
            objectPool[i].GetComponent<Enemy>().controller = this;
        }
    }

    /// <summary>
    /// Spawns an amount of players in front of the player
    /// </summary>
    public void SpawnEnemies()
    {
        float enemysYpos=playerPos.position.y+5;
        float enemysXpos;

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject newEnemy = GetObjectFromPool();

            enemysXpos = Random.Range(1, 10) % 2 == 0 ? gapBetweenPlayerAndEnemy*-1 : gapBetweenPlayerAndEnemy;

            if (newEnemy != null)
            {
                newEnemy.name = "enemy " + i;
                newEnemy.transform.position = new Vector2(enemysXpos, enemysYpos);
                enemysYpos = enemysYpos + gapBetweenEnemies;

                newEnemy.SetActive(true);
                activeEnemies.Add(newEnemy);
            }
            
        }
    }



}
