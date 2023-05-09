using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] card;
    public GameObject[] target;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < card.Length; i++)
        //{
        //    card[i].transform.position = Vector3.MoveTowards(card[i].transform.position, target[i].transform.position, 5 * Time.deltaTime);
        //}

        
    }

    public void GameStart()
    {
        for (int i = 0; i < card.Length; i++)
        {
            StartCoroutine(MoveCard(card[i], target[i]));
        }
    }

    //coroutine
    //update문처럼/멀티스레드처럼 움직이는 거
    //권한을 얘한테 돌려줌 yield return 하면 다시 시스템으로 돌아감 리턴 안하면 무한반복
    IEnumerator MoveCard(GameObject myCard, GameObject goal) //GameObject myCard, GameObject goal
    {
        while (timer <= 15)
        {
            timer += Time.deltaTime;
            myCard.transform.position = Vector3.MoveTowards(myCard.transform.position, goal.transform.position, 5 * Time.deltaTime);

            yield return null;
        }


        //if(timer >= 5)
        //{
        //    Debug.Log("timer가 5를 넘겼습니다.");
        //}
    }
}
