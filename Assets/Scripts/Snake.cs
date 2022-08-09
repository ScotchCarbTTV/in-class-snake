using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Snake : MonoBehaviour
{
    /* Your task is to recreate the classic video game Snake. This script will have to address the following gameplay features:
     
     - Move the snake head in the direction that the user presses on the arrow keys, every half-second repeating
     - Every subsequent segment (square) of the snake body should move to the space occupied by the preceding segment every time the head moves
     - When the snake head reaches the edge of the play area, it should teleport to the opposite side
     - When the snake head overlaps the apple object, increase the number of segments on the snake by 1, and respawn the apple somewhere else that is both
       on screen and is NOT occupied by the snake body or head
     - If the snake head overlaps a segment of its own body, end the game (or restart the level) 
     
     The following concepts will be useful to accomplish this task:
        
     - Loops (specifically FOR and FOREACH loops) to help move all the segments at once
     - Arrays/Lists (for grouping all the body segments)
     - Random.Range will help with randomly deciding where to put the apple
     - OnTriggerEnter will help detect objects such as the apple or body
     - Vector3 AND Transform will be useful for positioning objects such as the apple and the snake segments
    
    You should only need this one script to accomplish this task, but if you feel you need to write others feel free to do so */


    //vector3 variable for direction
    [SerializeField] private Vector3 direction;
    
    
    //values for storing the inputs
    private float horizontal;
    private float vertical;

    //vector 3 value to store 'next body segment position'
    [SerializeField] private Vector3 nextBodyPos;
    [SerializeField] private Vector3 currentBodyPos;

    //list of game objects for storing the snake body segments
    [SerializeField] private List<GameObject> bodySeg = new List<GameObject>();

    //prefab game object for body sections
    [SerializeField] private GameObject bodySegPrefab;

    [SerializeField] private NodeManager nodeManager;

    private GameObject newBodySeg;

    private bool spawnBodySeg = false;

    private void Awake()
    {
        direction = new Vector3(1, 0, 0);
    }

    void Start() {
        //Calls this function every half second after the game starts indefinitely 
        InvokeRepeating( nameof(MoveSnakeEveryHalfSecond), 0.5f, 0.5f );
    }

    
    void Update() 
    {
        //whenever player uses horizontal or vertical input assign its value to the relevant float
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal < 0 && direction.x != 1)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (horizontal > 0 && direction.x != -1)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (vertical < 0 && direction.y != 1)
        {
            direction = new Vector3(0, -1, 0);
        }
        else if(vertical > 0 && direction.y != -1)
        {
            direction = new Vector3(0, 1, 0);
        }
    }

    //Use this function to move the snake 
    void MoveSnakeEveryHalfSecond() {
        //assign the transform position of the snake head to the next body segment vector3
        nextBodyPos = transform.position;
        //add diretion to transform position of head
        transform.position += direction;
        
        GameObject lastSeg = bodySeg.Last();
        //loop through all the body segments
        for (int x = 0; x < bodySeg.Count; x++)
        {
            //assign their current position to a temporary vector3
            currentBodyPos = bodySeg[x].transform.position;
            //change their position to next body segment
            bodySeg[x].transform.position = nextBodyPos;
            //assign temporary vector 3 to next body segment vector 3
            nextBodyPos = currentBodyPos;

            if (bodySeg[x] == lastSeg && spawnBodySeg == true)
            {
                newBodySeg = Instantiate(bodySegPrefab, currentBodyPos, Quaternion.identity);

            }
        }     
        if(newBodySeg != null)
        {
            bodySeg.Add(newBodySeg);
            newBodySeg = null;
            spawnBodySeg = false;
        }
    }

    //Call this to restart the game
    void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Call this to quit the game when in the editor (NOTE: this will not work if you build the executable)
    void QuitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Snake")
        {
            Debug.Log("OOPS");
            Time.timeScale = 0;
        }
        else if(collision.gameObject.tag == "Apple")
        {
            Debug.Log("CHOMP");
            spawnBodySeg = true;

            collision.transform.position = nodeManager.GetRandomNodeTransform().position;
        }
        else if(collision.gameObject.tag == "WallR")
        {
            transform.position = new Vector3(-17, transform.position.y, 0);
        }
        else if(collision.gameObject.tag == "WallL")
        {
            transform.position = new Vector3(17, transform.position.y, 0);
        }
        else if (collision.gameObject.tag == "WallT")
        {
            transform.position = new Vector3(transform.position.x, -9, 0);
        }
        else if (collision.gameObject.tag == "WallB")
        {
            transform.position = new Vector3(transform.position.x, 9, 0);
        }
    }
}





