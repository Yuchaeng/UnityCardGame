using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float destroyTimer = 4; //���Ѽ�, �̰� �Ǹ� ����
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
            Destroy(gameObject);
            currentTimer = 0;
        }
    }
}
