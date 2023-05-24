using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject playerObj;
    public Player playerCs;
    public GameObject bossBullet;
    Rigidbody2D bossBulletRigid;

    float current = 0;
    float delay = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerObj.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(-0.04f, 4.8f, 0), 3 * Time.deltaTime);

        current += Time.deltaTime;
        if(current > delay)
        {
            bossBulletRigid = Instantiate(bossBullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            bossBulletRigid.AddForce((playerObj.transform.position - transform.position).normalized * 7, ForceMode2D.Impulse);

            current = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.transform.tag == "Player")
        {
            Debug.Log("Äç");
        }
    }
}
