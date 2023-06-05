using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float destroyTimer = 1.8f; //상한선, 이거 되면 죽음
    float currentTimer = 0;

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
            gameObject.SetActive(false);
            currentTimer = 0;
        }
    }

    
}
