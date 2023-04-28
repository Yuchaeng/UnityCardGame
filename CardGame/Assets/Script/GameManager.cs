using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //겜매니저가 카드의 spriterenderer 받아서 얘가 지정한 이미지로 변환시켜줌
    //겜매니저 시켜서 카드 모양 바꾸게함
    //[SerializeField] private으로 선언 후 inspector 창에서 보고싶을 때 사용, 다른 클래스에서 가져오기는 안됨
    public GameObject[] card = new GameObject[6];

    public Sprite[] mySprites;

    SpriteRenderer[] cardRenderer = new SpriteRenderer[6];

    List<string> cardList = new List<string>();
    List<int> onlyNum = new List<int>();
    List<string> onlyShape = new List<string>();

    int[] randomIdx = new int[6];

    bool isFlush = false;
    bool isMount = false;
    bool isStraight = false;
    
    int pair = 0;
    int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < randomIdx.Length; i++)
        {
            num = UnityEngine.Random.Range(0, mySprites.Length-1);
            
            for (int j = 0; j < i; j++)
            {
                if (randomIdx[j] == num)
                {
                    i--;
                }
                else
                {
                    randomIdx[i] = num;
                }
            }
        }
        for (int i = 0; i < card.Length-1; i++)
        {
            cardRenderer[i] = card[i].GetComponent<SpriteRenderer>();
            cardRenderer[i].sprite = mySprites[randomIdx[i]];
            cardList.Add(cardRenderer[i].sprite.name);
        }
        //카드를 바꾼다면
        //cardList.Remove(누른카드);
        //안바꾸면
        //cardList.RemoveAt(5);
        for (int i = 0; i < cardList.Count; i++)
        {
            onlyNum.Add(Convert.ToInt32(cardList[i].Substring(0, 2)));
            onlyShape.Add(cardList[i].Substring(2, 1));

            Debug.Log(onlyNum[i] + ", " + onlyShape[i]);
        }

        onlyNum.Sort();
        onlyShape.Sort();

        //플러쉬
        if (onlyShape[0] == onlyShape[onlyShape.Count-1])
        {
            isFlush = true;
        }

        //마운틴
        if (onlyNum[0]==06 && onlyNum[1]==10 && onlyNum[2] == 11 && onlyNum[3] == 12 && onlyNum[4] == 13)
        {
            isMount = true;
        }

        //스트레이트
        for (int i = 0; i < onlyNum.Count-1; i++)
        {
            if (onlyNum[i] + 1 == onlyNum[i+1])
            {
                isStraight = true;
            }
            else
            {
                isStraight = false;
                break;
            }
        }

        //페어
        for (int i = 0; i < onlyNum.Count-1; i++)
        {
            for (int j = i+1; j < onlyNum.Count; j++)
            {
                if (onlyNum[i] == onlyNum[j])
                {
                    pair++;
                }
            }
        }

        if (isFlush && isMount)
        {
            Debug.Log("로얄 스트레이트 플러쉬");
        }
        else if (isFlush && isStraight)
        {
            Debug.Log("스트레이트 플러쉬");
        }
        else if (pair == 6)
        {
            Debug.Log("포카드");
        }
        else if (pair == 4)
        {
            Debug.Log("풀하우스");
        }
        else if (isFlush)
        {
            Debug.Log("플러쉬");
        }
        else if (isMount)
        {
            Debug.Log("마운틴");
        }
        else if (isStraight)
        {
            Debug.Log("스트레이트");
        }
        else if (pair == 3)
        {
            Debug.Log("트리플");
        }
        else if (pair == 2)
        {
            Debug.Log("투페어");
        }
        else if (pair == 1)
        {
            Debug.Log("원페어");
        }
        else
        {
            Debug.Log("하이카드(족보 완성X)");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
