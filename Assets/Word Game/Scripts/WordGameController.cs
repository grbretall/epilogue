﻿using UnityEngine;
using System.Collections;

public class WordGameController : MonoBehaviour {

    public char letter;       //This is the alphabetical character the current letter represents
    public float spawnTime;     //The time when the letter will spawn
    public float letterDuration;//How long the letter will last before the next one is moved on to
    public bool currentlyActive = false;
    public bool keyHeld = false;

    float keyStrokePressedTime = 0.0f;
    float keyStrokeHeldTime = 0.0f;
    float shortLongCommandThreshold = 1.0f;
    int bufferSize;
    int[] morseCodeBuffer;     //0 indicates an empty array space, 1 indicates short, 2 indicates long
    int[] correctCode;
    int currentBufferPos = 0;

    /*
    float bufferStartTime = 0;
    float bufferTime = 8.0f;
    */


    // Use this for initialization
    void Start ()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        determineCorrectCode();
        for(int i = 0; i < morseCodeBuffer.Length; i++)
        {
            morseCodeBuffer[i] = 0;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void FixedUpdate()
    {
        if (currentlyActive)
        {
            if (bufferFull())
            { 
                if (checkBuffer())
                {
                    Debug.Log("You did it!");
                    currentlyActive = false;
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreTracker>().updateWordGameScore(1);
                }
            }
            if (Input.GetButton("MorseCode") && !keyHeld)
            {
                keyStrokePressedTime = Time.time;
                keyHeld = true;
            }
            if (Input.GetButtonUp("MorseCode"))
            {
                keyStrokeHeldTime = Time.time - keyStrokePressedTime;
                if (keyStrokeHeldTime <= shortLongCommandThreshold && currentBufferPos < morseCodeBuffer.Length)
                {
                    morseCodeBuffer[currentBufferPos] = 1;
                    currentBufferPos++;
                    Debug.Log("Short Command");
                }
                else if (currentBufferPos < morseCodeBuffer.Length)
                {
                    morseCodeBuffer[currentBufferPos] = 2;
                    currentBufferPos++;
                    Debug.Log("Long Command");
                }
                keyHeld = false;
            }
            if(Input.GetButton("RefreshBuffer") || bufferFull())
            {
                    for (int i = 0; i < morseCodeBuffer.Length; i++)
                    {
                        morseCodeBuffer[i] = 0;
                    }
                    currentBufferPos = 0;
                    Debug.Log(morseCodeBuffer.Length);
                    Debug.Log("Buffer Cleared");
            }
        }
    }

    //Initializes an individual letter
    public void initialize(Vector3 startPosition, char letter, float spawnTime, float letterDuration)
    {
        transform.position = startPosition;
        this.letter = letter;
        this.spawnTime = spawnTime;
        this.letterDuration = letterDuration;
    }

    //Sets the array for the correct morse code for this letter
    public void determineCorrectCode()
    {
        switch(letter)
        {
            case 'a':
                bufferSize = 2;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                break;
            case 'b':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 1;
                correctCode[3] = 1;
                break;
            case 'c':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 2;
                correctCode[3] = 1;
                break;
            case 'd':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 1;
                break;
            case 'e':
                bufferSize = 1;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                break;
            case 'f':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                correctCode[2] = 2;
                correctCode[3] = 1;
                break;
            case 'g':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 2;
                correctCode[2] = 1;
                break;
            case 'h':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                correctCode[2] = 1;
                correctCode[3] = 1;
                break;
            case 'i':
                bufferSize = 2;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                break;
            case 'j':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                correctCode[2] = 2;
                correctCode[3] = 2;
                break;
            case 'k':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 2;
                break;
            case 'l':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                correctCode[2] = 1;
                correctCode[3] = 1;
                break;
            case 'm':
                bufferSize = 2;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 2;
                break;
            case 'n':
                bufferSize = 2;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                break;
            case 'o':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 2;
                correctCode[2] = 2;
                break;
            case 'p':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                correctCode[2] = 2;
                correctCode[3] = 1;
                break;
            case 'q':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 2;
                correctCode[2] = 1;
                correctCode[3] = 2;
                break;
            case 'r':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                correctCode[2] = 1;
                break;
            case 's':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                correctCode[2] = 1;
                break;
            case 't':
                bufferSize = 1;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                break;
            case 'u':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                correctCode[2] = 2;
                break;
            case 'v':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 1;
                correctCode[2] = 1;
                correctCode[3] = 2;
                break;
            case 'w':
                bufferSize = 3;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 1;
                correctCode[1] = 2;
                correctCode[2] = 2;
                break;
            case 'x':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 1;
                correctCode[3] = 2;
                break;
            case 'y':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 1;
                correctCode[2] = 2;
                correctCode[3] = 2;
                break;
            case 'z':
                bufferSize = 4;
                morseCodeBuffer = new int[bufferSize];
                correctCode = new int[bufferSize];
                correctCode[0] = 2;
                correctCode[1] = 2;
                correctCode[2] = 1;
                correctCode[3] = 1;
                break;
        }
    }

    //Checks the current morse code buffer against the correct
    //code for this letter to see if they are equivalent
    public bool checkBuffer()
    {
        for(int i = 0; i < morseCodeBuffer.Length; i++)
        {
            if (morseCodeBuffer[i] != correctCode[i])
                return false;
        }
        return true;
    }

    public void destroy()
    {
        Destroy(this);
    }

    public bool bufferFull()
    {
        for(int i = 0; i < morseCodeBuffer.Length; i++)
        {
            if (morseCodeBuffer[i] == 0)
                return false;
        }
        return true;
    }
}
