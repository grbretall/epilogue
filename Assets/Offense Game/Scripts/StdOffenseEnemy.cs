using UnityEngine;
using System.Collections;

public class StdOffenseEnemy : MonoBehaviour
{
    public bool isMoving = false;
    public float speed = 2;
    public float spawnTime = 0;
    public int damageValue = 1;
    public Vector3 destPos;
    public Vector3 endZone;

    public int health = 3;
    public bool nonLethalDamage = false;
    public bool civilian = false;
    public bool resistant = false;

    public Vector3[] nodeList = new Vector3[51];
    public int[] enemyRoute = new int[10];
    public int currentNode = 0;

    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        destPos = nodeList[enemyRoute[currentNode]];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(health <= 0)
        {
            killEnemy();
        }
        if(transform.position.Equals(endZone))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateOffenseGameDamage(damageValue);
            killEnemy();
        }
        if(transform.position.Equals(destPos))
        {
            if (currentNode != -1)
            {
                currentNode++;
                if (nodeList[enemyRoute[currentNode]].Equals(Vector3.zero))
                {
                    destPos = endZone;
                    currentNode = -1;
                }
                else
                {
                    destPos = nodeList[enemyRoute[currentNode]];
                }
            }
        }
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);
        }
    }

    public void Initialize(float speed, float spawnTime, int damage, int health, bool civilian, bool resistant, Vector3 startPos, int[] enemyRoute)
    {
        this.speed = speed;
        this.spawnTime = spawnTime;
        damageValue = damage;
        this.health = health;
        this.civilian = civilian;
        this.resistant = resistant;
        transform.position = startPos;
        this.enemyRoute = enemyRoute;
    }

    public void onTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("NonLethalAttack") && !resistant)
        {
            nonLethalDamage = true;
            dealDamage();
        }
    }

    public void dealDamage()
    {
        health--;
    }

    public void killEnemy()
    {
        if (health <= 0)
        {
            if (nonLethalDamage)
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateOffenseGameKnockouts(1);
            }
            else
            {
                if(civilian)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateOffenseGameCivilianKills(1);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateOffenseGameEnemyKills(1);
                }
            }
        }
        GameObject.Destroy(this.gameObject);
    }
}
