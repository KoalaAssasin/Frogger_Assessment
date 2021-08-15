using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
   
    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?
    public bool onPlatform = false;
    public bool inWater = false;

    private GameManager myGameManager; //A reference to the GameManager in the scene.

    public GameObject explosionFX;

    //Audio section
    public AudioClip jumpSound;
    public AudioClip deathSound;


    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsAlive)
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
                //Another way to do this: (still need to add audiosource reference for it to work though)
                //GetComponent<AudioSource>().clip = jumpSound;
                //GetComponent<AudioSource>().Play();
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

        GetComponent<SpriteRenderer>().enabled = false;

        playerIsAlive = false;
        playerCanMove = false;
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        Instantiate(explosionFX, transform.position, Quaternion.identity);
    }

}
