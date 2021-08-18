using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using TMPro;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining = 3; //PLayers actual lives remaining.
    public TMP_Text currentLivesUI;

    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?
    public bool onPlatform = false;
    public bool inWater = false;

    private GameManager myGameManager; //A reference to the GameManager in the scene.

    public GameObject explosionFX;
    public GameObject FrogIcon;
    public int round = 1;

    //Audio section
    public AudioClip jumpSound;
    public AudioClip deathSound;

    //Setting if score zones can be landed in again
    public bool Scorezone0Filled = false;
    public bool Scorezone1Filled = false;
    public bool Scorezone2Filled = false;
    public bool Scorezone3Filled = false;
    public bool Scorezone4Filled = false;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
        currentLivesUI.text = playerLivesRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsAlive && playerCanMove == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < myGameManager.levelConstraintTop)
            {
                //transform.position = transform.position + new Vector3(0, 1, 0);
                transform.Translate(new Vector2(0, 1));
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
                //currently adds 1 point when moving up
                myGameManager.UpdateScore(1);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom)
            {
                transform.Translate(new Vector2(0, -1));
                GetComponent<AudioSource>().PlayOneShot(jumpSound);

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft)
            {
                transform.Translate(new Vector2(-1, 0));
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight)
            {
                transform.Translate(new Vector2(1, 0));
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
        }

    }

    private void LateUpdate()
    {
        if(inWater == true && onPlatform == false && playerIsAlive == true)
        {
            PlayerKiller();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerIsAlive)
        {
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                PlayerKiller();
            }
            else if (collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(collision.transform);
                onPlatform = true;
            }
            else if(collision.transform.tag == "Water")
            {
                inWater = true;
            }
            else if (collision.transform.tag == "Coiny")
            {
                myGameManager.UpdateScore(10);
                Destroy(collision.gameObject);
            }

            // Scoring Zone code
            else if (collision.transform.tag == "Scorer0")
            {
                if (Scorezone0Filled == false)
                {
                    myGameManager.UpdateScore(100);
                    Scorezone0Filled = true;

                    Vector2 Goal0 = new Vector2(-6.14f, 6.39f);
                    GameObject Score0 = Instantiate(FrogIcon, Goal0, Quaternion.identity) as GameObject;

                    round = myGameManager.RoundAdder(round);
                }
                else
                {
                    PlayerKiller();
                }
            }
            else if (collision.transform.tag == "Scorer1")
            {
                if (Scorezone1Filled == false)
                {
                    myGameManager.UpdateScore(100);
                    Scorezone1Filled = true;

                    Vector2 Goal1 = new Vector2(-3.09f, 6.39f);
                    GameObject Score1 = Instantiate(FrogIcon, Goal1, Quaternion.identity) as GameObject;

                    round = myGameManager.RoundAdder(round);
                }
                else
                {
                    PlayerKiller();
                }

            }
            else if (collision.transform.tag == "Scorer2")
            {
                if (Scorezone2Filled == false)
                {
                    myGameManager.UpdateScore(100);
                    Scorezone2Filled = true;

                    Vector2 Goal2 = new Vector2(-0.04f, 6.39f);
                    GameObject Score2 = Instantiate(FrogIcon, Goal2, Quaternion.identity) as GameObject;

                    round = myGameManager.RoundAdder(round);
                }
                else
                {
                    PlayerKiller();
                }
            }
            else if (collision.transform.tag == "Scorer3")
            {
                if (Scorezone3Filled == false)
                {
                    myGameManager.UpdateScore(100);
                    Scorezone3Filled = true;

                    Vector2 Goal3 = new Vector2(3.06f, 6.39f);
                    GameObject Score3 = Instantiate(FrogIcon, Goal3, Quaternion.identity) as GameObject;

                   round = myGameManager.RoundAdder(round);
                }
                else
                {
                    PlayerKiller();
                }
            }
            else if (collision.transform.tag == "Scorer4")
            {
                if (Scorezone4Filled == false)
                {
                    myGameManager.UpdateScore(100);
                    Scorezone4Filled = true;

                    Vector2 Goal4 = new Vector2(6.14f, 6.39f);
                    GameObject Score4 = Instantiate(FrogIcon, Goal4, Quaternion.identity) as GameObject;

                   round = myGameManager.RoundAdder(round);
                }
                else
                {
                    PlayerKiller();
                }
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (playerIsAlive)
        {
            if (collision.transform.GetComponent<Platform>() != null)
            {
                transform.SetParent(null);
                onPlatform = false;
            }
            else if (collision.transform.tag == "Water")
            {
                //PlayerKiller();
                inWater = false;
            }
        }
    }

    void PlayerKiller()
    {
        if (playerLivesRemaining != 0)
        {
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            Instantiate(explosionFX, transform.position, Quaternion.identity);

            GameObject playerMover = GameObject.Find("Player");
            Player player = playerMover.GetComponent<Player>();
            player.transform.position = new Vector2(0f, -5.5f);

            playerLivesRemaining -= 1;
            currentLivesUI.text = playerLivesRemaining.ToString();
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            Instantiate(explosionFX, transform.position, Quaternion.identity);

            GetComponent<SpriteRenderer>().enabled = false;

            playerIsAlive = false;
            playerCanMove = false;
        }

    }

}
