using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] card;
    public GameObject[] target;

    public Sprite[] sprites;
    SpriteRenderer[] spRenderer = new SpriteRenderer[8];

    string[] myConcat = new string[4];

    bool isStart = true;

    int num = -1;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Ä«µå ¼¯±â
        //for (int i = 0; i < sprites.Length; i++)
        //{
        //    Sprite temp;
        //    num = UnityEngine.Random.Range(0, sprites.Length);
        //    temp = sprites[i];
        //    sprites[i] = sprites[num];
        //    sprites[num] = temp;
        //}
  
        for (int i = 0; i < card.Length; i++)
        {
            spRenderer[i] = card[i].GetComponent<SpriteRenderer>();
            //spRenderer[i].sprite = sprites[i];
            //Debug.Log(sprites[i].name);
        }

        spRenderer[0].sprite = sprites[1];
        spRenderer[1].sprite = sprites[11];

        spRenderer[2].sprite = sprites[2];
        spRenderer[3].sprite = sprites[6];

        spRenderer[4].sprite = sprites[8];
        spRenderer[5].sprite = sprites[19];

        spRenderer[6].sprite = sprites[13];
        spRenderer[7].sprite = sprites[7];





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

                if (i == 0 || i == 1)
                    card[i].transform.Rotate(Vector3.back * 270);
                else if (i == 2 || i == 3)
                    card[i].transform.Rotate(Vector3.back * 180);
                else if (i == 4 || i == 5)
                    card[i].transform.Rotate(Vector3.back * 90);
            }
        }

    }

    public void Batting()
    {
        for (int i = 0; i < myConcat.Length; i++)
        {
            myConcat[i] = spRenderer[i * 2].sprite.name + spRenderer[i * 2 + 1].sprite.name;
        }
        

    }

    

    public void CardRotate()
    {
        //card[4].transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        //card[5].transform.Rotate(Vector3.back * 90);

    }

    //coroutine
    //update¹®Ã³·³/¸ÖÆ¼½º·¹µåÃ³·³ ¿òÁ÷ÀÌ´Â °Å
    //±ÇÇÑÀ» ¾êÇÑÅ× µ¹·ÁÁÜ yield return ÇÏ¸é ´Ù½Ã ½Ã½ºÅÛÀ¸·Î µ¹¾Æ°¨ ¸®ÅÏ ¾ÈÇÏ¸é ¹«ÇÑ¹Ýº¹
    IEnumerator MoveCard(GameObject myCard, GameObject goal) //GameObject myCard, GameObject goal
    {
        while (timer <= 20)
        {
            timer += Time.deltaTime;
            myCard.transform.position = Vector3.MoveTowards(myCard.transform.position, goal.transform.position, 5 * Time.deltaTime);

            yield return null;
        }

        Debug.Log("ÄÚ·çÆ¾ Á¾·á");
    }

    Dictionary<string, int> jokbo = new Dictionary<string, int>()
{
    {"df", 100}, {"dF", 100}, {"Df", 100}, {"DF", 100}, //¼¼·ú(4,6)
    {"dj", 110}, {"dJ", 110}, {"Dj", 110}, {"DJ", 110}, //Àå»ç(4,10)
    {"aj", 120}, {"aJ", 120}, {"Aj", 120}, {"AJ", 120}, //Àå»æ(1,10)
    {"ai", 130}, {"aI", 130}, {"Ai", 130}, {"AI", 130}, //±¸»æ(1,9)
    {"ad", 140}, {"aD", 140}, {"Ad", 140}, {"AD", 140}, //µ¶»ç(1,4)
    {"ab", 150}, {"aB", 150}, {"Ab", 150}, {"AB", 150}, //¾Ë¸®(1,2)
    {"aA", 200}, {"bB", 210}, {"cC", 220}, {"dD", 230}, {"eE", 240}, //¶¯
    {"fF", 250}, {"gG", 260}, {"hH", 270}, {"iI", 280}, {"jJ", 290}, //¶¯
    {"AC", 500}, {"AH", 500}, //13±¤¶¯, 18±¤¶¯
    {"CH", 1000} //38±¤¶¯
};

    //²ý¼ö µñ¼Å³Ê¸® Ãß°¡ÇÏ±â

}
