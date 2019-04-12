using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag!="Spawner")
            coll.gameObject.SetActive(false);
    }
}
