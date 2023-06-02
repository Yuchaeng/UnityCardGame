using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject[] position = new GameObject[4];
    public GameObject playerSpawnPos;
    public GameObject playerObj;
    public ObjectManager objManagerInGM;
    public GameObject bossObj, bossTargetPos;
    public GameObject bossHpObj;
    Slider bossHpSlider;

    GameObject playerInfo;
    GameObject enemyInfo;

    float spawnDelay = .7f;
    float currentDelay = 0;

    public Text scoreNum;

    int randNum = -1;

    Enemy enemyCs;
    Player playerCs;
    Boss bossCs;

    bool isBossSpawn = false;
    

    private void Awake()
    {
        playerInfo = Instantiate(playerObj, playerSpawnPos.transform.position, playerSpawnPos.transform.rotation);
        playerCs = playerInfo.GetComponent<Player>();
        playerCs.objManager = objManagerInGM;   //�÷��̾�� objManagerInBoss ���־��ִϱ� ������ ��������

        playerCs.scoreNum = scoreNum;

        bossHpSlider = bossHpObj.GetComponent<Slider>();
        bossHpObj.SetActive(false);
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

        //���� ����
        if (currentDelay > spawnDelay && !isBossSpawn)
        {
            SpawnBoss();
        }

        //� ����
        if (currentDelay > spawnDelay )
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
        enemyCs.hp = 3;  //������ �� ü���� �ٽ� 3���� ���õ����ʾƼ� �ٽ� �ʱ�ȭ����

        enemyCs.objectManager = objManagerInGM;

        enemyCs.playerObj = playerInfo;
        enemyCs.playerCs = playerCs;  //***

        Rigidbody2D asteroidRigid = enemyInfo.GetComponent<Rigidbody2D>();
        asteroidRigid.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

    }

    void SpawnBoss()
    {
        isBossSpawn= true;
        GameObject bossInfo = Instantiate(bossObj, bossTargetPos.transform.position, bossTargetPos.transform.rotation);
        bossInfo.transform.Rotate(Vector3.back * 180);

        bossCs = bossInfo.GetComponent<Boss>();  //bossInfo���� bossObj�� �ؼ� Boss ��ũ��Ʈ���� �Ѿ˽������ �� �� ������
        bossCs.objManagerInBoss = objManagerInGM;
        bossCs.playerObj = playerInfo;
        bossCs.playerCs = playerCs;
        bossCs.bossSlider = bossHpSlider;

        bossHpObj.SetActive(true);
    }

}
