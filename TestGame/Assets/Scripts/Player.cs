using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigid;

    Vector2 inputVec;
    Vector2 nextVec;

    Animator myAnimator;
    SpriteRenderer mySpriteRender;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        mySpriteRender= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        nextVec = inputVec.normalized;

    }

    private void FixedUpdate()
    {
       
        myRigid.MovePosition(myRigid.position + nextVec * Time.fixedDeltaTime * 4);

        myAnimator.SetFloat("speed", nextVec.magnitude);  //벡터값의 길이를 리턴

        if(nextVec.x < 0)
        {
            mySpriteRender.flipX = true;
        }
        else if(nextVec.x > 0)
        {
            mySpriteRender.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("parent 맞다");
        }
    }
}
