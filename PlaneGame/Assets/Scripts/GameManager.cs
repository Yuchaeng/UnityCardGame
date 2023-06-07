using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject playerHpObj;
    Slider playerHpSlider;

    GameObject playerInfo;
    GameObject enemyInfo;

    float spawnDelay = .7f;
    float currentDelay = 0;

    public Text scoreNum;
    public GameObject winText;
    public GameObject loseText;
    public GameObject restartBtn;
    public GameObject homeBtn;
    public GameObject gameWinObj;
    public GameObject gameOverObj;

    int randNum = -1;

    Enemy enemyCs;
    Player playerCs;
    Boss bossCs;

    bool isBossSpawn = false;


    private void Awake()
    {
        playerInfo = Instantiate(playerObj, playerSpawnPos.transform.position, playerSpawnPos.transform.rotation);
        playerCs = playerInfo.GetComponent<Player>();
        playerCs.objManager = objManagerInGM;   //플레이어에게 objManagerInBoss 못넣어주니까 강제로 지정해줌

        playerCs.scoreNum = scoreNum;

        playerHpSlider = playerHpObj.GetComponent<Slider>();
        playerCs.playerHpSlider = playerHpSlider;
        playerCs.playerHpObj = playerHpObj;

        playerCs.gameOverObj = gameOverObj;
        playerCs.loseText = loseText;
        playerCs.restartBtn = restartBtn;

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

        //보스 생성
        if (playerCs.score >= 100 && !isBossSpawn)
        {
            SpawnBoss();
        }

        //운석 생성
        if (currentDelay > spawnDelay)
        {
            SpawnEnemy();
            currentDelay = 0;
        }

    }


    void SpawnEnemy()
    {
        //Instantiate은 gb등 매번 동작해서 렉 심함
        //enemyInfo = Instantiate(asteroid, position[randNum].transform.position, position[randNum].transform.rotation); //그냥 transform.position하면 스크립트 달려있는 오브젝트 위치를 가리킴(여기선 게임매니저)
        enemyInfo = objManagerInGM.SelectObj("enemy");
        enemyInfo.transform.position = position[randNum].transform.position;
        enemyCs = enemyInfo.GetComponent<Enemy>();
        enemyCs.hp = 3;  //재사용할 때 체력이 다시 3으로 세팅되지않아서 다시 초기화해줌

        enemyCs.objectManager = objManagerInGM;

        enemyCs.playerObj = playerInfo;
        enemyCs.playerCs = playerCs;  //***

        Rigidbody2D asteroidRigid = enemyInfo.GetComponent<Rigidbody2D>();
        asteroidRigid.AddForce(Vector2.down * 7, ForceMode2D.Impulse);

    }

    void SpawnBoss()
    {
        isBossSpawn = true;
        GameObject bossInfo = Instantiate(bossObj, bossTargetPos.transform.position, bossTargetPos.transform.rotation);
        bossInfo.transform.Rotate(Vector3.back * 180);

        bossCs = bossInfo.GetComponent<Boss>();  //bossInfo말고 bossObj로 해서 Boss 스크립트에서 총알쏘려고할 때 널 오류남
        bossCs.objManagerInBoss = objManagerInGM;
        bossCs.playerObj = playerInfo;
        bossCs.playerCs = playerCs;

        bossCs.bossSliderObj = bossHpObj;
        bossCs.bossSlider = bossHpSlider;
        bossCs.gameWinObj = gameWinObj;
        bossCs.winText = winText;
        bossCs.restartBtn = restartBtn;
        

        bossHpObj.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Stage1"); //씬 이름으로 불러오기
        //SceneManager.LoadScene(0);  //위 방법이랑 같음
        Time.timeScale = 1;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
