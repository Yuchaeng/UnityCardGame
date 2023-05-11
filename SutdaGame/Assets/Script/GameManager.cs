using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject[] card;
    public GameObject[] target;

    public Sprite[] sprites;
    SpriteRenderer[] cardInfo = new SpriteRenderer[8];
    public int[] randomIndex = new int[8];

    public List<string> player0 = new List<string>();
    public List<string> computer1 = new List<string>();
    public List<string> computer2 = new List<string>();
    public List<string> computer3 = new List<string>();

    public string[] myConcat = new string[4];
    public int[] myScore = new int[4];


    bool isStart = true;
    float timer = 0;
    bool isDDaeng = false;
    bool isGuang = false;

    // Start is called before the first frame update
    void Start()
    {
        //�������� ī�� �ֱ�
        //�����ϸ� �� ī��� ���� �� ��ġ�� ��ǻ��1,2,3�� ���常 ��ġ��
        //�� ī�尡 ������ �� ������ �� ���� �ְ� �����ϰڳİ� �����
        //�����ϱ� ������ ī�� �� ��� ��� ���


        //for (int i = 0; i < randomIndex.Length; i++)
        //{
        //    int num = -1;
        //    num = UnityEngine.Random.Range(0, sprites.Length);
        //    for (int j = 0; j < i; j++)
        //    {
        //        if (randomIndex[j] == num)
        //        {
        //            i--;
        //            break;
        //        }    
        //    }
        //    randomIndex[i] = num;
        //}

        for (int i = 0; i < card.Length; i++)
        {
            cardInfo[i] = card[i].GetComponent<SpriteRenderer>();
            //cardInfo[i].sprite = sprites[i];
           
        }

        //cardInfo[0].sprite = sprites[1];
        //cardInfo[1].sprite = sprites[11];

        //cardInfo[2].sprite = sprites[2];
        //cardInfo[3].sprite = sprites[6];

        //cardInfo[4].sprite = sprites[8];
        //cardInfo[5].sprite = sprites[19];

        //cardInfo[6].sprite = sprites[13];
        //cardInfo[7].sprite = sprites[7];





    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        if(isStart)
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

    }

    public void Batting()
    {
        player0.Add(cardInfo[0].sprite.name);
        player0.Add(cardInfo[1].sprite.name);
        player0.Sort();

        computer1.Add(cardInfo[2].sprite.name);
        computer1.Add(cardInfo[3].sprite.name);
        computer1.Sort();

        computer2.Add(cardInfo[4].sprite.name);
        computer2.Add(cardInfo[5].sprite.name);
        computer2.Sort();

        computer3.Add(cardInfo[6].sprite.name);
        computer3.Add(cardInfo[7].sprite.name);
        computer3.Sort();

        myConcat[0] = player0[0] + player0[1];
        myConcat[1] = computer1[0] + computer1[1];
        myConcat[2] = computer2[0] + computer2[1];
        myConcat[3] = computer3[0] + computer3[1];


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

        //������
        for (int i = 0; i < myScore.Length; i++)
        {
            if (myScore[i] >= 200 && myScore[i] <= 280)
            {
                isDDaeng = true;
            }
        }

        if(isDDaeng)
        {
            for (int i = 0; i < myConcat.Length; i++)
            {
                if (myConcat[i] == "cg" || myConcat[i] == "cG" || myConcat[i] == "Cg" || myConcat[i] == "CG")
                {
                    myScore[i] = 410;
                }
            }
        }       

        //������
        for (int i = 0; i < myScore.Length; i++)
        {
            if (myScore[i] == 500)
            {
                isGuang= true;
            }
        }
        if (isGuang)
        {
            for (int i = 0; i < myConcat.Length; i++)
            {
                if (myConcat[i] == "dg" || myConcat[i] == "dG" || myConcat[i] == "Dg" || myConcat[i] == "DG")
                {
                    myScore[i] = 750;
                }
            }
        }

    }


    private int CountEndNum(string concat)
    {
        int sumOfIndex = 0;

        for (int i = 0; i < concat.Length; i++)
        {
            for (int j = 0; j < cardInfo.Length; j++)
            {
                if (concat[i].ToString() == cardInfo[j].sprite.name)
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

    //���� ��ųʸ� �߰��ϱ�

}
