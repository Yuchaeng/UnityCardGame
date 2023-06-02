using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Rigidbody2D backRigid;

    // Start is called before the first frame update
    void Start()
    {
        backRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        backRigid.AddForce(Vector2.down * Time.fixedDeltaTime * 50);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "BackGroundBoundary")
        {
            //backRigid.MovePosition(new Vector2(0, 17));
            backRigid.transform.position = backRigid.transform.position + new Vector3(0, 39, 0);
        }
    }
}
