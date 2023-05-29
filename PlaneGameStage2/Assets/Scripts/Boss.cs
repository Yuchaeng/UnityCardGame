using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject playerObj;
    public Player playerCs;
    public GameObject bossBullet;
    Rigidbody2D bossBulletRigid;
    public ParticleSystem particle;

    List<GameObject> bulletArray = new List<GameObject>();
    List<Rigidbody2D> bossBullets = new List<Rigidbody2D>();

    Rigidbody2D[] bulletRigids = new Rigidbody2D[4];

    float current = 0;
    float delay = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BossPattern", 3); //3초후에 BossPattern을 실행하겠다는 뜻, BossPattern에는 코루틴들어있음
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerObj.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-0.04f, 4.8f, 0), 3 * Time.deltaTime);

        //current += Time.deltaTime;
        //if(current > delay)
        //{
        //    bossBulletRigid = Instantiate(bossBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        //    bossBulletRigid.AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);

        //    current = 0;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
            Instantiate(particle, collision.transform.position, collision.transform.rotation);
        }
        else if(collision.transform.tag == "Player")
        {
            Debug.Log("쾅");
        }
    }

    void BossPattern()
    {
        StartCoroutine(FireCross());
    }

    IEnumerator FireCross()
    {
        //한번에 4개씩 3번 총알쏨
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                //리스트로 쓸 때 이렇게, 따로 for문 4부터 8까지 또 써줌, 걍 Add하는거라 외부 for문껴서 또 돌면 리스트 4번부터 계속 추가됨
                bulletArray.Add(Instantiate(bossBullet, transform.position, transform.rotation));
                bossBullets.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                //bulletRigids[i] = Instantiate(bossBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();  // -> 배열
                bossBullets[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(.7f); //몇 초후에 리턴하라는 뜻

            for (int i = 0; i < 4; i++)
            {
                //bulletRigids[i].velocity = Vector2.zero;
                bossBullets[i].velocity+= Vector2.zero;
            }

            bossBullets[0].AddForce((new Vector2(0, 1) + new Vector2(1,0))* 3, ForceMode2D.Impulse); //오위
            bossBullets[1].AddForce((new Vector2(0, -1) + new Vector2(1,0))* 3, ForceMode2D.Impulse); //오아래
            bossBullets[2].AddForce((new Vector2(0, 1) + new Vector2(-1,0)) * 3, ForceMode2D.Impulse); //왼위
            bossBullets[3].AddForce((new Vector2(0, -1) + new Vector2(-1,0)) * 3, ForceMode2D.Impulse); //왼아래

            //bulletRigids[0].AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse);
            //bulletRigids[1].AddForce(new Vector2(0, -1) * 3, ForceMode2D.Impulse);
            //bulletRigids[2].AddForce(new Vector2(1, 0) * 3, ForceMode2D.Impulse);
            //bulletRigids[3].AddForce(new Vector2(-1, 0) * 3, ForceMode2D.Impulse);

            bulletArray.Clear();
            bossBullets.Clear();
        }
 
        Invoke("BossPattern", 1);
    }
}
