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
        //�������� ī�� �ֱ�
        //�����ϸ� �� ī��� ���� �� ��ġ�� ��ǻ��1,2,3�� ���常 ��ġ��
        //�� ī�尡 ������ �� ������ �� ���� �ְ� �����ϰڳİ� �����
        //�����ϱ� ������ ī�� �� ��� ��� ���

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
        //���� �ε��� �̱�
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

        //player0.Add(cardInfo[randomIndex[0]].sprite.name);  -> cardInfo�� card.getComponent�޾Ƽ� �ߴµ� �׷� ī�� �޸鸸 ��
        //ī�� �����ֱ�
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

        //���� ����ؼ� ����
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
        startMsg.text = "ī�带 �����ϼ���.";
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
                myJokbo[i] = "������ or " + jokboNameOfScore[myScore[i]];
            else if (myConcat[i] == "dg" || myConcat[i] == "dG" || myConcat[i] == "Dg" || myConcat[i] == "DG")
                myJokbo[i] = "������ or " + jokboNameOfScore[myScore[i]];
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


        //������
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
                    myJokbo[i] += "�߿��� �����̿����ϴ�.";
                }
            }
        }

        //������
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
                    myJokbo[i] += "�߿��� �����翴���ϴ�.";
                }
            }
        }

        //1�� ���
        int temp = myScore.Max();
        int index = Array.FindIndex(myScore, element => element == temp);
        switch (index)
        {
            case 0:
                result.text = $"�� player0 ��(��) {myJokbo[0]}, {myScore[0]}������ �̰���ϴ�. ��";
                break;
            case 1:
                result.text = $"�� computer1 ��(��) {myJokbo[1]}, {myScore[1]}������ �̰���ϴ�. ��";
                break;
            case 2:
                result.text = $"�� computer2 ��(��) {myJokbo[2]}, {myScore[2]}������ �̰���ϴ�. ��";
                break;
            case 3:
                result.text = $"�� computer3 ��(��) {myJokbo[3]}, {myScore[3]}������ �̰���ϴ�. ��";
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
    //update��ó��/��Ƽ������ó�� �����̴� ��
    //������ ������ ������ yield return �ϸ� �ٽ� �ý������� ���ư� ���� ���ϸ� ���ѹݺ�
    IEnumerator MoveCard(GameObject myCard, GameObject goal) //GameObject myCard, GameObject goal
    {
        while (timer <= 20)
        {
            timer += Time.deltaTime;
            myCard.transform.position = Vector3.MoveTowards(myCard.transform.position, goal.transform.position, 5 * Time.deltaTime);

            yield return null;
        }

        //Debug.Log("�ڷ�ƾ ����");
    }

    Dictionary<string, int> jokbo = new Dictionary<string, int>()
{
    {"df", 100}, {"dF", 100}, {"Df", 100}, {"DF", 100}, //����(4,6)
    {"dj", 110}, {"dJ", 110}, {"Dj", 110}, {"DJ", 110}, //���(4,10)
    {"aj", 120}, {"aJ", 120}, {"Aj", 120}, {"AJ", 120}, //���(1,10)
    {"ai", 130}, {"aI", 130}, {"Ai", 130}, {"AI", 130}, //����(1,9)
    {"ad", 140}, {"aD", 140}, {"Ad", 140}, {"AD", 140}, //����(1,4)
    {"ab", 150}, {"aB", 150}, {"Ab", 150}, {"AB", 150}, //�˸�(1,2)
    {"aA", 200}, {"bB", 210}, {"cC", 220}, {"dD", 230}, {"eE", 240}, //��
    {"fF", 250}, {"gG", 260}, {"hH", 270}, {"iI", 280}, {"jJ", 290}, //��
    {"AC", 500}, {"AH", 500}, //13����, 18����
    {"CH", 1000} //38����
};


    Dictionary<int, string> jokboNameOfScore = new Dictionary<int, string>()
{
    {10,"0��(����)"}, {15, "1��"}, {20, "2��"}, {25, "3��"}, {30, "4��"}, {35,"5��"},
    {40, "6��"}, {45, "7��"}, {50, "8��"}, {55, "9��(����)"},
    {100, "����(4,6)"}, {110, "���(4,10)"} ,{120, "���(1,10)"}, {130, "����(1,9)"},
    {140,"����(1,4)"}, {150, "�˸�(1,2)"}, {200,"1��"}, {210,"2��"}, {220, "3��"},
    {230,"4��"}, {240, "5��"}, {250, "6��"}, {260,"7��"},{270,"8��"},{280,"9��"},{290,"10��(�嶯)"},
    {500, "����"}, {1000, "38����"}, {410,"������"}, {750,"������"}
};

}
