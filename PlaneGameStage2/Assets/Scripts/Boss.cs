using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject playerObj;
    public Player playerCs;
    public GameObject bossBullet;
    Rigidbody2D bossBulletRigid;
    public ParticleSystem particle;

    public GameObject[] barrels;

    List<GameObject> bulletArray = new List<GameObject>();
    List<Rigidbody2D> bulletArrayRigids = new List<Rigidbody2D>();

    Rigidbody2D[] bulletRigids = new Rigidbody2D[4];

    float current = 0;
    float delay = 1;

    int patternSelect = -1;

    public float bossHp = 100;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BossPattern", 3); //3���Ŀ� BossPattern�� �����ϰڴٴ� ��, BossPattern���� �ڷ�ƾ�������
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
            bossHp--;
        }
        else if(collision.transform.tag == "Player")
        {
            Debug.Log("��");
        }
    }

    void BossPattern()
    {
        //patternSelect = 3;
        patternSelect++;

        switch (patternSelect)
        {
            case 0:
                StartCoroutine(FireCross());
                break;
            case 1:
                StartCoroutine(FireX());
                break;
            case 2:
                StartCoroutine(FireCircle());
                break;
            case 3:
                StartCoroutine(FireSin());
                patternSelect = -1;
                break;
            default:
                break;
        }
    }

    IEnumerator FireCross()
    {
        //�ѹ��� 4���� 3�� �Ѿ˽�
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                //����Ʈ�� �� �� �̷���, ���� for�� 4���� 8���� �� ����, �� Add�ϴ°Ŷ� �ܺ� for������ �� ���� ����Ʈ 4������ ��� �߰���
                bulletArray.Add(Instantiate(bossBullet, transform.position, transform.rotation));
                bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                //bulletRigids[i] = Instantiate(bossBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();  // -> �迭
                bulletArrayRigids[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(.7f); //�� ���Ŀ� �����϶�� ��

            for (int i = 0; i < 4; i++)
            {
                //bulletRigids[i].velocity = Vector2.zero;
                bulletArrayRigids[i].velocity = Vector2.zero;
            }

            bulletArrayRigids[0].AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse); //����
            bulletArrayRigids[1].AddForce(new Vector2(0, -1) * 3, ForceMode2D.Impulse); //���Ʒ�
            bulletArrayRigids[2].AddForce(new Vector2(1, 0) * 3, ForceMode2D.Impulse); //����
            bulletArrayRigids[3].AddForce(new Vector2(-1, 0) * 3, ForceMode2D.Impulse); //�޾Ʒ�

            //bulletRigids[0].AddForce(new Vector2(0, 1) * 3, ForceMode2D.Impulse);
            //bulletRigids[1].AddForce(new Vector2(0, -1) * 3, ForceMode2D.Impulse);
            //bulletRigids[2].AddForce(new Vector2(1, 0) * 3, ForceMode2D.Impulse);
            //bulletRigids[3].AddForce(new Vector2(-1, 0) * 3, ForceMode2D.Impulse);

            bulletArray.Clear();
            bulletArrayRigids.Clear();
        }
 
        Invoke("BossPattern", 1);
    }

    IEnumerator FireX()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                bulletArray.Add(Instantiate(bossBullet, transform.position, transform.rotation));
                bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                bulletArrayRigids[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);

            }
            yield return new WaitForSeconds(.8f);

            for (int i = 0; i < 4; i++)
            {
                bulletArrayRigids[i].velocity = Vector2.zero;
            }

            bulletArrayRigids[0].AddForce(new Vector2(1, 1) * 3, ForceMode2D.Impulse); //����
            bulletArrayRigids[1].AddForce(new Vector2(-1, 1) * 3, ForceMode2D.Impulse); //����
            bulletArrayRigids[2].AddForce(new Vector2(1, -1) * 3, ForceMode2D.Impulse); //���Ʒ�
            bulletArrayRigids[3].AddForce(new Vector2(-1, -1) * 3, ForceMode2D.Impulse); //�޾Ʒ�

            bulletArray.Clear();
            bulletArrayRigids.Clear();
        }

        Invoke("BossPattern", 1);

    }

    IEnumerator FireCircle()
    {
        Vector2 dir = playerObj.transform.position - transform.position;
        for (int i = 0; i < 30; i++)
        {
            bulletArray.Add(Instantiate(bossBullet, transform.position, transform.rotation));
            bulletArrayRigids.Add(bulletArray[i].GetComponent<Rigidbody2D>());
            bulletArrayRigids[i].AddForce(dir.normalized * 4, ForceMode2D.Impulse);
        }
        
        yield return new WaitForSeconds(.8f);

        for (int i = 0; i < 30; i++)
        {
            Vector2 bulletDir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / 30), Mathf.Sin(Mathf.PI * 2 * i / 30));
            bulletArrayRigids[i].velocity = Vector2.zero;
            bulletArrayRigids[i].AddForce(bulletDir.normalized * 5, ForceMode2D.Impulse);
        }

        bulletArray.Clear();
        bulletArrayRigids.Clear();
        Invoke("BossPattern", 1);
    }

    IEnumerator FireSin()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject bulletInfo1 = Instantiate(bossBullet, barrels[0].transform.position, barrels[0].transform.rotation);
            GameObject bulletInfo2 = Instantiate(bossBullet, barrels[1].transform.position, barrels[1].transform.rotation);

            Rigidbody2D bulletRigid1 = bulletInfo1.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletRigid2 = bulletInfo2.GetComponent<Rigidbody2D>();

            Vector2 bulletDir1 = new Vector2(Mathf.Sin(Mathf.PI * 3 * i/30), -1);
            Vector2 bulletDir2 = new Vector2(Mathf.Sin(Mathf.PI * 3 * i/30), -1);

            bulletRigid1.AddForce(bulletDir1.normalized * 5, ForceMode2D.Impulse);
            bulletRigid2.AddForce(bulletDir2.normalized * 5, ForceMode2D.Impulse);

            yield return new WaitForSeconds(.3f);

        }

        Invoke("BossPattern", 1);
    }

}
