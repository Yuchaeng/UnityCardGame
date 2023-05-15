using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D myRigid;
    public float bulletSpeed;

    float destroyTimer = 1; //상한선 이거 되면 죽음
    float currentTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        myRigid= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        myRigid.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);

        currentTimer += Time.deltaTime;
        if(currentTimer > destroyTimer)
        {
            Destroy(gameObject);
            currentTimer = 0;
        }
    }
}
