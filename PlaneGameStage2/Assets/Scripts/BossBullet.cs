using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    float currentTimer = 0;
    float destoryTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer > destoryTimer)
        {
            Destroy(gameObject);
            currentTimer= 0;
        }
    }

}
