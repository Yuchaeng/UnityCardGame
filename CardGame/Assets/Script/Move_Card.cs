using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Card : MonoBehaviour
{
    Rigidbody2D myRigid;
    Vector3 myPosition;
    Vector3 goalPosition;
    Vector3 nextPosition;

    public GameObject goal;
  

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myPosition= transform.position;

        goalPosition = goal.transform.position;

        nextPosition = goalPosition - myPosition;

        //Debug.Log("My Position : " + myPosition);
        //Debug.Log("Goal Position : " + goalPosition);
        //Debug.Log("move Position : " + nextPosition);
        //Debug.Log("단위벡터 : " + nextPosition.normalized);
        
        //transform.position = nextPosition + myPosition;
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition = goalPosition - transform.position;
        //transform.position = transform.position + goalPosition * Time.deltaTime; //원하는 방향과 다름
        //천천히 스르륵 가게 하는거
        //transform.position = transform.position + nextPosition.normalized * Time.deltaTime;
        //goalPosition - transform.position

        //myRigid.MovePosition(transform.position + nextPosition.normalized * 5 * Time.deltaTime); //미세하기 떨림이 있음
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, 5*Time.deltaTime); //정확하게 저 좌표로 이동하게(시작,도착,속도)
    }
}
