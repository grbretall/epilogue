using UnityEngine;
using System.Collections;

public class StdOffenseEnemy : MonoBehaviour
{
    public bool isMoving = false;       //Whether or not the enemy is moving
    public float speed = 2;             //The speed of the enemy
    public float spawnTime = 0;         //The time in seconds when the enemy will spawn
    public int damageValue = 1;         //How much damage the enemy will deal if it reaches the goal
    public Vector3 destPos;             //The current vector the enemy is moving towards
    public Vector3 endZone;             //The overall end goal area

    public int health = 3;              //How much damage this enemy must receive before dying
    public bool nonLethalDamage = false;//Whether or not this enemy has received non-lethal damage
    public bool civilian = false;       //Whether or not this enemy is a civilian
    public bool resistant = false;      //Whether or not this enemy is affected by non-lethal attacks

    public Vector3[] nodeList = new Vector3[51];    //The list of all possible destination nodes for the enemy to travel between
    public int[] enemyRoute = new int[10];          //The route the enemywill take amongst the above node list (stores indexes for nodeList)
    public int currentNode = 0;                     //The current position in enemyRoute

    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        destPos = nodeList[enemyRoute[currentNode]];
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Checks if an enemy's health has gone to 0 or below
        if (health <= 0)
        {
            killEnemy();
        }
        //Checks if the enemy has reached the end zone
        if(transform.position.Equals(endZone))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateOffenseGameDamage(damageValue);
            killEnemy();
        }
        //Checks if the enemy has reached a destiantion
        //If it has, the next node in the route is set as the destination
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
        //Moves the enemy towards its current destination
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

    //If the enemy is not resistant and is colliding
    //with a nonlethal attack prefab, then the enemy
    //will take damage and get its nonlethal flag set
    //to true
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("NonLethalAttack") && !resistant)
        {
            nonLethalDamage = true;
            dealDamage();
        }
    }

    //Decrements the enemy's health by 1
    public void dealDamage()
    {
        health--;
    }

    //Checks how an enemy died and updates the score accordingly
    //The result will either be:
    //  1. knocked out, updating knockouts
    //  2. killed and civilian, updating civilians killed
    //  3. killed and enemy, updating enemies killed
    //  4. reached the end zone, updating the player's damage score
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
        Destroy(gameObject);
    }
}
