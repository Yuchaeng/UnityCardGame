using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerChild : MonoBehaviour
{
    public Vector2 center;
    public Vector2 size;

    BoxCollider2D box;

    List<GameObject> enemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        size = box.size;
        center = box.offset;
        Debug.Log(box.size);

        if(enemy.Count > 0)
        {
            enemy.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        center = transform.position + new Vector3(0,2,0);  //center값을 업데이트해주면 플레이어 움직일 때 박스 같이 따라감
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("child 맞다");

            enemy.Add(collision.gameObject);
            for (int i = 0; i < enemy.Count; i++)
            {
                Instantiate(enemy[i], collision.transform.position, collision.transform.rotation);
                Debug.Log(enemy[i]);

            }
        }
    }
}
