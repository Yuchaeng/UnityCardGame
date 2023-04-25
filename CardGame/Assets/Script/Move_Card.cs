using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Card : MonoBehaviour
{
    Rigidbody2D myRigid;
    Vector2 myPosition;
    Vector2 goalPosition;
    Vector2 movePosition;

    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myPosition= transform.position;

        goalPosition = goal.transform.position;

        movePosition = goalPosition - myPosition;

        Debug.Log("My Position : " + myPosition);
        Debug.Log("Goal Position : " + goalPosition);
        Debug.Log("move Position : " + movePosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
