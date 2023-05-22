using UnityEngine;

public class BackGround : MonoBehaviour
{
   
    Rigidbody2D backRigid;
    float backSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        backRigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        backRigid.MovePosition(backRigid.position + (Vector2.down * backSpeed * Time.fixedDeltaTime));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "BackGroundBoundary")
        {
            transform.position = transform.position + new Vector3(0, 25, 0);
        }
    }
}
