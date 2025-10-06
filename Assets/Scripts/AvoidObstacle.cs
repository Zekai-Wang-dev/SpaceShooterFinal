using System.Collections.Generic; 
using UnityEngine;

public class AvoidObstacle : MonoBehaviour
{

    public List<Vector3> points = new List<Vector3>();
    public List<List<Vector3>> rectangleObs = new List<List<Vector3>>();

    public Transform enemyTransform;
    public Transform plrTransform;

    public Vector3 direction;
    public float range; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        points.Add(new Vector3(0, 0, 0));
        points.Add(new Vector3(5, 0, 0));
        points.Add(new Vector3(5, 5, 0));
        points.Add(new Vector3(0, 5, 0));

        rectangleObs.Add(points);

    }

    // Update is called once per frame
    void Update()
    {


        FindPath();

    }

    public void FindPath()
    {

        direction = (plrTransform.position - enemyTransform.position).normalized;
        

        for (int i = 0; i < rectangleObs.Count; i++)
        {

            for (int j = 0; j < rectangleObs[i].Count; j++)
            {

                Vector3 point = rectangleObs[i][j];
                Vector3 directionToPoint = (point - enemyTransform.position).normalized;
                Vector3 targetPoint = enemyTransform.position + direction * range;

                float distanceToPoint = Vector3.Distance(enemyTransform.position, point);

                Debug.DrawRay(enemyTransform.position, directionToPoint * distanceToPoint, Color.blue);

            }

        }

        for (int i = 0; i < rectangleObs.Count; i++)
        {
            for (int j = 0; j < rectangleObs[i].Count - 1; j++)
            {

                Debug.DrawLine(rectangleObs[i][j], rectangleObs[i][j + 1], Color.green);

            }
            Debug.DrawLine(rectangleObs[i][rectangleObs[i].Count - 1], rectangleObs[i][0], Color.green);
        }

        Debug.DrawRay(enemyTransform.position, direction * range, Color.red);


    }

}
