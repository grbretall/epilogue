using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    Rigidbody2D grabbedObject = null;       //The current object that is grabbed by the mouse
    Vector2 origPos2D = new Vector2(0, 0);  

    public Transform nonLethalAttack;       //Stores the prefab for the non-lethal attack radius
    public bool cooldown = false;           //Tells you whether or not you are currently in cooldown for the nonlethal
    public float cooldownDuration = 1.0f;   //The length of time the cooldown period lasts for
    public float cooldownStartTime = 0;     //Tracks what time  a given cooldown period starts

	// Update is called once per frame
	void Update ()
    {
        //Checks if the cooldown for the offense game right click attack has expired
        if((Time.time - cooldownStartTime) >= cooldownDuration)
        {
            cooldown = false;
            cooldownStartTime = 0;
        }
        //Processes what happens when left mouse button is pressed
	    if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            Vector2 dir = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, dir);
            //Checks if we hit something and it had a collider
            if (hit != null && hit.collider != null)                                                         
            {
                //Checks if what we hit has a rigibody and has the shield tag
                if (hit.collider.GetComponent<Rigidbody2D>() != null && hit.collider.tag.Equals("Shield"))   
                {
                    grabbedObject = hit.collider.GetComponent<Rigidbody2D>(); //We set grabbedObject equal to the valid thing we've clicked on
                }
                //Checks if what we hit has a rigibody and has the enemy tag
                //If it is, we will deal damage to that enemy
                else if (hit.collider.GetComponent<Rigidbody2D>() != null && hit.collider.tag.Equals("Enemy"))
                {
                    hit.collider.GetComponent<StdOffenseEnemy>().dealDamage(); ;
                }
            }
        }

        //Checks what happens when the left mouse button is released
        if(Input.GetMouseButtonUp(0))                                                                       
        {
            // If they let up the mouse button, the grabbed object will stop being attached to the mouse so long as an object is grabbed
            if (grabbedObject != null)
            {
                grabbedObject.velocity = Vector2.zero;
                grabbedObject = null;
            }
        }

        //Checks what happens when the right mouse button is pushed down
        if (Input.GetMouseButtonDown(1))
        {
            
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            Vector2 dir = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, dir);
            if (hit != null && hit.collider != null)
            {
                //  Checks to see if the mouse has collided with anywhere in the offense game section of the screen 
                //  It also checks to make sure you are currently not in cooldown and if these are both true, then
                //  a new prefab is instantiated that serves as the radius of effect for the non-lethal attack
                if (hit.collider.GetComponent<Rigidbody2D>() != null && hit.collider.tag.Equals("OffenseGameBackground") && !cooldown)
                {
                    //Debug.Log("Right Clicked at " + mouseWorldPos3D);
                    Instantiate(nonLethalAttack).GetComponent<NonLethalHitController>().Initialize(new Vector3(mouseWorldPos3D.x, mouseWorldPos3D.y, 0));
                    cooldown = true;
                    cooldownStartTime = Time.time;
                }
            }
        }

        Rotate();
	}

    //Rotates the shield while it is grabbed in accordance with the mouse movement
    void Rotate()
    {
        if(grabbedObject != null)
        {
            Vector2 moveDirection = grabbedObject.position - Vector2.zero;
            if(moveDirection != Vector2.zero)
            {
                float angleOfMovement = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                grabbedObject.transform.rotation = Quaternion.AngleAxis(angleOfMovement, Vector3.forward);
            }
        }
    }

    void FixedUpdate()
    {
        float newX;
        float newY;
        if(grabbedObject != null)
        {
            //Checks to see if we currently have an object being grabbed by the mouse then moves the object to where the mouse is
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            /*if (mouseWorldPos3D.x < 1.9)
                newX = 1.9f;
            else
                newX = mouseWorldPos3D.x;
            if (mouseWorldPos3D.y < 1.9)
                newY = 1.9f;
            else
                newY = mouseWorldPos3D.y;
            Vector2 mousePos2D = new Vector2(newX, newY);*/
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

            Vector2 allowedPos2D = mousePos2D - origPos2D;
            
            allowedPos2D = Vector2.ClampMagnitude(allowedPos2D, 2.0f);

            grabbedObject.position = origPos2D + allowedPos2D;
            //grabbedObject.position = allowedPos2D - origPos2D;
        }
    }
}
