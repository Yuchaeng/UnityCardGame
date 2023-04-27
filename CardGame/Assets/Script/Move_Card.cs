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
        //Debug.Log("�������� : " + nextPosition.normalized);
        
        //transform.position = nextPosition + myPosition;
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition = goalPosition - transform.position;
        //transform.position = transform.position + goalPosition * Time.deltaTime; //���ϴ� ����� �ٸ�
        //õõ�� ������ ���� �ϴ°�
        //transform.position = transform.position + nextPosition.normalized * Time.deltaTime;
        //goalPosition - transform.position

        //myRigid.MovePosition(transform.position + nextPosition.normalized * 5 * Time.deltaTime); //�̼��ϱ� ������ ����
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, 5*Time.deltaTime); //��Ȯ�ϰ� �� ��ǥ�� �̵��ϰ�(����,����,�ӵ�)
    }
}
