using UnityEngine;
using System.Collections;

public class NonLethalHitController : MonoBehaviour
{
    public float initTime = 0.0f;
    public float animRunTime = 1.0f;
	// Use this for initialization
	void Start ()
    {
        initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if((Time.time - animRunTime) >= initTime)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    public void Initialize(Vector3 startPos)
    {
        transform.position = startPos;
    }
}
