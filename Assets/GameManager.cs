using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script is to be attached to a GameObject called GameManager in the scene. It is to be used to manager the settings and overarching gameplay loop.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public int currentScore = 0; //The current score in this round.
    public int highScore = 0; //The highest score achieved either in this session or over the lifetime of the game.
    public TMP_Text currentScoreUI;
    public TMP_Text currentRoundUI;

    [Header("GameFinish")]
    public TMP_Text finalScoreUI;
    public TMP_Text youwinorloseUI;
    public GameObject uiGame;
    public GameObject uiGameOver;
   

    [Header("Startup")]
    public TMP_Text readyUI;
    public float Starttime1 = 2;
    public AudioClip GetReady;

    [Header("Playable Area")]
    public float levelConstraintTop; //The maximum positive Y value of the playable space.
    public float levelConstraintBottom; //The maximum negative Y value of the playable space.
    public float levelConstraintLeft; //The maximum negative X value of the playable space.
    public float levelConstraintRight; //The maximum positive X value of the playablle space.

    [Header("Gameplay Loop")]
    public bool isGameRunning; //Is the gameplay part of the game current active?
    public float gameTimeRemaining = 121; //Two munutes, plus two seconds for when player can't move during the get ready
    public TMP_Text TimeUI;
    public int currentRound = 1;

    void Start()
    {
        currentScoreUI.text = "0";
        currentRoundUI.text = "1";

        GetComponent<AudioSource>().PlayOneShot(GetReady);
    }

    void Update()
    {
        // Stops the player from moving right away and shows ready message
        if (Starttime1 > 1)
        {
            Starttime1 -= Time.deltaTime;
            readyUI.text = "Ready?";

        }
        else if (Starttime1 > 0)
        {
            GameObject playerMover = GameObject.Find("Player");
            Player playerScript = playerMover.GetComponent<Player>();

            playerScript.playerCanMove = true;

            Starttime1 -= Time.deltaTime;
            readyUI.text = "Go!";
        }
        else if (Starttime1 <= 0)
        {
            readyUI.text = " ";
        }

        if (gameTimeRemaining > 0)
        {
            TimeUI.text = Mathf.Round(gameTimeRemaining).ToString();
            gameTimeRemaining -= Time.deltaTime;
        }
        else
        {
            GameOver(false);
        }

    }

    public void UpdateScore(int scoreNum)
        //Scoring goes:
        // 1 point: Move up
        // 70 points: Reach a goal
        // 100 points: Get a bonus super challege coin
        // Then, if the player gets all 5 frogs to the goals:
        // 10 (?) points for each spare second
    {
        currentScore += scoreNum;
        currentScoreUI.text = currentScore.ToString();
    }

    // Adds a round to the counter and puts the player back to the start
    public int RoundAdder(int round)
    {
        round += 1;
        currentRoundUI.text = round.ToString();

        GameObject playerMover = GameObject.Find("Player");
        Player player = playerMover.GetComponent<Player>();

        player.transform.position = new Vector2(0f, -5.5f);

        return round;


    }

    // Self explanatory
    public void GameOver(bool win)
    {
        if (win == true)
        {
            youwinorloseUI.text = "You win!";

            int timeleft = (int)gameTimeRemaining;

            UpdateScore(timeleft * 10);
        }
        else if (win == false)
        {
            youwinorloseUI.text = "You lose!";
        }

        finalScoreUI.text = currentScore.ToString();
        uiGameOver.SetActive(true);

        uiGame.SetActive(false);
    }

 }
