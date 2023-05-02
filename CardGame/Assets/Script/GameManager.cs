using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    //�׸Ŵ����� ī���� spriterenderer �޾Ƽ� �갡 ������ �̹����� ��ȯ������
    //�׸Ŵ��� ���Ѽ� ī�� ��� �ٲٰ���

    //[SerializeField] private���� ���� �� inspector â���� ������� �� ���, �ٸ� Ŭ�������� ��������� �ȵ�
    public GameObject[] card = new GameObject[6];

    public Sprite[] mySprites;

    SpriteRenderer[] cardRenderer = new SpriteRenderer[5];

    List<string> cardList = new List<string>();
    List<int> onlyNum = new List<int>();
    List<string> onlyShape = new List<string>();

    int[] randomIdx = new int[6];

    bool isFlush = false;
    bool isMount = false;
    bool isStraight = false;
    
    int num = 0;

    public Text changeText;
    public Button changeSet;
    public GameObject[] change = new GameObject[5];


    // Start is called before the first frame update
    void Start()
    {
        changeText.text = "������ �����մϴ�.";

        for (int i = 0; i < change.Length; i++)
        {
            change[i].SetActive(false);
        }

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
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeCard()
    {
        string ButtonName = EventSystem.current.currentSelectedGameObject.name;
        
        switch (int.Parse(ButtonName))
        {
            case 0:
                cardRenderer[0].sprite = mySprites[randomIdx[5]];
                break;
            case 1:
                cardRenderer[1].sprite = mySprites[randomIdx[5]];
                break;
            case 2:
                cardRenderer[2].sprite = mySprites[randomIdx[5]];
                break;
            case 3:
                cardRenderer[3].sprite = mySprites[randomIdx[5]];
                break;
            case 4:
                cardRenderer[4].sprite = mySprites[randomIdx[5]];
                break;
            default:
                break;
        }

        for (int i = 0; i < change.Length; i++)
        {
            change[i].SetActive(false);
        }

        changeSet.interactable = false;
    }

    public void setchange()
    {
        for (int i = 0; i < change.Length; i++)
        {
            change[i].SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Batting()
    {
        for (int i = 0; i < card.Length - 1; i++)
        {
            cardList.Add(cardRenderer[i].sprite.name);
            onlyNum.Add(Convert.ToInt32(cardList[i].Substring(0, 2)));
            onlyShape.Add(cardList[i].Substring(2, 1));

            Debug.Log(onlyNum[i] + ", " + onlyShape[i]);
        }

        onlyNum.Sort();
        onlyShape.Sort();

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

        changeSet.interactable = false;

    }
}
