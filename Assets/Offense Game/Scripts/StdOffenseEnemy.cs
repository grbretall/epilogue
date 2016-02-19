using UnityEngine;
using System.Collections;

public class StdOffenseEnemy : MonoBehaviour
{
    public bool isMoving = true;
    public float speed = 5;
    public float spawnTime = 0;
    public Vector3 destPos;
    public Vector3 endZone;

    public Vector3[] nodeList = new Vector3[50];
    public int[] enemyRoute = new int[10];
    public int currentNode = 0;

    // Use this for initialization
    void Start ()
    {
        destPos = nodeList[enemyRoute[currentNode]];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(transform.position.Equals(destPos))
        {
            currentNode++;
            if (nodeList[enemyRoute[currentNode]].Equals(Vector3.zero))
            {
                destPos = endZone;
            }
            else
            {
                destPos = nodeList[enemyRoute[currentNode]];
            }
        }
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);
        }
    }

    public void Initialize(float speed, float spawnTime, Vector3 startPos, int[] enemyRoute)
    {
        this.speed = speed;
        this.spawnTime = spawnTime;
        transform.position = startPos;
        this.enemyRoute = enemyRoute;
    }
}
