using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    float currentTimer = 0;
    float destroyTimer = 5;


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
}
