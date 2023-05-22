using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float currentTime = 0;
    float destroyTime = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > destroyTime)
        {
            Destroy(gameObject);
            currentTime = 0;
        }
    }
}
