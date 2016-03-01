using UnityEngine;
using System.Collections;

public class Arrow_Movement : MonoBehaviour
{
    public float speed = 5;                         //How fast the arrow will move
    public float spawnTime = 0;                     //What time in seconds that the arrow will spawn
    public bool moving = false;                     //Whether or not the arrow is currently moving
    public enum arrowType {UP, DOWN, LEFT, RIGHT};  //A type for arrow directions
    public arrowType dir;                           //The arrow's set direction (up, down, left or right)
    public Vector3 destPos;                         //The destination position of a specific arrow

    //Boolean values to check if a certain arrow is pressed
    public bool upPressed = false;
    private bool leftPressed = false;
    private bool downPressed = false;
    private bool rightPressed = false;

    //Tracks what time the key is first pressed
    private float upPressedTime = 0;
    private float leftPressedTime = 0;
    private float downPressedTime = 0;
    private float rightPressedTime = 0;

    //Tracks how long the key press will last for
    public float holdButtonTime = 1f;

    //Position vector for where each arrow will be heading toward (the miss zone)
    public Vector3 leftArrowDest = new Vector3(-16.5f, 1f, 0);
    public Vector3 rightArrowDest = new Vector3(-16.5f, -3.5f, 0);
    public Vector3 upArrowDest = new Vector3(-16.5f, 3.5f, 0);
    public Vector3 downArrowDest = new Vector3(-16.5f, -1f, 0);

    // Use this for initialization
    void Start ()
    {
        //Start the arrow as invisible and not moving
        GetComponentInParent<SpriteRenderer>().enabled = false;
        moving = false;

        //Sets this arrow's destPos to the correct destination zone for its direction
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
                destPos = rightArrowDest;
                break;
            default:
                break;
                
        }
    
	}

    //The function used to intiialize an arrow when it's created.  Sets speed, spawn time, direction, and starting position
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
        checkButtonPress();             //Checks to see if the relevant arrow key to this arrow is being pressed
        checkButtonPressExpiration();   //Checks if the current arrow key press has expired
        
        //If moving is true, the arrow will move towards the destPos vector in a straight line
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destPos, step);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Checks for collision in the perfect hit zone
        /*if (other.tag.Equals("Perfect_Hit_Zone"))
        {
            if(dir == arrowType.UP && upPressed)
            {
                Debug.Log("Perfect Hit Up");
                performHit(3, .04);
            }
            if (dir == arrowType.LEFT && leftPressed)
            {
                Debug.Log("Perfect Hit Left");
                performHit(3, .04);
            }
            if (dir == arrowType.DOWN && downPressed)
            {
                Debug.Log("Perfect Hit Down");
                performHit(3, .04);
            }
            if (dir == arrowType.RIGHT && rightPressed)
            {
                Debug.Log("Perfect Hit Right");
                performHit(3, .04);
            }
        }
        //Checks for collision in the near hit zone (the area to the left of the perfect hit zone
        if(other.tag.Equals("Near_Hit_Zone"))
        {
            if (dir == arrowType.UP && upPressed)
            {
                Debug.Log("Near Hit Up");
                performHit(2, .04);
            }
            if (dir == arrowType.LEFT && leftPressed)
            {
                Debug.Log("Near Hit Left");
                performHit(2, .04);
            }
            if (dir == arrowType.DOWN && downPressed)
            {
                Debug.Log("Near Hit Down");
                performHit(2, .04);
            }
            if (dir == arrowType.RIGHT && rightPressed)
            {
                Debug.Log("Near Hit Right");
                performHit(2, .04);
            }
        }*/
        if (other.tag.Equals("Perfect_Hit_Zone"))
        {
            if (dir == arrowType.UP && getButtonPress() == 1)
            {
                Debug.Log("Perfect Hit Up");
                performHit(3, .04);
            }
            if (dir == arrowType.LEFT && getButtonPress() == 2)
            {
                Debug.Log("Perfect Hit Left");
                performHit(3, .04);
            }
            if (dir == arrowType.DOWN && getButtonPress() == 3)
            {
                Debug.Log("Perfect Hit Down");
                performHit(3, .04);
            }
            if (dir == arrowType.RIGHT && getButtonPress() == 4)
            {
                Debug.Log("Perfect Hit Right");
                performHit(3, .04);
            }
        }
        if (other.tag.Equals("Near_Hit_Zone"))
        {
            if (dir == arrowType.UP && getButtonPress() == 1)
            {
                Debug.Log("Near Hit Up");
                performHit(2, .04);
            }
            if (dir == arrowType.LEFT && getButtonPress() == 2)
            {
                Debug.Log("Near Hit Left");
                performHit(2, .04);
            }
            if (dir == arrowType.DOWN && getButtonPress() == 3)
            {
                Debug.Log("Near Hit Down");
                performHit(2, .04);
            }
            if (dir == arrowType.RIGHT && getButtonPress() == 4)
            {
                Debug.Log("Near Hit Right");
                performHit(2, .04);
            }
        }
        //Checks for a miss and resets the combo
        if (other.tag.Equals("Miss_Zone"))
        {
            //Debug.Log("Miss");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().resetRhythmGameCombo();
            GameObject.Destroy(this.gameObject);
        }
    }

    //Records the point value and combo increase for a hit
    private void performHit(int scoreMod, double comboMod)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateRhythmGameScore(scoreMod, comboMod);
        GameObject.Destroy(this.gameObject);
    }

    //Checks to see if the relevant arrow key to this arrow is being pressed
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

    private int getButtonPress()
    {
        if (Input.GetButton("UpArrow"))
        {
            return 1;
        }
        if (Input.GetButton("LeftArrow"))
        {
            return 2;
        }
        if (Input.GetButton("DownArrow"))
        {
            return 3;
        }
        if (Input.GetButton("RightArrow"))
        {
            return 4;
        }
        return 0;
    }

    //Checks if the current arrow key press has expired
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