using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D my_rigid;
    public Vector2 input_vec;
    public Vector2 next_vec;

    public List<GameObject> enemy_arr = new List<GameObject>();

    Vector2 bullet_dir;
    public GameObject bullet;

    public float minDistance;
    public float curDistance;

    float fire_delay = 0.3f;
    float current_delay;

    public int minIdx;

    Animator my_animator;
    SpriteRenderer my_SR;


    void Start()
    {

        my_rigid= GetComponent<Rigidbody2D>();
        my_animator= GetComponent<Animator>();
        my_SR= GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        current_delay = current_delay + Time.deltaTime;

        input_vec.x = Input.GetAxisRaw("Horizontal");
        input_vec.y = Input.GetAxisRaw("Vertical");

        next_vec = input_vec.normalized;

        //Vector2 dir = enemy_arr[i].transform.position - transform.position;
        //float distance = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);


        if (enemy_arr.Count != 0)
        {
            //여기서 초기화 안해줘서 오류남
            minDistance = 999f;
            minIdx = 0;

            for (int i = 0; i < enemy_arr.Count; i++)
            {
                Debug.DrawRay(transform.position, enemy_arr[i].transform.position - transform.position, Color.red);
                curDistance  = Vector2.Distance(enemy_arr[i].transform.position, transform.position);
                if (curDistance <= minDistance)
                {
                    bullet_dir = enemy_arr[i].transform.position - transform.position;
                    bullet_dir = bullet_dir.normalized;
                    minDistance = curDistance;
                    minIdx = i;
                }
            }

            Debug.DrawRay(transform.position, enemy_arr[minIdx].transform.position - transform.position, Color.blue);

            if(current_delay > fire_delay)
            {
                Fire(bullet_dir);
                current_delay = 0;
            }



            //Debug.Log(distance);
            //Debug.Log(Vector2.Distance(transform.position, enemy_arr[0].transform.position));

        }

    }

    private void FixedUpdate()
    {
        my_rigid.MovePosition(my_rigid.position + next_vec*4 * Time.fixedDeltaTime);

        my_animator.SetFloat("speed", next_vec.magnitude);

        if (next_vec.x < 0)
        {
            my_SR.flipX= true;
        }
        else if (next_vec.x > 0)
        {
            my_SR.flipX = false;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {



        }    



    }

    void Fire(Vector2 targetDir)
    {
        GameObject bulletInfo = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigid = bulletInfo.GetComponent<Rigidbody2D>();
        rigid.AddForce(targetDir * 4, ForceMode2D.Impulse);
    }


}
