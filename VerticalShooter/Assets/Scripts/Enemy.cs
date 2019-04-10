using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    /// <summary>
    /// Enemies controller, set when spawned
    /// </summary>
    public EnemyController controller;

    private void OnEnable()
    {
        InvokeRepeating("FireBullets", 0, .5f);
        //FireBullets();
    }

    /// <summary>
    /// Retrieves a bullet from the bullet pool and calls the fire method on the bullet script
    /// </summary>
    private void FireBullets()
    {
        GameObject enemyBullet = EnemyBulletController.Instance.GetObjectFromPool();

        if (enemyBullet != null)
        {
            enemyBullet.SetActive(true);
            enemyBullet.GetComponent<Bullet>().Fire(transform.position, controller.PlayerPos.position);
        }
        
    }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
