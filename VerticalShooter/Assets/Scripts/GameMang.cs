using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMang : Singleton<GameMang> {

    /// <summary>
    /// Notifies listening object game has ended
    /// </summary>
    public Action GameFinished;

    /// <summary>
    /// Called when player is killed
    /// </summary>
    public void GameEnd()
    {
        Debug.Log("Game Over");
        GameFinished?.Invoke();
    }



}
