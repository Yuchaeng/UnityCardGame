using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject[] position = new GameObject[4];
    GameObject enemyInfo;
    public GameObject playerSpawnPos;
    public GameObject playerObj;
    public ObjectManager objManagerInGM;
    public GameObject bossObj;

    GameObject playerInfo;

    float spawnDelay = .7f;
    float currentDelay = 0;
    public Text scoreInGm;

    int randNum = -1;

    Enemy enemyCs;
    Player playerCs;
    Boss bossCs;

    private void Awake()
    {
        playerInfo = Instantiate(playerObj, playerSpawnPos.transform.position, playerSpawnPos.transform.rotation);
        playerCs = playerInfo.GetComponent<Player>();
        playerCs.objManager = objManagerInGM;   //�÷��̾�� objManager ���־��ִϱ� ������ ��������

        bossCs = bossObj.GetComponent<Boss>();

        playerCs.scoreNum = scoreInGm;
    }

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
            SpawnEnemy();
            currentDelay = 0;
        }

    }


    void SpawnEnemy()
    {
        //Instantiate�� gb�� �Ź� �����ؼ� �� ����
        //enemyInfo = Instantiate(asteroid, position[randNum].transform.position, position[randNum].transform.rotation); //�׳� transform.position�ϸ� ��ũ��Ʈ �޷��ִ� ������Ʈ ��ġ�� ����Ŵ(���⼱ ���ӸŴ���)
        enemyInfo = objManagerInGM.SelectObj("enemy");
        enemyInfo.transform.position = position[randNum].transform.position;
        enemyCs = enemyInfo.GetComponent<Enemy>();
        enemyCs.hp = 10;  //������ �� ü���� �ٽ� 3���� ���õ����ʾƼ� �ٽ� �ʱ�ȭ����

        enemyCs.objectManager = objManagerInGM;
        enemyCs.playerCs = playerCs;  //***

        Rigidbody2D asteroidRigid = enemyInfo.GetComponent<Rigidbody2D>();
        asteroidRigid.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

    }
    
}
