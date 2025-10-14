using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform playerTransform;

    public float speed = 5f;
    public Vector3 velocity;
    public Vector3 lookDirection; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        playerMovement();
        lookAtMouse();
        playerTransform.position += velocity;

    }
    public void lookAtMouse()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        lookDirection = (mousePos - playerTransform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        
        playerTransform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);


    }

    public void playerMovement()
    {
        velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {

            velocity += Vector3.up * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {

            velocity += Vector3.down * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {

            velocity += Vector3.left * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {

            velocity += Vector3.right * speed * Time.deltaTime;

        }

    }

}
