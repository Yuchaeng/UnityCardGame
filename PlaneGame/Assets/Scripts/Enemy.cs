using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    float currentTimer = 0;
    float destroyTimer = 5;

    int hp = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer > destroyTimer)
        {
            Destroy(gameObject);
            currentTimer = 0;
        }

        transform.Rotate(Vector3.back * Time.deltaTime * 100);  //new Vector3(0, 0, 1)

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject); //gameObject : 이 스크립트가 달려있는 오브젝트

            hp--;

            if (hp <= 0)
            {
                Destroy(gameObject);
                hp = 3;
            }
        }
    }
}
