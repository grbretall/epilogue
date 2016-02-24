using UnityEngine;
using System.Collections;
using System.IO;


public class Arrow_Spawner : MonoBehaviour {

    //The transfors where the prefab for each different arrow is stored.
    public Transform UpArrow;
    public Transform LeftArrow;
    public Transform DownArrow;
    public Transform RightArrow;

    //The destination zone for each arrow
    public Vector3 upArrowDest = new Vector3(-38, 3.5f, 0);
    public Vector3 leftArrowDest = new Vector3(-38, 1f, 0);
    public Vector3 downArrowDest = new Vector3(-38, -1, 0);
    public Vector3 rightArrowDest = new Vector3(-38, -4, 0);

    //The text file that we will parse the level from and the delimiters that we are splitting that text file on (currently space and new line)
    public TextAsset myFile;
    char[] delimiters = { ' ', '\n' };

    public float[] waveTimes;               //Tracks the times are which various waves are decided
    public int currentWave = 0;             //The current index in the waveTimes array
    public bool primaryWaveChoice = true;   //Indicates whether or not you are on the A or B wave (true is A, false is B)
    public TextAsset wave1File;             //The various different files to be loaded for each wave
    public TextAsset wave2FileA;
    public TextAsset wave2FileB;
    public TextAsset wave3FileA;
    public TextAsset wave3FileB;

    // Use this for initialization
    void Start ()
    {
        parseFile();                //Creates all the prefabs in the level from the input file
        waveTimes = new float[1];
        waveTimes[0] = .2f;
    }
	

    /**Checks all arrow prefabs that currently exist within the scene
    *  and checks if their sprite renderer is off and it is their spawn time
    *  If it is an arrow's spawn time, we turn on the sprite renderer and set
    *  moving to true 
    */
    void FixedUpdate()
    {
        checkWave();                                                            //Checks if there's a new wave
        Arrow_Movement[] projectiles = FindObjectsOfType<Arrow_Movement>();     
        foreach (Arrow_Movement am in projectiles)
        {
            if (am.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                if (am.spawnTime == Time.time)
                {
                    am.GetComponentInParent<SpriteRenderer>().enabled = true;
                    am.moving = true;
                }
            }
        }
    }

    private void parseFile()
    {
        string[] arrow_params;              //An array that stores all of the strings we will use to build our level
        float currentSpeed = 0;             //Stores the speed for the current arrow we are spawning
        float currentSpawnTime = 0;         //Stores the spawn time for the current arrow we are spawning
        try
        {
            arrow_params = myFile.text.Split(delimiters);
            for (int i = 0; i < arrow_params.Length; i++)
            {
                //Parse the speed
                if (i % 3 == 0)
                {
                    currentSpeed = float.Parse(arrow_params[i]);
                }
                //Parse the spawn time
                else if (i % 3 == 1)
                {
                    currentSpawnTime = float.Parse(arrow_params[i]);
                }
                //Parse the direction of the arrow and then instantiate the arrow with the parsed speed, spawn time, and direction
                else
                {
                    switch (int.Parse(arrow_params[i]))
                    {
                        case 1:
                            Instantiate(UpArrow).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.UP, upArrowDest);
                            break;
                        case 2:
                            Instantiate(LeftArrow).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.LEFT, leftArrowDest);
                            break;
                        case 3:
                            Instantiate(DownArrow).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.DOWN, downArrowDest);
                            break;
                        default:
                            Instantiate(RightArrow).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.RIGHT, rightArrowDest);
                            break;
                    }
                    currentSpawnTime = 0;
                    currentSpeed = 0;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("File Couldn't be read");
            Debug.Log(e.Message);
        }
    }

    //Checks if there is a new wave
    //If there is, then the proper wave file is set as myFile and the new file
    //is subsequently parsed
    void checkWave()
    {
        if (currentWave < waveTimes.Length)
        {
            if (waveTimes[currentWave] == Time.time)
            {
                if (primaryWaveChoice)
                {
                    switch (currentWave)
                    {
                        case 0:
                            myFile = wave2FileA;
                            break;
                        case 1:
                            myFile = wave3FileA;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (currentWave)
                    {
                        case 0:
                            myFile = wave2FileB;
                            break;
                        case 1:
                            myFile = wave3FileB;
                            break;
                        default:
                            break;
                    }
                }
                currentWave++;
                parseFile();
            }
        }
    }
}
