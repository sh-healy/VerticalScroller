using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : Singleton<test> {

    public GameObject[] enemies;
    public Tree enemyTree;

	// Use this for initialization
	void Awake () {

        enemyTree = new Tree();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyTree.Add(enemies[i]);
        }

       // enemyTree.ShowList();
        
    }

    // Update is called once per frame
    void Update() {
       
    }
}
