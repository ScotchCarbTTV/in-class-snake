using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    //prefab node object
    [SerializeField] GameObject navNode;

    //list of available nodes
    [SerializeField] private List<GameObject> availNodes = new List<GameObject>();

    private void Awake()
    {
        //call generategrid method
        GenerateGrid();
    }


    //method to generate the 100x100 grid
    public void GenerateGrid()
    {
        for (float x = -17; x < 18; x++)
        {
            for (float z = -9; z < 10; z++)
            {
                transform.position = new Vector3(x, z, 0);

                GameObject _node =  Instantiate(navNode, transform.position, Quaternion.identity);
                availNodes.Add(_node);
            }
        }
    }

    public Transform GetRandomNodeTransform()
    {
        int ranNode = Random.Range(0, availNodes.Count);
        GameObject node = availNodes[ranNode];
        return node.transform;
    }

    public void RemoveNode(GameObject node)
    {
        //check if node is present in list
        if (availNodes.Contains(node))
        {
            availNodes.Remove(node);
        }
    }

    public void AddNode(GameObject node)
    {
        if (!availNodes.Contains(node))
        {
            availNodes.Add(node);
        }
    }


}
