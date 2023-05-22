using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPos;
    public GameObject stone;

    float currentTime = 0;
    float delayTime = .8f;

    int randNum = -1;

    public GameObject playerObj;
    Player playercs;

    // Start is called before the first frame update
    void Start()
    {
        playercs = playerObj.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (currentTime < delayTime)
            return;

        randNum = Random.Range(0, spawnPos.Length);
        Rigidbody2D stoneRigid = Instantiate(stone, spawnPos[randNum].transform.position, spawnPos[randNum].transform.rotation).GetComponent<Rigidbody2D>();
        stoneRigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        currentTime = 0;

        playercs.score += 100;
    }
}
