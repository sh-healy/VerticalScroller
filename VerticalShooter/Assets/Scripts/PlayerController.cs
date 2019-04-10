using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// bool to check whether jump button is being pressed
    /// </summary>
    private bool moveForward;
    
    /// <summary>
    /// player's rigidbody
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Speed for the player to travel
    /// </summary>
    [SerializeField] private float speed;

    /// <summary>
    /// Enemy Controller
    /// </summary>
    [SerializeField] private EnemyController enemyController;

    public bool shoot;
    public test tst;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (shoot)
            InvokeRepeating("Shoot", 1, 20f);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKey(KeyCode.Space))
            moveForward = true;
        if (Input.GetKeyUp(KeyCode.Space))
            moveForward = false;
    }

    private void FixedUpdate()
    {
        if (moveForward)
        {
            //Vector3 newPos = (speed * Vector3.up) * Time.deltaTime;
            rb.MovePosition(transform.position + Vector3.up);
        }
        else
        {
            rb.MovePosition(transform.position + Vector3.down);
        }

    }

    private void Shoot()
    {
        GameObject playerBullet = PlayerBulletController.Instance.GetObjectFromPool();

        if (playerBullet != null)
        {
            GameObject closestEnemy = enemyController.activeEnemies.GetClosest(transform.position);

            playerBullet.SetActive(true);
            if (closestEnemy != null)
            {
                Debug.Log("found enemy: " + closestEnemy.name);
                Vector3 bulletTarget = closestEnemy.transform.position;
                playerBullet.GetComponent<Bullet>().Shoot(transform.position, bulletTarget);
            }
            else
                Debug.Log("No enemy");


        }

    }

}
