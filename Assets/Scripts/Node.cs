using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    //bool called occupied
    private bool occupied;

    //variable referencing the NodeManager
    [SerializeField] private NodeManager nodeManager;


    private void Start()
    {
        //find object tagged with NodeManager and retrieve the NodeManager script
        GameObject.FindGameObjectWithTag("NodeManager").TryGetComponent<NodeManager>(out nodeManager);
    }

    //ToggleOccupied method
    public void ToggleOccupied(bool _occupied)
    {
        //if the occupied bool is true 
        if(_occupied == true)
        {
            //then call the method on NodeManager to remove Node from unoccupiedNodes
            nodeManager.RemoveNode(gameObject);
        }
        //if the bool is false 
        else if(_occupied == false)
        {
            //then call the method on NodeManager to add this Node to unoccupiedNodes
            nodeManager.AddNode(gameObject);
        }
    }

    //OnTriggerStay2d
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if occupied isn't true then set it to true
        if(occupied != true)
        {
            occupied = true;
        }
    }


    //OnTriggerEnter2d
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set occupied to true
        occupied = true;
        //call ToggleOccupied
        ToggleOccupied(occupied);
    }


    //OnTriggerExit2d
    private void OnTriggerExit2D(Collider2D collision)
    {
        //set occupied to false
        occupied = false;
        //call ToggleOccupied
        ToggleOccupied(occupied);
    }
    
}
