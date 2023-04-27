using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�׸Ŵ����� ī���� spriterenderer �޾Ƽ� �갡 ������ �̹����� ��ȯ������
    //�׸Ŵ��� ���Ѽ� ī�� ��� �ٲٰ���
    public GameObject[] card = new GameObject[5];

    public Sprite[] mySprites = new Sprite[5];

    SpriteRenderer[] cardRenderer = new SpriteRenderer[5];

    List<string> cardList = new List<string>();
    List<int> onlyNum = new List<int>();
    List<string> onlyShape = new List<string>();

    bool isFlush = false;
    bool isMount = false;
    bool isStraight = false;

    int pair = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < card.Length; i++)
        {
            cardRenderer[i] = card[i].GetComponent<SpriteRenderer>();
            cardRenderer[i].sprite = mySprites[i];

            cardList.Add(cardRenderer[i].sprite.name);
            onlyNum.Add(Convert.ToInt32(cardList[i].Substring(0, 2)));
            onlyShape.Add(cardList[i].Substring(2, 1));

            Debug.Log(onlyNum[i] + ", " + onlyShape[i]);
        }

        onlyNum.Sort();
        onlyShape.Sort();

        //�÷���
        if (onlyShape[0] == onlyShape[onlyShape.Count-1])
        {
            isFlush = true;
            Debug.Log("flush");
        }

        //����ƾ
        if (onlyNum[0]==06 && onlyNum[1]==10 && onlyNum[2] == 11 && onlyNum[3] == 12 && onlyNum[4] == 13)
        {
            isMount = true;
            Debug.Log("Mountain");
        }

        //��Ʈ����Ʈ
        for (int i = 0; i < onlyNum.Count-1; i++)
        {
            for (int j = 1; j < onlyNum.Count; j++)
            {
                if (onlyNum[i] + 1 == onlyNum[j])
                {
                    continue;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
