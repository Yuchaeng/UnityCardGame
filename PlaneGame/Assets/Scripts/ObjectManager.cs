using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemy, playerBullet, particle;

    GameObject[] enemyArr, playerBulletArr, particleArr;

    // Start is called before the first frame update
    void Start()
    {
        enemyArr = new GameObject[30];
        playerBulletArr = new GameObject[32];
        particleArr = new GameObject[32];
        InitObj();
    }

    void InitObj()
    {
        for (int i = 0; i < enemyArr.Length; i++)
        {
            enemyArr[i] = Instantiate(enemy);
            enemyArr[i].SetActive(false); 
        }

        for (int i = 0; i < playerBulletArr.Length; i++)
        {
            playerBulletArr[i] = Instantiate(playerBullet);
            playerBulletArr[i].SetActive(false);
        }
        for (int i = 0; i < particleArr.Length; i++)
        {
            particleArr[i] = Instantiate(particle);
            particleArr[i].SetActive(false);
        }
    }

    public GameObject SelectObj(string objectName)
    {
        GameObject[] gameObjArr;
        switch (objectName)
        {
            case "enemy":
                gameObjArr = enemyArr;
                break;
            case "playerBullet":
                gameObjArr = playerBulletArr;
                break;
            case "particle":
                gameObjArr = particleArr;
                break;
            default:
                gameObjArr = null;
                break;
        }

        for (int i = 0; i < gameObjArr.Length; i++)
        {
            if (!gameObjArr[i].activeSelf)
            {
                gameObjArr[i].SetActive(true);
                return gameObjArr[i];
            }
        }
        return null;
    }




}
