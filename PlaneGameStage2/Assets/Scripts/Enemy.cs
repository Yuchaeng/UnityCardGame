using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    float currentTime = 0;
    float delayTime = 5;

    int enemyHp = 3;

    public GameObject playerObj; //뱅기
    public Player playercs;
    public ParticleSystem particle;
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > delayTime)
        {
            Destroy(gameObject);
            currentTime = 0;
        }

        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * 80);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
            Instantiate(particle, collision.transform.position, collision.transform.rotation);          
            enemyHp--;

            if(enemyHp <= 0)
            {   Destroy(gameObject);

                float random = Random.Range(0, 1.0f);
                if(random > 0.6f)
                {
                    Instantiate(item, collision.transform.position, collision.transform.rotation);
                }
                Debug.Log(random);

                playercs.score += 100;  //GameManager에서 뱅기 정보 받았으므로 점수 올릴 수 있음
            }
        }
    }
}
