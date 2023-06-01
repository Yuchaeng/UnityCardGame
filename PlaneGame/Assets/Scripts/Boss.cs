using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject barrel;
    public ObjectManager objManagerInBoss;
 
    Player playerCs;

    float currentTime = 0;
    float delayTime = .8f;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, new Vector3(0, 5.17f, 0), Time.deltaTime * 3);

        if (currentTime > delayTime)
        {
            GameObject bossBullet = objManagerInBoss.SelectObj("bossBullet");
            bossBullet.transform.position = barrel.transform.position;
            Rigidbody2D bulletRigid = bossBullet.GetComponent<Rigidbody2D>();
            bulletRigid.AddForce(Vector2.down * 3, ForceMode2D.Impulse);  //(barrel.transform.position - playerCs.transform.position).normalized * 5
            currentTime = 0;

        }

    }


}
