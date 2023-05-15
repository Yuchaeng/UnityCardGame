using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public GameObject[] spawns;
    public float mySpeed = 10;
    Rigidbody2D myRigid;

    Vector2 inputVec;

    float fireDelay = .1f;
    float currentDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void Update()
    {
        currentDelay += Time.deltaTime;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if(currentDelay < fireDelay)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            currentDelay = 0;
        }


        


    }

    private void FixedUpdate()
    {

        inputVec = inputVec.normalized * mySpeed * Time.fixedDeltaTime;

        myRigid.MovePosition(myRigid.position + inputVec);

        

    }


}
