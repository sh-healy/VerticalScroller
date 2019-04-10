using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public GameObject prefab;
    public float speed;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Instantiate(prefab);
            //rb = prefab.GetComponent<Rigidbody2D>();
            //rb.velocity = transform.up * speed;

        }
    }
}
