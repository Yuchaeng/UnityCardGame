using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject target; //플레이어 -> 얘 따라가게 할 것

    public Vector2 radius;
    public Vector2 cubeSize;

    float camHeight, camWidth;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        Debug.Log(camHeight);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(radius, cubeSize);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);

        float camBoundaryX = cubeSize.x * 0.5f - camWidth;
        float camBoundaryY = cubeSize.y * 0.5f - camHeight;

        //특정값보다는 작게 안만들고 특정값보다는 크게 안만들겠다
        float clampX = Mathf.Clamp(transform.position.x, -camBoundaryX + radius.x, camBoundaryX + radius.x);
        float clampY = Mathf.Clamp(transform.position.y, -camBoundaryY + radius.y, camBoundaryY + radius.y);

        transform.position = new Vector3(clampX, clampY, -10);


    }
}
