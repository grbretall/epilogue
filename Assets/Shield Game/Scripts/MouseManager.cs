using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour
{

    Rigidbody2D grabbedObject = null;
    Vector2 origPos2D = new Vector2(0, 0);
    float dragSpeed = 10f;

	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            Vector2 dir = Vector2.zero;

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, dir);
            if(hit != null && hit.collider != null)                                                         
            {
            //We hit something and it had a collider
                if (hit.collider.GetComponent<Rigidbody2D>() != null && hit.collider.tag.Equals("Shield"))   
                {
                //What we hit has a rigibody and has the shield tag

                    grabbedObject = hit.collider.GetComponent<Rigidbody2D>(); //We set grabbedObject equal to the valid thing we've clicked on
                }
            }
        }

        if(Input.GetMouseButtonUp(0))                                                                       
        {
            // If they let up the mouse button, the grabbed object will stop being attached to the mouse
            grabbedObject.velocity = Vector2.zero;
            grabbedObject = null;
        }
        Rotate();
	}

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
        if(grabbedObject != null)
        {
            //Checks to see if we currently have an object being grabbed by the mouse then moves the object to where the mouse is
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

            Vector2 allowedPos2D = mousePos2D - origPos2D;
            
            allowedPos2D = Vector2.ClampMagnitude(allowedPos2D, 2.0f);

            grabbedObject.position = origPos2D + allowedPos2D;
        }
    }
}
