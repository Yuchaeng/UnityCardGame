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
    public List<float> distances;
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
        input_vec.x = Input.GetAxisRaw("Horizontal");
        input_vec.y = Input.GetAxisRaw("Vertical");

        next_vec = input_vec.normalized;
        
        if(enemy_arr.Count != 0)
        {
            for (int i = 0; i < enemy_arr.Count; i++)
            {
                Debug.DrawRay(transform.position, enemy_arr[i].transform.position - transform.position, Color.red);
                Vector2 dir = enemy_arr[i].transform.position - transform.position;
                float distance = Mathf.Sqrt(dir.x * dir.x + dir.y * dir.y);

                distances.Add(distance);
            }

            for (int i = 0; i < enemy_arr.Count; i++)
            {
                int index = distances.IndexOf(distances.Min());
                
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


}
