using UnityEngine;
using System.Collections;

public class StdOffenseEnemy : MonoBehaviour
{
    public bool isMoving = true;
    public float speed = 5;
    public float spawnTime = 0;
    public float startPos = 0;
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
        if(destPos.Equals(Vector3.zero))
        {
            destPos = endZone;
        }
        else if(transform.position.Equals(destPos))
        {
            currentNode++;
            destPos = nodeList[enemyRoute[currentNode]];
        }
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);
        }
    }
}
