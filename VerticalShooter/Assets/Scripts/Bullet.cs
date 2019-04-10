using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    /// <summary>
    /// checks to see if the bullet is active and being fired
    /// </summary>
    private bool beingFired;

    /// <summary>
    /// speed the bullet travels ats
    /// </summary>
    [SerializeField] float speed;

    /// <summary>
    /// direction for the bullet to go
    /// </summary>
    private Vector3 targetDir;

    /// <summary>
    /// Sets the diretion of the bullet
    /// </summary>
    /// <param name="direction">the target the bullet should aim towards</param>
    public void Fire(Vector3 spawnerPos , Vector3 targetDir)
    {
        transform.position = spawnerPos;
        targetDir = (targetDir - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    private void Update()
    {
        //if(beingFired)
            //GetComponent<Rigidbody2D>().AddForce (Vector3.forward * speed);
    }

    private void OnDisable()
    {
        beingFired = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.SetActive(false);
    }
}
