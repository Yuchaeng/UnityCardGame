using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject[] card;
    public GameObject[] target;

    public Sprite[] sprites;
    SpriteRenderer[] cardInfo = new SpriteRenderer[8];
    public int[] randomIndex = new int[8];

    List<string> player0 = new List<string>();
    List<string> computer1 = new List<string>();
    List<string> computer2 = new List<string>();
    List<string> computer3 = new List<string>();

    List<string> cardList = new List<string>();
    public string[] myConcat = new string[4];
    public int[] myScore = new int[4];
    string[] myJokbo = new string[4];

    bool isStart = true;
    float timer = 0;
    bool isDDaeng = false;
    bool isGuang = false;

    public Text infoText, myCardIs, battingMsg, startMsg, result;
    public Button gameStart, batting, openCard;
    public Text[] jokboName = new Text[4];

    // Start is called before the first frame update
    void Start()
    {
        //랜덤으로 카드 주기
        //시작하면 내 카드는 두장 다 펼치고 컴퓨터1,2,3은 한장만 펼치기
        //내 카드가 땡인지 몇 끗인지 등 정보 주고 배팅하겠냐고 물어보기
        //배팅하기 누르면 카드 다 까고 결과 출력

        myCardIs.GetComponent<UnityEngine.UI.Text>().enabled = false;
        infoText.GetComponent<UnityEngine.UI.Text>().enabled = false;
        battingMsg.GetComponent<UnityEngine.UI.Text>().enabled = false;     
        startMsg.GetComponent<UnityEngine.UI.Text>().enabled = false;
        result.GetComponent<UnityEngine.UI.Text>().enabled = false;

        batting.GetComponent<UnityEngine.UI.Button>().interactable = false;
        openCard.GetComponent<UnityEngine.UI.Button>().interactable = false;

        for (int i = 0; i < card.Length; i++)
        {
            cardInfo[i] = card[i].GetComponent<SpriteRenderer>();
            //cardInfo[i].sprite = sprites[i]; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        //랜덤 인덱스 뽑기
        for (int i = 0; i < randomIndex.Length; i++)
        {
            int num = -1;
            num = UnityEngine.Random.Range(0, cardInfo.Length);
            randomIndex[i] = num;

            for (int j = 0; j < i; j++)
            {
                if (randomIndex[j] == num)
                {
                    randomIndex[i] = -1;
                    i--;
                    break;
                }
            }
        }

        //player0.Add(cardInfo[randomIndex[0]].sprite.name);  -> cardInfo에 card.getComponent받아서 했는데 그럼 카드 뒷면만 들어감
        //카드 나눠주기
        player0.Add(sprites[randomIndex[0]].name);
        player0.Add(sprites[randomIndex[1]].name);
        player0.Sort();

        computer1.Add(sprites[randomIndex[2]].name);
        computer1.Add(sprites[randomIndex[3]].name);
        computer1.Sort();

        computer2.Add(sprites[randomIndex[4]].name);
        computer2.Add(sprites[randomIndex[5]].name);
        computer2.Sort();

        computer3.Add(sprites[randomIndex[6]].name);
        computer3.Add(sprites[randomIndex[7]].name);
        computer3.Sort();

        myConcat[0] = player0[0] + player0[1];
        myConcat[1] = computer1[0] + computer1[1];
        myConcat[2] = computer2[0] + computer2[1];
        myConcat[3] = computer3[0] + computer3[1];

        //점수 계산해서 저장
        for (int i = 0; i < myScore.Length; i++)
        {
            if (jokbo.ContainsKey(myConcat[i]))
            {
                myScore[i] = jokbo[myConcat[i]];
            }
            else
            {
                myScore[i] = CountEndNum(myConcat[i]);
            }
        }

        if (isStart)
        {
            isStart = false;

            for (int i = 0; i < card.Length; i++)
            {
                StartCoroutine(MoveCard(card[i], target[i]));

                if (i == 6 || i == 7)
                    card[i].transform.Rotate(Vector3.back * 270);
                else if (i == 4 || i == 5)
                    card[i].transform.Rotate(Vector3.back * 180);
                else if (i == 2 || i == 3)
                    card[i].transform.Rotate(Vector3.back * 90);
            }
        }
        startMsg.text = "카드를 오픈하세요.";
        startMsg.GetComponent<UnityEngine.UI.Text>().enabled = true;

        openCard.GetComponent<UnityEngine.UI.Button>().interactable = true;

    }

    public void OpenCard()
    {
        startMsg.GetComponent<UnityEngine.UI.Text>().enabled = false;
        batting.GetComponent<UnityEngine.UI.Button>().interactable = true;

        cardInfo[0].sprite = sprites[randomIndex[0]];
        cardInfo[1].sprite = sprites[randomIndex[1]];
        cardInfo[2].sprite = sprites[randomIndex[2]];
        cardInfo[4].sprite = sprites[randomIndex[4]];
        cardInfo[6].sprite = sprites[randomIndex[6]];

        myCardIs.GetComponent<UnityEngine.UI.Text>().enabled = true;
        infoText.GetComponent<UnityEngine.UI.Text>().enabled = true;
        battingMsg.GetComponent<UnityEngine.UI.Text>().enabled = true;

        for (int i = 0; i < myJokbo.Length; i++)
        {
            if (myConcat[i] == "AC")
                myJokbo[i] = "13" + jokboNameOfScore[myScore[i]];
            else if (myConcat[i] == "AH")
                myJokbo[i] = "18" + jokboNameOfScore[myScore[i]];
            else if (myConcat[i] == "cg" || myConcat[i] == "cG" || myConcat[i] == "Cg" || myConcat[0] == "CG")
                myJokbo[i] = "땡잡이 or " + jokboNameOfScore[myScore[i]];
            else if (myConcat[i] == "dg" || myConcat[i] == "dG" || myConcat[i] == "Dg" || myConcat[i] == "DG")
                myJokbo[i] = "암행어사 or " + jokboNameOfScore[myScore[i]];
            else
                myJokbo[i] = jokboNameOfScore[myScore[i]];
        }

        infoText.text = myJokbo[0];
    }

    public void Batting()
    {
        cardInfo[3].sprite = sprites[randomIndex[3]];
        cardInfo[5].sprite = sprites[randomIndex[5]];
        cardInfo[7].sprite = sprites[randomIndex[7]];


        //땡잡이
        for (int i = 0; i < myScore.Length; i++)
        {
            if (myScore[i] >= 200 && myScore[i] <= 280)
            {
                isDDaeng = true;
            }
        }

        if (isDDaeng)
        {
            for (int i = 0; i < myConcat.Length; i++)
            {
                if (myConcat[i] == "cg" || myConcat[i] == "cG" || myConcat[i] == "Cg" || myConcat[i] == "CG")
                {
                    myScore[i] = 410;
                    myJokbo[i] += "중에서 땡잡이였습니다.";
                }
            }
        }

        //암행어사
        for (int i = 0; i < myScore.Length; i++)
        {
            if (myScore[i] == 500)
            {
                isGuang = true;
            }
        }
        if (isGuang)
        {
            for (int i = 0; i < myConcat.Length; i++)
            {
                if (myConcat[i] == "dg" || myConcat[i] == "dG" || myConcat[i] == "Dg" || myConcat[i] == "DG")
                {
                    myScore[i] = 750;
                    myJokbo[i] += "중에서 암행어사였습니다.";
                }
            }
        }

        //1등 계산
        int temp = myScore.Max();
        int index = Array.FindIndex(myScore, element => element == temp);
        switch (index)
        {
            case 0:
                result.text = $"★ player0 이(가) {myJokbo[0]}, {myScore[0]}점으로 이겼습니다. ★";
                break;
            case 1:
                result.text = $"★ computer1 이(가) {myJokbo[1]}, {myScore[1]}점으로 이겼습니다. ★";
                break;
            case 2:
                result.text = $"★ computer2 이(가) {myJokbo[2]}, {myScore[2]}점으로 이겼습니다. ★";
                break;
            case 3:
                result.text = $"★ computer3 이(가) {myJokbo[3]}, {myScore[3]}점으로 이겼습니다. ★";
                break;
            default:
                break;
        }

        for (int i = 0; i < jokboName.Length; i++)
        {
            jokboName[i].text = myJokbo[i];
        }

        myCardIs.GetComponent<UnityEngine.UI.Text>().enabled = false;
        infoText.GetComponent<UnityEngine.UI.Text>().enabled = false;
        battingMsg.GetComponent<UnityEngine.UI.Text>().enabled = false;
        startMsg.GetComponent<UnityEngine.UI.Text>().enabled = false;
        result.GetComponent<UnityEngine.UI.Text>().enabled = true;


    }
    


    private int CountEndNum(string concat)
    {
        int sumOfIndex = 0;

        for (int i = 0; i < concat.Length; i++)
        {
            for (int j = 0; j < sprites.Length; j++)
            {
                if (concat[i].ToString() == sprites[j].name)
                {
                    sumOfIndex += j;
                    break;
                }
            }
        }
        

        int endNum = (sumOfIndex + 2) % 10;

        switch (endNum)
        {
            case 0:
                return 10;
            case 1:
                return 15;
            case 2:
                return 20;
            case 3:
                return 25;
            case 4:
                return 30;
            case 5:
                return 35;
            case 6:
                return 40;
            case 7:
                return 45;
            case 8:
                return 50;
            case 9:
                return 55;
            default:
                return -1;
        }

    }


    //public void CardRotate()
    //{
    //    card[4].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
    //    card[5].transform.Rotate(Vector3.back * 90);
    //}


    //coroutine
    //update문처럼/멀티스레드처럼 움직이는 거
    //권한을 얘한테 돌려줌 yield return 하면 다시 시스템으로 돌아감 리턴 안하면 무한반복
    IEnumerator MoveCard(GameObject myCard, GameObject goal) //GameObject myCard, GameObject goal
    {
        while (timer <= 20)
        {
            timer += Time.deltaTime;
            myCard.transform.position = Vector3.MoveTowards(myCard.transform.position, goal.transform.position, 5 * Time.deltaTime);

            yield return null;
        }

        //Debug.Log("코루틴 종료");
    }

    Dictionary<string, int> jokbo = new Dictionary<string, int>()
{
    {"df", 100}, {"dF", 100}, {"Df", 100}, {"DF", 100}, //세륙(4,6)
    {"dj", 110}, {"dJ", 110}, {"Dj", 110}, {"DJ", 110}, //장사(4,10)
    {"aj", 120}, {"aJ", 120}, {"Aj", 120}, {"AJ", 120}, //장삥(1,10)
    {"ai", 130}, {"aI", 130}, {"Ai", 130}, {"AI", 130}, //구삥(1,9)
    {"ad", 140}, {"aD", 140}, {"Ad", 140}, {"AD", 140}, //독사(1,4)
    {"ab", 150}, {"aB", 150}, {"Ab", 150}, {"AB", 150}, //알리(1,2)
    {"aA", 200}, {"bB", 210}, {"cC", 220}, {"dD", 230}, {"eE", 240}, //땡
    {"fF", 250}, {"gG", 260}, {"hH", 270}, {"iI", 280}, {"jJ", 290}, //땡
    {"AC", 500}, {"AH", 500}, //13광땡, 18광땡
    {"CH", 1000} //38광땡
};


    Dictionary<int, string> jokboNameOfScore = new Dictionary<int, string>()
{
    {10,"0끗(망통)"}, {15, "1끗"}, {20, "2끗"}, {25, "3끗"}, {30, "4끗"}, {35,"5끗"},
    {40, "6끗"}, {45, "7끗"}, {50, "8끗"}, {55, "9끗(갑오)"},
    {100, "세륙(4,6)"}, {110, "장사(4,10)"} ,{120, "장삥(1,10)"}, {130, "구삥(1,9)"},
    {140,"독사(1,4)"}, {150, "알리(1,2)"}, {200,"1땡"}, {210,"2땡"}, {220, "3땡"},
    {230,"4땡"}, {240, "5땡"}, {250, "6땡"}, {260,"7땡"},{270,"8땡"},{280,"9땡"},{290,"10땡(장땡)"},
    {500, "광땡"}, {1000, "38광땡"}, {410,"땡잡이"}, {750,"암행어사"}
};

}
