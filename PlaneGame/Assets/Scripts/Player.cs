using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public float mySpeed = 10;
    Rigidbody2D myRigid;

    Vector2 inputVec;

    float fireDelay = .2f; //0.2초가 상한선
    float currentDelay = 0;

    float bulletSpeed = 6;

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

        Fire();
 
    }

    private void FixedUpdate()
    {

        inputVec = inputVec.normalized * mySpeed * Time.fixedDeltaTime;

        myRigid.MovePosition(myRigid.position + inputVec);

        

    }

    void Fire()
    {
        if (currentDelay < fireDelay)
            return;

        //Instantiate(bullet, transform.position, transform.rotation); -> gameObject에 담을 수 있음
        //Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>(); -> 하나만 앞으로 나감 bullet은 instantiate해서 복제된 애들이랑 달라서 복제된 애들 getcomponent는 못가져옴
        
        Rigidbody2D bulletRigid = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);

        currentDelay = 0;
    }


}
