using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // randomize the food position, inside the grid area that we set before

    public BoxCollider2D gridArea;

    private void Start() // called very first frame this script is enable on a game object
    {
        RandomizedPosition();
    }

    public void RandomizedPosition()
    {
        // bounds are the property of BoxCollider2D
        // this property gives us the exact size and everything about the area
        // so our food will spwan randomly inside the grid area
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // assign our food pos into this coordinate
        // make sure all the position is a whole number
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    // this func will called automatically
    // whenever our game object collide with each other
    // trigger is on
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the food is collide with the tag of "player"
        // which is our snake
        if(collision.tag == "Player")
        {
            // if its collide, than the food will randomize its pos again
            RandomizedPosition();
        }
    }


}
