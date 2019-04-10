using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : PoolSpawner {

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
            newEnemy.transform.position = new Vector2(enemysXpos, enemysYpos);
            enemysYpos = enemysYpos + gapBetweenEnemies;

            newEnemy.SetActive(true);
        }
    }


}
