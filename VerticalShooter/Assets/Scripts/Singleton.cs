﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T Instance { get; private set; }
	
    // Use this for initialization
	protected void Awake () {

        if (Instance == null)
            Instance = (T)this;
        else if (Instance != this)
            Destroy(this); 

    }
	
	
}
