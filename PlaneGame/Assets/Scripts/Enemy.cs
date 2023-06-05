using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
    float currentTimer = 0;
    float destroyTimer = 10;

    public int hp = 3;
    public GameObject particle;
    public ObjectManager objectManager;

    public GameObject playerObj;
    public Player playerCs;

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

        transform.Rotate(Vector3.back * Time.deltaTime * 100);  //new Vector3(0, 0, 1)

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
            //Destroy(gameObject); //gameObject : �� ��ũ��Ʈ�� �޷��ִ� ������Ʈ
            particle = objectManager.SelectObj("particle");
            particle.transform.position = collision.transform.position;
            //particle.SetActive(true); //��� objManagerInBoss �Ȱ�ġ�� �Ѱ�

            hp--;

            if (hp <= 0)
            {
                //������ �� Ÿ�̸� �ʱ�ȭ �ȵż� Ÿ�̸� 0���� ��������
                currentTimer= 0;
                gameObject.SetActive(false);
                playerCs.score += 100;
                //Debug.Log(playerCs.score);
                //hp = 3;

            }
        }
    }

}
