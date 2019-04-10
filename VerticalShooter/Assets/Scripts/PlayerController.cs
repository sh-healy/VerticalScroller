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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

}
