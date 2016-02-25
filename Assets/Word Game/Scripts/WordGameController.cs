using UnityEngine;
using System.Collections;

public class WordGameController : MonoBehaviour {

    public char letter;       //This is the alphabetical character the current letter represents
    public float spawnTime;     //The time when the letter will spawn
    public float letterDuration;//How long the letter will last before the next one is moved on to
    public bool currentlyActive;


	// Use this for initialization
	void Start ()
    {
        //GetComponentInParent<SpriteRenderer>().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    //Initializes an individual letter
    public void initialize(Vector3 startPosition, char letter, float spawnTime, float letterDuration)
    {
        transform.position = startPosition;
        this.letter = letter;
        this.spawnTime = spawnTime;
        this.letterDuration = letterDuration;
    }
}
