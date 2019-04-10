using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    /// <summary>
    /// script that spawns enemies
    /// </summary>
    [SerializeField] private EnemySpawner enemySpawner;

    /// <summary>
    /// distance between spawn points
    /// </summary>
    [SerializeField] private int spaceBetweenSpawns=10;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemySpawner.SpawnEnemies();
            transform.position = new Vector2(transform.position.x, transform.position.y+spaceBetweenSpawns);
        }
        
    }
}
