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

    /// <summary>
    /// Stops enemy being able to shoot
    /// </summary>
    public bool shoot;

    /// <summary>
    /// bool to track if the game was stopped
    /// </summary>
    private bool applicationWasQuit;

    #region Monobehaviour methods
    private void OnEnable()
    {
        fireRate = Random.Range(1f, 2f);
        if (shoot)
            InvokeRepeating("Shoot", 0, fireRate);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (!applicationWasQuit)
        {
            controller.activeEnemies.RemoveObj(gameObject);
            CancelInvoke();
        }
        
    }

    private void OnApplicationQuit()
    {
        applicationWasQuit = true;
    }

    #endregion Monobehaviour methods

    /// <summary>
    /// Retrieves a bullet from the bullet pool and calls the fire method on the bullet script
    /// </summary>
    private void Shoot()
    {
        GameObject enemyBullet = EnemyBulletController.Instance.GetObjectFromPool();

        if (enemyBullet != null)
        {
            enemyBullet.SetActive(true);

            
            Vector3 bulletTarget = transform.position.x>controller.PlayerPos.position.x? Vector3.left : Vector3.right;//new Vector3(0, controller.PlayerPos.position.y, transform.position.z);
            enemyBullet.GetComponent<Bullet>().Shoot(transform.position, bulletTarget);
        }
        
    }

    
}
