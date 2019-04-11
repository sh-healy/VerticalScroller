using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D coll)
    {
    //    Debug.Log("Boundary Hit: " + coll.gameObject.name);
        coll.gameObject.SetActive(false);
    }
}
