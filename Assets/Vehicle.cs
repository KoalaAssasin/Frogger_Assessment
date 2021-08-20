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
    public Vector2 startingPosition; //This variable is to be used to indicate where on the map the vehicle starts (or spawns)
    public Vector2 endPosition; //This variablle is to be used to indicate the final destination of the vehicle.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed * moveDirection);

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

        // Do rest of car rows

    }

}
