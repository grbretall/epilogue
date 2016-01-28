using UnityEngine;
using System.Collections;

public class Arrow_Movement : MonoBehaviour
{
    public float speed = 5;
    public float spawnTime = 0;
    public bool moving = false;
    public enum arrowType {UP, DOWN, LEFT, RIGHT};
    public arrowType dir = arrowType.UP;
    public Vector3 destPos;

    public bool upPressed = false;
    private bool leftPressed = false;
    private bool downPressed = false;
    private bool rightPressed = false;

    private float upPressedTime = 0;
    private float leftPressedTime = 0;
    private float downPressedTime = 0;
    private float rightPressedTime = 0;
    private float holdButtonTime = 10f;

    public Vector3 leftArrowDest = new Vector3(-18, 1.7f, 0);
    public Vector3 rightArrowDest = new Vector3(-18, -4f, 0);
    public Vector3 upArrowDest = new Vector3(-18, 3.5f, 0);
    public Vector3 downArrowDest = new Vector3(-18, -1f, 0);

    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;

        switch (dir)
        {
            case arrowType.UP:
                destPos = upArrowDest;
                break;
            case arrowType.DOWN:
                destPos = downArrowDest;
                break;
            case arrowType.LEFT:
                destPos = leftArrowDest;
                break;
            case arrowType.RIGHT:
                destPos = downArrowDest;
                break;
            default:
                break;
                
        }
    
	}

    public void Initialize(float speed, float spawnTime, arrowType dir, Vector3 startPos)
    {
        this.speed = speed;
        this.spawnTime = spawnTime;
        this.dir = dir;
        transform.position = startPos;
    }
	
	// Update is called once per frame
	void Update ()
    {
        checkButtonPress();
        checkButtonPressExpiration();
        
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collider Entered");
        if (other.tag.Equals("Perfect_Hit_Zone"))
        {
            if(dir == arrowType.UP && upPressed)
            {
                Debug.Log("PerfectHit");       
                performHit(3, .04);
            }
            if (dir == arrowType.LEFT && leftPressed)
            {
                performHit(3, .04);
            }
            if (dir == arrowType.DOWN && downPressed)
            {
                performHit(3, .04);
            }
            if (dir == arrowType.RIGHT && rightPressed)
            {
                performHit(3, .04);
            }
        }
        if(other.tag.Equals("Near_Hit_Zone"))
        {
            if (dir == arrowType.UP && upPressed)
            {
                Debug.Log("Near Hit");
                performHit(2, .04);
            }
            if (dir == arrowType.LEFT && leftPressed)
            {
                performHit(2, .04);
            }
            if (dir == arrowType.DOWN && downPressed)
            {
                performHit(2, .04);
            }
            if (dir == arrowType.RIGHT && rightPressed)
            {
                performHit(2, .04);
            }
        }
        if(other.tag.Equals("MissZone"))
        {
            Debug.Log("Miss");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().resetRhythmGameCombo();
            GameObject.Destroy(this.gameObject);
        }
    }

    private void performHit(int scoreMod, double comboMod)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateRhythmGameScore(scoreMod, comboMod);
        GameObject.Destroy(this.gameObject);
    }

    private void checkButtonPress()
    {
        if (Input.GetButtonDown("UpArrow"))
        {
            upPressed = true;
            upPressedTime = Time.time;
        }
        if (Input.GetButtonDown("LeftArrow"))
        {
            leftPressed = true;
            leftPressedTime = Time.time;
        }
        if (Input.GetButtonDown("DownArrow"))
        {
            downPressed = true;
            downPressedTime = Time.time;
        }
        if (Input.GetButtonDown("RightArrow"))
        {
            rightPressed = true;
            rightPressedTime = Time.time;
        }
    }

    private void checkButtonPressExpiration()
    {
        if (Time.time - upPressedTime > holdButtonTime)
        {
            upPressed = false;
            upPressedTime = 0;
        }
        if (leftPressedTime - Time.time <= holdButtonTime)
        {
            leftPressed = false;
            leftPressedTime = 0;
        }
        if (downPressedTime - Time.time <= holdButtonTime)
        {
            downPressed = false;
            downPressedTime = 0;
        }
        if (rightPressedTime - Time.time <= holdButtonTime)
        {
            rightPressed = false;
            rightPressedTime = 0;
        }
    }
}