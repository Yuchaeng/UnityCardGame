using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject[] position = new GameObject[4];
    GameObject astInfo;

    float spawnDelay = .7f;
    float currentDelay = 0;

    int randNum = -1;

    // Start is called before the first frame update
    void Start()
    {             
    }

    // Update is called once per frame
    void Update()
    {
        randNum = Random.Range(0, position.Length);

        currentDelay += Time.deltaTime;

        if(currentDelay > spawnDelay )
        {
            astInfo = Instantiate(asteroid, position[randNum].transform.position, position[randNum].transform.rotation); //�׳� transform.position�ϸ� ��ũ��Ʈ �޷��ִ� ������Ʈ ��ġ�� ����Ŵ(���⼱ ���ӸŴ���)
            Rigidbody2D asteroidRigid = astInfo.GetComponent<Rigidbody2D>();
            asteroidRigid.AddForce(Vector2.down * 3, ForceMode2D.Impulse);

            currentDelay = 0;
        }
        
    }

    
}
