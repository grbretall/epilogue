using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public Vector3 centerPos = new Vector3(0, 0, 0);
    public enum direction {UP, LEFT, DOWN, RIGHT };
    public direction orientation; 
    public float speed = 1;
    public float spawnTime = 0;
    public bool moving = false;
	
    void Start()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = centerPos - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, 0, zAngle);

        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, centerPos, step);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Shield"))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if(other.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateShieldGameScore(1);
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnCollision2D(Collider2D other)
    {
        if (other.tag.Equals("Shield"))
        {
            GameObject.Destroy(this.gameObject);
        }
        else if (other.tag.Equals("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateShieldGameScore(1);
            GameObject.Destroy(this.gameObject);
        }
    }

    public void Initialize(float speed, float spawnTime,Vector3 startPos)
    {
        this.speed = speed;
        this.spawnTime = spawnTime;
        transform.position = startPos;
    }
}
