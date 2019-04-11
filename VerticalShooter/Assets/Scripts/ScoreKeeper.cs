using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    
    /// <summary>
    /// Highest score player has eveer gotten
    /// </summary>
    public float HighestScore { get; private set; }

    /// <summary>
    /// current score
    /// </summary>
    public float CurrentScore { get; private set; }

    /// <summary>
    /// Players y position
    /// </summary>
    private float playersY;

    /// <summary>
    /// Player position to keep track of how far they go
    /// </summary>
    [SerializeField] private Transform player;

    #region Monobehaviour methods

    // Use this for initialization
    private void Start () {

        if (PlayerPrefs.HasKey("HighestScore"))
            HighestScore = PlayerPrefs.GetFloat("HighestScore");
        else
            HighestScore = 0;

        GameMang.Instance.GameFinished += GameEnd;
	}
	
	// Update is called once per frame
	private void Update () {

        playersY = Mathf.Round(player.position.y);
        CurrentScore = playersY > CurrentScore ? playersY : CurrentScore;
		
	}

    #endregion Monobehaviour methods

    /// <summary>
    /// Invoked when player hit
    /// </summary>
    private void GameEnd()
    {
        HighestScore = CurrentScore;
        PlayerPrefs.SetFloat("HighestScore", HighestScore);
    }
}
