using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public float mySpeed = 10;
    Rigidbody2D myRigid;

    public Vector2 inputVec;

    float fireDelay = .4f; //0.2�ʰ� ���Ѽ�
    float currentDelay = 0;

    float bulletSpeed = 6;

    bool hitLeftBox = false;
    bool hitRightBox = false;
    bool hitUpBox = false;
    bool hitDownBox = false;

    int playerHp = 100;

    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        hpText.text = playerHp.ToString();      
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay += Time.deltaTime;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (hitLeftBox && inputVec.x == -1)
            inputVec.x = 0;
        
        else if (hitRightBox && inputVec.x == 1)
            inputVec.x = 0;

        else if(hitUpBox && inputVec.y == 1)
            inputVec.y = 0;

        else if(hitDownBox && inputVec.y == -1)
            inputVec.y = 0;  

        Fire();

    }

    private void FixedUpdate()
    {
        inputVec = inputVec.normalized * mySpeed * Time.fixedDeltaTime;
        myRigid.MovePosition(myRigid.position + inputVec);
    }

    void Fire()
    {
        if (currentDelay < fireDelay)
            return;

        //Instantiate(bullet, transform.position, transform.rotation); -> gameObject�� ���� �� ����
        //Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>(); -> �ϳ��� ������ ���� bullet�� instantiate�ؼ� ������ �ֵ��̶� �޶� ������ �ֵ� getcomponent�� ��������
        
        Rigidbody2D bulletRigid = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);

        currentDelay = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if(collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    hitLeftBox = true; break;
                case "RightBoundary":
                    hitRightBox= true; break;
                case "UpBoundary":
                    hitUpBox= true; break;
                case "DownBoundary":
                    hitDownBox= true; break;
                default:
                    break;
            }
        }

        if(collision.transform.tag == "enemy")
        {
            Destroy(collision.gameObject);
            playerHp -= 5;
            hpText.text = playerHp.ToString();

            if(playerHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    hitLeftBox = false; break;
                case "RightBoundary":
                    hitRightBox = false; break;
                case "UpBoundary":
                    hitUpBox = false; break;
                case "DownBoundary":
                    hitDownBox = false; break;
                default:
                    break;
            }
        }
    }

}
