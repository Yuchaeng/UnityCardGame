using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject barrel;
    public ObjectManager objManagerInBoss;
    public GameObject playerObj;
    GameObject bossBullet;
    Rigidbody2D bulletRigid;

    public Player playerCs;

    List<GameObject> bullets = new List<GameObject>();
    List<Rigidbody2D> bulletsRigid = new List<Rigidbody2D>();

    int patternSelect = -1;

    float bossCurrentHp = 100;
    float bossMAxHp = 100;

    public GameObject bossSliderObj;
    public Slider bossSlider;

    public GameObject winText;
    public GameObject restartBtn;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BossPattern", 3);
        bossSlider.value = bossCurrentHp / bossMAxHp;       
    }

    // Update is called once per frame
    void Update()
    {       
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(0, 5.17f, 0), Time.deltaTime * 3);

        if (bossCurrentHp <= 0)
        {
            Destroy(gameObject);
            bossSliderObj.SetActive(false);
            winText.SetActive(true);
            restartBtn.SetActive(true);
            Time.timeScale = 0;
        }

    }

    void BossPattern()
    {
        patternSelect++;

        switch (patternSelect)
        {
            case 0:
                StartCoroutine(FireCross());
                break;
            case 1:
                StartCoroutine(FireCircle());
                patternSelect = -1;
                break;
            default:
                break;
        }
    }

    IEnumerator FireCross()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                bullets.Add(objManagerInBoss.SelectObj("bossBullet"));
                bullets[i].transform.position = barrel.transform.position;
                bulletsRigid.Add(bullets[i].GetComponent<Rigidbody2D>());

                Vector2 bulletDir = playerObj.transform.position - barrel.transform.position;

                bulletsRigid[i].AddForce(bulletDir.normalized * 5, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(.5f);

            Vector2 dir = new Vector2(0, 0);

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        dir = new Vector2(1, 0);
                        break;
                    case 1:
                        dir = new Vector2(-1, 0);
                        break;
                    case 2:
                        dir = new Vector2(0, -1);
                        break;
                    case 3:
                        dir = new Vector2(0, 1);
                        break;
                    default:
                        break;
                }

                bulletsRigid[i].velocity = Vector2.zero;
                bulletsRigid[i].AddForce(dir.normalized * 3, ForceMode2D.Impulse);
            }

            bullets.Clear();
            bulletsRigid.Clear();
        }   

        Invoke("BossPattern", 1);

    }

    IEnumerator FireCircle()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 30; i++)
            {
                bullets.Add(objManagerInBoss.SelectObj("bossBullet"));
                bullets[i].transform.position = barrel.transform.position;
                bulletsRigid.Add(bullets[i].GetComponent<Rigidbody2D>());

                Vector2 bulletDir = playerObj.transform.position - barrel.transform.position;
                bulletsRigid[i].AddForce(bulletDir.normalized * 5, ForceMode2D.Impulse);

            }
            yield return new WaitForSeconds(1);

            for (int i = 0; i < 30; i++)
            {
                Vector2 circleDir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i/30), Mathf.Sin(Mathf.PI * 2 * i/30));
                bulletsRigid[i].velocity = Vector2.zero;
                bulletsRigid[i].AddForce(circleDir.normalized * 5, ForceMode2D.Impulse);
            }


            bullets.Clear();
            bulletsRigid.Clear();

        }

        Invoke("BossPattern", 1);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerBullet")
        {
            collision.gameObject.SetActive(false);
            bossCurrentHp-=20;
            bossSlider.value = bossCurrentHp / bossMAxHp;
            
            playerCs.score += 100;
        }
    }


}
