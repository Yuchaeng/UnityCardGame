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

    public GameObject playerObj, playerInfo;  //뱅기
    public Player playercs;   //뱅기cs
    //오브젝트 -> 스크립트 순서로 접근
    public GameObject playerSpawn;

    public Text scoreText, scoreNum, hp;

    public GameObject bossObj, bossSpawn;
    bool isBossSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo = Instantiate(playerObj, playerSpawn.transform.position, playerSpawn.transform.rotation);
        playercs = playerInfo.GetComponent<Player>();  //playercs는 뱅기에 들어있음 playerObj의 스크립트 정보를 가져옴
        playercs.scoreText = scoreText;
        playercs.scoreNum = scoreNum;
        //player prefab에 스코어 텍스트 없어서 null임 겜매니저에서 만든 스코어텍스트를 playercs.scoreText어쩌구에 넣어줌
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

        Enemy enemycs = enemy.GetComponent<Enemy>();  //돌멩이에 붙어있는 enemy스크립트 가져옴 (stone.GetComponent해도 되긴함 근데 논리는 복제된 애들의 스크립트 붙여주는게 맞음)
        enemycs.playerObj = playerObj; //enemy에 있는 뱅기에 player에서 받은 뱅기 넣어줌 -> 정보 넘겨줌, 여기서 점수만 올릴 때는 오브젝트는 굳이 안넘겨줘도 되긴함
        enemycs.playercs = playercs; //enemy에 있는 뱅기스크립트에 player에서 받은 뱅기스크립트 넣어줌 -> 정보 넘겨줌

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

    //enemy prefab이라서 드래그로 뱅기obj 못넣음 -> 스크립트 상에서 정보 넘겨주기
    //1. enemy에 player 정보를 받을 수 있는 변수 선언
    //2. gamemanager에서 enemy에게 player정보를 넘겨줌
    //3. enemy파괴될 때 score를 올림
}
