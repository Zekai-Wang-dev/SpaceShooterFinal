using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform enemyTransform;
    public Transform plrTransform;

    public float speed = 2f;
    public float rotateSpeed = 2f;
    public float radarRange = 5f; 
    public Vector3 velocity;
    public Vector3 lookDirection;

    public bool detected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        fieldOfView();
        followPlayer();
        turnToPlayer();
        enemyTransform.position += velocity;

    }

    public void fieldOfView()
    {

        float distanceToPlayer = (plrTransform.position - enemyTransform.position).magnitude;
        Vector3 directionToPlayer = (plrTransform.position - enemyTransform.position).normalized;
        float dotProduct = Vector3.Dot(enemyTransform.up, directionToPlayer);

        if (distanceToPlayer < radarRange && dotProduct > 0.8f)
        {

            detected = true;


        }
        else if (distanceToPlayer > radarRange)
        {

            detected = false;

        }

    }

    public void turnToPlayer()
    {
        
        float targetAngle = Mathf.Atan2(plrTransform.position.y, plrTransform.position.x) * Mathf.Rad2Deg;
        float enemyAngle = Mathf.Atan2(enemyTransform.up.y, enemyTransform.up.x) * Mathf.Rad2Deg;
        Vector3 directionToPlayer = (plrTransform.position - enemyTransform.position).normalized;
        float dotProduct = Vector3.Dot(enemyTransform.up, directionToPlayer);

        float deltaAngle = Mathf.DeltaAngle(enemyAngle, targetAngle);

        if (detected && Mathf.Abs(dotProduct) < 1f)
        {
            enemyTransform.Rotate(new Vector3(0f, 0f, Mathf.Sign(deltaAngle) * rotateSpeed * Time.deltaTime));

        }

    }

    public void followPlayer()
    {
        velocity = Vector3.zero;

        if (detected)
        {

            if (plrTransform.position.x > enemyTransform.position.x)
            {

                velocity += Vector3.right * speed * Time.deltaTime;

            }
            if (plrTransform.position.x < enemyTransform.position.x)
            {

                velocity += Vector3.left * speed * Time.deltaTime;

            }
            if (plrTransform.position.y > enemyTransform.position.y)
            {

                velocity += Vector3.up * speed * Time.deltaTime;

            }
            if (plrTransform.position.y < enemyTransform.position.y)
            {

                velocity += Vector3.down * speed * Time.deltaTime;

            }

        }


    }
    
}
