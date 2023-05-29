using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigid;
    Vector2 inputVec;
    public GameObject bullet;
    Rigidbody2D bulletRigid;
    float speed = 5;

    float currentTime = 0;
    float delayTime = .2f;

    bool leftHit = false;
    bool rightHit = false;
    bool topHit = false;
    bool bottomHit = false;

    int playerHp = 100;
    public Text myHp;

    public Text scoreText, scoreNum;
    public float score = 0;

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myHp.text = playerHp.ToString();

        scoreText.text = "SCORE";
        scoreNum.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (leftHit && inputVec.x == -1)
            inputVec.x = 0;
        else if (rightHit && inputVec.x == 1)
            inputVec.x = 0;
        else if(topHit && inputVec.y == 1)
            inputVec.y = 0;
        else if(bottomHit && inputVec.y == -1)
            inputVec.y = 0;      

        Fire();

        scoreNum.text = score.ToString();
    }

    private void FixedUpdate()
    {
        //update는 너무 빨리 불려져와서 update보다 안정적인 fixedupdate 사용
        inputVec = inputVec.normalized * Time.fixedDeltaTime * speed; //0.1초마다 불려져서 걍 정규화만 하면 한번 클릭했을 때 열배로 움직이는 느낌???
        transform.position = new Vector2(transform.position.x + inputVec.x, transform.position.y + inputVec.y);
        //myRigid.MovePosition(myRigid.position + inputVec);
        //transform.position = myRigid.position + inputVec;

        float clampX = Mathf.Clamp(transform.position.x, -4, 4);
        float clampY = Mathf.Clamp(transform.position.y, -7, 7);

        transform.position = new Vector2(clampX, clampY);
    }

    void Fire()
    {
        if (currentTime < delayTime)
            return;

        bulletRigid = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletRigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        currentTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "enemy")
        {
            Destroy(collision.gameObject);
            Instantiate(particle, collision.transform.position, collision.transform.rotation);
            playerHp -= 5;
            myHp.text = playerHp.ToString();

            if(playerHp <= 0)
            {
                //Destroy(gameObject);
                myHp.text = "Die";
            }
        }

        if(collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    leftHit= true;
                    break;
                case "RightBoundary":
                    rightHit = true; break;
                case "TopBoundary":
                    topHit= true; break;
                case "BottomBoundary":
                    bottomHit= true; break;
                default:
                    break;
            }
        }

        if(collision.transform.tag == "Item")
        {
            Destroy(collision.gameObject);
            score += 150;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Boundary")
        {
            switch (collision.transform.name)
            {
                case "LeftBoundary":
                    leftHit = false;
                    break;
                case "RightBoundary":
                    rightHit = false;
                    break;
                case "TopBoundary":
                    topHit = false;
                    break;
                case "BottomBoundary":
                    bottomHit = false;
                    break;
                default:
                    break;
            }
        }
    }



}
