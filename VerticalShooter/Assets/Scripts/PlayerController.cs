using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// bool to check whether jump button is being pressed
    /// </summary>
    private bool moveForward;
    
    /// <summary>
    /// Game over and player shouldn't move
    /// </summary>
    private bool gameOver = false;

    /// <summary>
    /// player's rigidbody
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Speed for the player to travel
    /// </summary>
    [SerializeField] private float speed;

    /// <summary>
    /// Speed for the player to travel
    /// </summary>
    [SerializeField] private float fireRate = 1f;

    /// <summary>
    /// Stops player being able to shoot
    /// </summary>
    [SerializeField] private bool shoot;

    /// <summary>
    /// Enemy Controller
    /// </summary>
    [SerializeField] private EnemyController enemyController;

    

    #region Monobehaviour methods

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (shoot)
            InvokeRepeating("Shoot", 1, fireRate);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Jump"))
            moveForward = true;
        if (Input.GetButtonUp("Jump"))
            moveForward = false;
    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            if (moveForward)
                 rb.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
            else
                rb.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            GameMang.Instance.GameEnd();
            gameOver = true;
            CancelInvoke();
            //gameObject.SetActive(false);
        }
    }

    #endregion Monobehaviour methods

    /// <summary>
    /// AutoFires bullet at closest enemy
    /// </summary>
    private void Shoot()
    {
        GameObject playerBullet = PlayerBulletController.Instance.GetObjectFromPool();

        if (playerBullet != null)
        {
            GameObject closestEnemy = enemyController.activeEnemies.GetClosest(transform.position);

            
            if (closestEnemy != null)
            {
                Debug.Log("ClosestEnemy is: " +  closestEnemy.name);
                playerBullet.SetActive(true);

                Vector3 bulletTarget = closestEnemy.transform.position;
                playerBullet.GetComponent<Bullet>().Shoot(transform.position, bulletTarget);
            }


        }

    }

    

}
