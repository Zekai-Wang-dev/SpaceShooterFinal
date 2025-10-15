using UnityEngine;
using System.Collections.Generic; 

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
        //followPlayer();
        turnToPlayer();
        FindPath();
        enemyTransform.position += velocity;

    }

    public void FindPath()
    {

        string playerLocation = "nil";
        string enemyLocation = "nil";

        direction = (plrTransform.position - enemyTransform.position).normalized;

        for (int i = 0; i < points.Count; i++)
        {

            Vector3 bottomLeft = points[i];
            Vector3 bottomRight = new Vector3(points[i].x + size[i].x, bottomLeft.y, 0);
            Vector3 topRight = new Vector3(bottomRight.x, bottomLeft.y + size[i].y, 0);
            Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, 0);

            Debug.DrawLine(bottomLeft, bottomRight);
            Debug.DrawLine(bottomRight, topRight);
            Debug.DrawLine(topRight, topLeft);
            Debug.DrawLine(topLeft, bottomLeft);

            if (plrTransform.position.x < bottomLeft.x && plrTransform.position.y < bottomLeft.y)
            {

                playerLocation = "bottomLeft";

            }
            else if (plrTransform.position.x > bottomRight.x && plrTransform.position.y < bottomRight.y)
            {

                playerLocation = "bottomRight";

            }
            else if (plrTransform.position.x > topRight.x && plrTransform.position.y > topRight.y)
            {

                playerLocation = "topRight";

            }
            else if (plrTransform.position.x < topLeft.x && plrTransform.position.y > topLeft.y)
            {

                playerLocation = "topLeft";

            }
            else if (plrTransform.position.x > bottomRight.x && plrTransform.position.y < topRight.y && plrTransform.position.y > bottomRight.y)
            {

                playerLocation = "right";

            }
            else if (plrTransform.position.x < bottomLeft.x && plrTransform.position.y < topLeft.y && plrTransform.position.y > bottomLeft.y)
            {

                playerLocation = "left";

            }
            else if (plrTransform.position.y > topLeft.y && plrTransform.position.x < topRight.x && plrTransform.position.x > topLeft.x)
            {

                playerLocation = "up";

            }
            else if (plrTransform.position.y < bottomLeft.y && plrTransform.position.x < bottomRight.x && plrTransform.position.x > bottomLeft.x)
            {

                playerLocation = "down";

            }
            else
            {

                playerLocation = "inside";

            }

            if (enemyTransform.position.x < bottomLeft.x && enemyTransform.position.y < bottomLeft.y)
            {

                enemyLocation = "bottomLeft";

            }
            else if (enemyTransform.position.x > bottomRight.x && enemyTransform.position.y < bottomRight.y)
            {

                enemyLocation = "bottomRight";

            }
            else if (enemyTransform.position.x > topRight.x && enemyTransform.position.y > topRight.y)
            {

                enemyLocation = "topRight";

            }
            else if (enemyTransform.position.x < topLeft.x && enemyTransform.position.y > topLeft.y)
            {

                enemyLocation = "topLeft";

            }
            else if (enemyTransform.position.x > bottomRight.x && enemyTransform.position.y < topRight.y && enemyTransform.position.y > bottomRight.y)
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

            if (playerLocation != "inside" || enemyLocation != "inside")
            {

                if (playerLocation == "bottomLeft" && (enemyLocation == "topRight" || enemyLocation == "right" || enemyLocation == "up"))
                {

                    blockedTL = true;
                    blockedTR = false;
                    blockedBL = false;
                    blockedBR = false;
                    blockedL = false;
                    blockedR = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "bottomRight" && (enemyLocation == "topLeft" || enemyLocation == "left" || enemyLocation == "up"))
                {

                    blockedTR = true;
                    blockedTL = false;
                    blockedBL = false;
                    blockedBR = false;
                    blockedL = false;
                    blockedR = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "topRight" && (enemyLocation == "bottomLeft" || enemyLocation == "left" || enemyLocation == "down"))
                {

                    blockedBR = true;
                    blockedTL = false;
                    blockedBL = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedR = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "topLeft" && (enemyLocation == "bottomRight" || enemyLocation == "right" || enemyLocation == "down"))
                {

                    blockedBL = true;
                    blockedTL = false;
                    blockedBR = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedR = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "right" && (enemyLocation == "left" || enemyLocation == "inside"))
                {

                    blockedR = true;
                    blockedTL = false;
                    blockedBR = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedBL = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "left" && (enemyLocation == "right" || enemyLocation == "inside"))
                {

                    blockedL = true;
                    blockedTL = false;
                    blockedBR = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedBL = false;
                    blockedUp = false;
                    blockedDown = false;

                }
                else if (playerLocation == "up" && (enemyLocation == "down" || enemyLocation == "inside"))
                {

                    blockedUp = true;
                    blockedTL = false;
                    blockedBR = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedBL = false;
                    blockedR = false;
                    blockedDown = false;

                }
                else if (playerLocation == "down" && (enemyLocation == "up" || enemyLocation == "inside"))
                {

                    blockedDown = true;
                    blockedTL = false;
                    blockedBR = false;
                    blockedTR = false;
                    blockedL = false;
                    blockedBL = false;
                    blockedR = false;
                    blockedUp = false;

                }
                else
                {

                    blockedTL = false;
                    blockedTR = false;
                    blockedBL = false;
                    blockedBR = false;
                    blockedL = false;
                    blockedR = false;
                    blockedUp = false;
                    blockedDown = false;

                }


            }

        }
        velocity = Vector3.zero;

        if (blockedTL)
        {

            if (direction.x > 0)
            {

                velocity += Vector3.right * speed * Time.deltaTime;

            }
            if (direction.y > 0)
            {

                velocity += Vector3.up * speed * Time.deltaTime;

            }

        }
        else if (blockedTR)
        {

            if (direction.x < 0)
            {

                velocity += Vector3.left * speed * Time.deltaTime;

            }
            if (direction.y > 0)
            {

                velocity += Vector3.up * speed * Time.deltaTime;

            }

        }
        else if (blockedBL)
        {

            if (direction.x > 0)
            {

                velocity += Vector3.right * speed * Time.deltaTime;

            }
            if (direction.y < 0)
            {

                velocity += Vector3.down * speed * Time.deltaTime;

            }

        }
        else if (blockedBR)
        {

            if (direction.x < 0)
            {

                velocity += Vector3.left * speed * Time.deltaTime;

            }
            if (direction.y < 0)
            {

                velocity += Vector3.down * speed * Time.deltaTime;

            }

        }
        else if (blockedL)
        {

            if (direction.x > 0)
            {

                velocity += Vector3.up * speed * Time.deltaTime;

            }

        }
        else if (blockedR)
        {

            if (direction.x < 0)
            {

                velocity += Vector3.down * speed * Time.deltaTime;

            }

        }
        else if (blockedUp)
        {

            if (direction.y < 0)
            {

                velocity += Vector3.right * speed * Time.deltaTime;

            }

        }
        else if (blockedDown)
        {

            if (direction.y > 0)
            {

                velocity += Vector3.left * speed * Time.deltaTime;

            }

        }
        else if (!blockedTL && !blockedTR && !blockedBL && !blockedBR && !blockedL && !blockedR && !blockedUp && !blockedDown)
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
