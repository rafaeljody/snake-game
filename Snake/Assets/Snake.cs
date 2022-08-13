using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // by default, snake will move right
    private Vector2 input;
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab; // this is the reference of prefab
    public int initialSize = 4;


    private void Start()
    {
        ResetState();

    }

    private void Update() // update is every single frame our game is running, but the frame rate variable (depends on hardware)
    {
        // Only allow turning up or down while moving in the x-axis
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2.left;
            }
        }


    }

    // make our snake move
    // because our snake using rigid body, indicates that our snake is physics object
    // that's why we handle the movement in fixed update
    private void FixedUpdate() // fix time interval (important for physics related code)
    {
        if (input != Vector2.zero)
        {
            _direction = input;
        }

        // loop for the tail following the one segment before it
        // until it reach the head
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        
        
        // accessing the transform object (position of the snake)
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );

    }


    private void Grow()
    {
        // instantitate copy of our prefab
        Transform segment = Instantiate(this.segmentPrefab);

        // set the pos of the segment at the end of our snake object
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState() 
    {
        // clear our list of segment and destroy the gameobject
        // start at index 1, cause index 0 is the head of the snake
        for (int i = 1; i< _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();


        // make sure to add back the head of the snake
        _segments.Add(this.transform);

        for(int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }


        //reset the pos of snake back
        this.transform.position = Vector3.zero;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Food")
        {
            Grow();
        } else if(collision.tag == "Obstacle")
        {
            ResetState();
        }
    }



}
