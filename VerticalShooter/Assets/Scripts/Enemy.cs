﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    /// <summary>
    /// Enemies controller, set when spawned
    /// </summary>
    public EnemyController controller;

    /// <summary>
    /// rate of repeat on enemy fire
    /// </summary>
    private float fireRate;

    public bool shoot;

    private void OnEnable()
    {
        fireRate = Random.Range(.6f, 1.2f);
        if (shoot)
            InvokeRepeating("Shoot", 0, fireRate);
        //FireBullets();
    }

    /// <summary>
    /// Retrieves a bullet from the bullet pool and calls the fire method on the bullet script
    /// </summary>
    private void Shoot()
    {
        GameObject enemyBullet = EnemyBulletController.Instance.GetObjectFromPool();

        if (enemyBullet != null)
        {
            enemyBullet.SetActive(true);

            Vector3 bulletTarget = new Vector3(controller.PlayerPos.position.x, transform.position.y, transform.position.z);
            enemyBullet.GetComponent<Bullet>().Shoot(transform.position, bulletTarget);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet" || coll.gameObject.tag== "Boundaries")
        {
            //remove it from active enemies list
            controller.activeEnemies.RemoveObj(gameObject);
            gameObject.SetActive(false);

        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
