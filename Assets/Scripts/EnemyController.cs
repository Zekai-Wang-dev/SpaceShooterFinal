using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public class EnemyController : MonoBehaviour
{

    public List<Vector3> size = new List<Vector3>();
    public List<Vector3> points = new List<Vector3>();

    public Transform enemyTransform;
    public Transform plrTransform;

    public Vector3 direction;
    public float range;

    public bool blockedTL;
    public bool blockedTR;
    public bool blockedBL;
    public bool blockedBR;
    public bool blockedL;
    public bool blockedR;
    public bool blockedUp;
    public bool blockedDown;


    public float speed = 2f;
    public float rotateSpeed = 2f;
    public float radarRange = 5f; 
    public Vector3 velocity;
    public Vector3 lookDirection;

    public float collisionRange = 5f; 

    public bool detected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        points.Add(new Vector3(0, 0, 0));
        size.Add(new Vector3(5, 5, 0));

    }

    // Update is called once per frame
    void Update()
    {

        fieldOfView();
        followPlayer();
        turnToPlayer();
        FindPath();
        enemyTransform.position += velocity;

    }

    public void FindPath()
    {

        string enemyLocation = "nil";

        direction = (plrTransform.position - enemyTransform.position).normalized;

        for (int i = 0; i < points.Count; i++)
        {

            Vector3 bottomLeft = points[i];
            Vector3 bottomRight = new Vector3(points[i].x + size[i].x, bottomLeft.y, 0);
            Vector3 topRight = new Vector3(bottomRight.x, bottomLeft.y + size[i].y, 0);
            Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, 0);

            float distanceBL = (bottomLeft - enemyTransform.position).magnitude;
            float distanceBR = (bottomRight - enemyTransform.position).magnitude;
            float distanceTL = (topRight - enemyTransform.position).magnitude;
            float distanceTR = (topLeft - enemyTransform.position).magnitude;


            Debug.DrawLine(bottomLeft, bottomRight);
            Debug.DrawLine(bottomRight, topRight);
            Debug.DrawLine(topRight, topLeft);
            Debug.DrawLine(topLeft, bottomLeft);

            if (enemyTransform.position.x > bottomRight.x && enemyTransform.position.y < topRight.y && enemyTransform.position.y > bottomRight.y)
            {

                enemyLocation = "right";

            }
            else if (enemyTransform.position.x < bottomLeft.x && enemyTransform.position.y < topLeft.y && enemyTransform.position.y > bottomLeft.y)
            {

                enemyLocation = "left";

            }
            else if (enemyTransform.position.y > topLeft.y && enemyTransform.position.x < topRight.x && enemyTransform.position.x > topLeft.x)
            {

                enemyLocation = "up";

            }
            else if (enemyTransform.position.y < bottomLeft.y && enemyTransform.position.x < bottomRight.x && enemyTransform.position.x > bottomLeft.x)
            {

                enemyLocation = "down";

            }
            else
            {

                enemyLocation = "inside";

            }

            if (enemyLocation == "left" && velocity.x > 0 && (distanceTL < collisionRange || distanceBL < collisionRange))
            {

                velocity.x = 0;

            }

            else if (enemyLocation == "right" && velocity.x < 0 && (distanceTR < collisionRange || distanceBR < collisionRange))
            {

                velocity.x = 0;

            }

            else if (enemyLocation == "down" && velocity.y > 0 && (distanceBL < collisionRange || distanceBR < collisionRange))
            {

                velocity.y = 0; 

            }

            else if (enemyLocation == "up" && velocity.y < 0 && (distanceTL < collisionRange || distanceTR < collisionRange))
            {

                velocity.y = 0;

            }

            print(enemyLocation);


        }

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
