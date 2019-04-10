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
    public void Shoot(Vector3 spawnerPos , Vector3 targetDir)
    {
        transform.position = spawnerPos;
        GetComponent<Rigidbody2D>().AddForce(targetDir * speed);
    }

    private void OnDisable()
    {
        beingFired = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BULLET HIT");
        gameObject.SetActive(false);
    }
}
