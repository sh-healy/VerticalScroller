using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            gameObject.SetActive(false);
        }
    }
}
