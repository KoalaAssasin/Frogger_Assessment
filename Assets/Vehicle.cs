using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script must be utlised as the core component on the 'vehicle' obstacle in the frogger game.
/// </summary>
public class Vehicle : MonoBehaviour
{
    /// <summary>
    /// -1 = left, 1 = right
    /// </summary>
    public int moveDirection = 0; //This variabe is to be used to indicate the direction the vehicle is moving in.
    public float speed; //This variable is to be used to control the speed of the vehicle.

    // Start position and end position are still setup here (and for the platforms), but in the end I found it more reliable not to use them in my vehicle code. Used for the two bugs though
    public Vector2 startingPosition; 
    public Vector2 endPosition; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed * moveDirection);

        // Cars
        if ( this.transform.tag == "car row 1")
        {
            if ((transform.position.x) < (-12))
            {
                transform.position = new Vector3(8f, -4.5f, 0f);
            }
        }

        if (this.transform.tag == "car row 2")
        {
            if ((transform.position.x) > 12)
            {
                transform.position = new Vector3(-8f, -3.5f, 0f);
            }
        }

        if (this.transform.tag == "car row 3")
        {
            if ((transform.position.x) < (-12))
            {
                transform.position = new Vector3(8f, -2.5f, 0f);
            }
        }

        if (this.transform.tag == "car row 4")
        {
            if ((transform.position.x) > 12)
            {
                transform.position = new Vector3(-8f, -1.5f, 0f);
            }
        }

        // Do rest of car rows

        //Crocs
        if (this.transform.tag == "croc row 1")
        {
            if ((transform.position.x) > 12)
            {
                transform.position = new Vector3(-8f, 5.5f, 0f);
            }
        }

        if (this.transform.tag == "croc row 2")
        {
            if ((transform.position.x) > 12)
            {
                transform.position = new Vector3(-8f, 3.5f, 0f);
            }
        }

        //Bugs
        if (this.transform.tag == "bug row 1")
        {
            if ((transform.position.x) < endPosition.x)
            {
                transform.position = startingPosition;
            }
        }

    }

}
