using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMang : Singleton<GameMang> {

    /// <summary>
    /// Notifies listening object game has ended
    /// </summary>
    public Action GameFinished;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Called when player is killed
    /// </summary>
    public void GameEnd()
    {
        GameFinished?.Invoke();
    }



}
