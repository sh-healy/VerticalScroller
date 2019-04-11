using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    /// <summary>
    /// Player position to keep track of how far they go
    /// </summary>
    [SerializeField]private Transform player;

    /// <summary>
    /// Players y position
    /// </summary>
    private float playersY;

    /// <summary>
    /// Highest score player has eveer gotten
    /// </summary>
    public float HighestScore { get; private set; }

    /// <summary>
    /// current score
    /// </summary>
    public float CurrentScore { get; private set; }

	// Use this for initialization
	private void Start () {

        if (PlayerPrefs.HasKey("HighestScore"))
            HighestScore = PlayerPrefs.GetFloat("HighestScore");
        else
            HighestScore = 0;
	}
	
	// Update is called once per frame
	private void Update () {

        playersY = Mathf.Round(player.position.y);
        CurrentScore = playersY > CurrentScore ? playersY : CurrentScore;
		
	}

    private void GameEnd()
    {
        HighestScore = CurrentScore;
        PlayerPrefs.SetFloat("HighestScore", HighestScore);
    }
}
