using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //�׸Ŵ����� ī���� spriterenderer �޾Ƽ� �갡 ������ �̹����� ��ȯ������
    //�׸Ŵ��� ���Ѽ� ī�� ��� �ٲٰ���

    //[SerializeField] private���� ���� �� inspector â���� ������� �� ���, �ٸ� Ŭ�������� ��������� �ȵ�
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
    
    int num = 0;

    public Text changeText;
    public Button[] change = new Button[5];

    // Start is called before the first frame update
    void Start()
    {
        changeText.text = "������ �����մϴ�.";

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
        for (int i = 0; i < card.Length - 1; i++)
        {
            cardRenderer[i] = card[i].GetComponent<SpriteRenderer>();
            cardRenderer[i].sprite = mySprites[randomIdx[i]];
            cardList.Add(cardRenderer[i].sprite.name);
        }
        
      
        for (int i = 0; i < cardList.Count; i++)
        {
            onlyNum.Add(Convert.ToInt32(cardList[i].Substring(0, 2)));
            onlyShape.Add(cardList[i].Substring(2, 1));

            Debug.Log(onlyNum[i] + ", " + onlyShape[i]);
        }

        onlyNum.Sort();
        onlyShape.Sort();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeCard()
    {
        int idx = UnityEngine.Random.Range(0, card.Length-2);
        cardRenderer[idx].sprite = mySprites[randomIdx[5]];
       
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Batting()
    {
        int pair = 0;

        //�÷���
        if (onlyShape[0] == onlyShape[onlyShape.Count - 1])
        {
            isFlush = true;
        }

        //����ƾ
        if (onlyNum[0] == 06 && onlyNum[1] == 10 && onlyNum[2] == 11 && onlyNum[3] == 12 && onlyNum[4] == 13)
        {
            isMount = true;
        }

        //��Ʈ����Ʈ
        for (int i = 0; i < onlyNum.Count - 1; i++)
        {
            if (onlyNum[i] + 1 == onlyNum[i + 1])
            {
                isStraight = true;
            }
            else
            {
                isStraight = false;
                break;
            }
        }

        //���
        for (int i = 0; i < onlyNum.Count - 1; i++)
        {
            for (int j = i + 1; j < onlyNum.Count; j++)
            {
                if (onlyNum[i] == onlyNum[j])
                {
                    pair++;
                }
            }
        }

        if (isFlush && isMount)
        {
            changeText.text = "�ξ� ��Ʈ����Ʈ �÷���";
        }
        else if (isFlush && isStraight)
        {
            changeText.text = "��Ʈ����Ʈ �÷���";
        }
        else if (pair == 6)
        {
            changeText.text = "��ī��";
        }
        else if (pair == 4)
        {
            changeText.text = "Ǯ�Ͽ콺";
        }
        else if (isFlush)
        {
            changeText.text = "�÷���";
        }
        else if (isMount)
        {
            changeText.text = "����ƾ";
        }
        else if (isStraight)
        {
            changeText.text = "��Ʈ����Ʈ";
        }
        else if (pair == 3)
        {
            changeText.text = "Ʈ����";
        }
        else if (pair == 2)
        {
            changeText.text = "�����";
        }
        else if (pair == 1)
        {
            changeText.text = "�����";
        }
        else
        {
            changeText.text = "����ī��(���� �ϼ�X)";
        }

        //if (change.interactable)
        //{
        //    change.interactable= false;
        //}
    }
}
