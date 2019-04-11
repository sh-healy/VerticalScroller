using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    /// <summary>
    /// GameManager in the scene
    /// </summary>
    private ScoreKeeper scoreKeeper;

    /// <summary>
    /// set to indicate game state
    /// </summary>
    private bool gameOver = false;

    /// <summary>
    /// Text that will show score
    /// </summary>
    [SerializeField] private Text scoreTxt;

    /// <summary>
    /// Text that will show highscore
    /// </summary>
    [SerializeField] private Text highscore;

    /// <summary>
    /// Ui for game over
    /// </summary>
    [SerializeField] private GameObject gameOverScreen;

    // Use this for initialization
    private void Start ()
    {
        GameMang.Instance.GameFinished += GameEnd;
        scoreKeeper = ScoreKeeper.Instance;
        highscore.text = scoreKeeper.HighestScore.ToString();

        StartCoroutine(UpdateScore());
	}

    /// <summary>
    /// Updates the score every second
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateScore()
    {
        while (!gameOver)
        {
            scoreTxt.text = scoreKeeper.CurrentScore.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Invoked when game ends
    /// </summary>
    private void GameEnd()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }
	
	


}
