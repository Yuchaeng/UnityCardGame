using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPos;
    public GameObject stone; //enemy

    float currentTime = 0;
    float delayTime = .8f;

    int randNum = -1;

    public GameObject playerObj, playerInfo;  //���
    public Player playercs;   //���cs
    //������Ʈ -> ��ũ��Ʈ ������ ����
    public GameObject playerSpawn;

    public Text scoreText, scoreNum, hp;

    public GameObject bossObj, bossSpawn;
    bool isBossSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = Instantiate(playerObj, playerSpawn.transform.position, playerSpawn.transform.rotation);
        playercs = playerInfo.GetComponent<Player>();  //playercs�� ��⿡ ������� playerObj�� ��ũ��Ʈ ������ ������
        playercs.scoreText = scoreText;
        playercs.scoreNum = scoreNum;
        //player prefab�� ���ھ� �ؽ�Ʈ ��� null�� �׸Ŵ������� ���� ���ھ��ؽ�Ʈ�� playercs.scoreText��¼���� �־���
        playercs.myHp= hp;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (currentTime < delayTime)
            return;

        randNum = Random.Range(0, spawnPos.Length);
        GameObject enemy = Instantiate(stone, spawnPos[randNum].transform.position, spawnPos[randNum].transform.rotation);
        Rigidbody2D stoneRigid = enemy.GetComponent<Rigidbody2D>();

        Enemy enemycs = enemy.GetComponent<Enemy>();  //�����̿� �پ��ִ� enemy��ũ��Ʈ ������ (stone.GetComponent�ص� �Ǳ��� �ٵ� ���� ������ �ֵ��� ��ũ��Ʈ �ٿ��ִ°� ����)
        enemycs.playerObj = playerObj; //enemy�� �ִ� ��⿡ player���� ���� ��� �־��� -> ���� �Ѱ���, ���⼭ ������ �ø� ���� ������Ʈ�� ���� �ȳѰ��൵ �Ǳ���
        enemycs.playercs = playercs; //enemy�� �ִ� ��⽺ũ��Ʈ�� player���� ���� ��⽺ũ��Ʈ �־��� -> ���� �Ѱ���

        stoneRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        currentTime = 0;

        if(playercs.score >= 100 && isBossSpawn)
        {
            SpawnBoss();
        }

    }

    void SpawnBoss()
    {
        isBossSpawn = false;
        GameObject bossInfo = Instantiate(bossObj, bossSpawn.transform.position, bossSpawn.transform.rotation);
        bossInfo.transform.Rotate(Vector3.back * 180);

        Boss bossCs = bossInfo.GetComponent<Boss>();
        bossCs.playerObj = playerInfo;
        bossCs.playerCs = playercs;
    }

    //enemy prefab�̶� �巡�׷� ���obj ������ -> ��ũ��Ʈ �󿡼� ���� �Ѱ��ֱ�
    //1. enemy�� player ������ ���� �� �ִ� ���� ����
    //2. gamemanager���� enemy���� player������ �Ѱ���
    //3. enemy�ı��� �� score�� �ø�
}
