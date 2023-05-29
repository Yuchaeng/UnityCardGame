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
        }
        else if(collision.transform.tag == "Player")
        {
            Debug.Log("��");
        }
    }

    void BossPattern()
    {
        StartCoroutine(FireCross());
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
                bossBullets.Add(bulletArray[i].GetComponent<Rigidbody2D>());

                //bulletRigids[i] = Instantiate(bossBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();  // -> �迭
                bossBullets[i].AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(.7f); //�� ���Ŀ� �����϶�� ��

            for (int i = 0; i < 4; i++)
            {
                //bulletRigids[i].velocity = Vector2.zero;
                bossBullets[i].velocity+= Vector2.zero;
            }

            bossBullets[0].AddForce((new Vector2(0, 1) + new Vector2(1,0))* 3, ForceMode2D.Impulse); //����
            bossBullets[1].AddForce((new Vector2(0, -1) + new Vector2(1,0))* 3, ForceMode2D.Impulse); //���Ʒ�
            bossBullets[2].AddForce((new Vector2(0, 1) + new Vector2(-1,0)) * 3, ForceMode2D.Impulse); //����
            bossBullets[3].AddForce((new Vector2(0, -1) + new Vector2(-1,0)) * 3, ForceMode2D.Impulse); //�޾Ʒ�

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
