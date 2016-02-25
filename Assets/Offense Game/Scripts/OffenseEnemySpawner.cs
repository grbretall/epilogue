using UnityEngine;
using System.Collections;

public class OffenseEnemySpawner : MonoBehaviour {

    public Transform Enemy1;
    public Transform Enemy2;
    public Transform Enemy3;
    public Transform Enemy4;
    public Transform Enemy5;
    public Transform Enemy6;

    float topBorder = -11.5f;

    public string fileName = "ExampleLevelOff";
    char[] delimiters = { ' ', '\n' };
    public TextAsset myFile;

    public float[] waveTimes;
    public int currentWave = 0;
    public bool primaryWaveChoice = true;
    public TextAsset wave1File;
    public TextAsset wave2FileA;
    public TextAsset wave2FileB;
    public TextAsset wave3FileA;
    public TextAsset wave3FileB;

    // Use this for initialization
    void Start ()
    {
        parseFile();
        waveTimes = new float[1];
        waveTimes[0] = .2f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        checkWave();
        StdOffenseEnemy[] enemies = FindObjectsOfType<StdOffenseEnemy>();
        //Checks all StdOffenseEnemy prefabs currently in the scene and checks
        //if it's their spawn time, their sprite renderer is off,
        //and they aren't moving.
        //If these conditions are met, the sprite renderer is turned on and moving is set to true;
        foreach (StdOffenseEnemy enemy in enemies)
        {
            if (enemy.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                if (enemy.spawnTime == Time.time)
                {
                    enemy.GetComponentInParent<SpriteRenderer>().enabled = true;
                    enemy.isMoving = true;
                }
            }
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

    void parseFile()
    {
        string[] offense_params;                    //Stores all the strings from the level file
        double currentSpawnTime = 0;                //Stores the spawn time of the current enemy
        double currentSpeed = 0;                    //Stores the speed of the current enemy
        int currentDamage = 0;                      //Stores the damage of the current enemy
        int currentHealth = 0;                      //Stores the health of the current enemy
        bool currentCivilian = false;               //Stores the civilian status of the current enemy
        bool currentResistant = false;              //Stores the resistance status of the current enemy
        int[] currentEnemyRoute = new int[10];      //Stores the node route for the current enemy (indexes for the full node list)
        int enemyRouteCounter = 0;                  //Stores our position in currentEnemyRoute
        Vector3 currentStartVector = Vector3.zero;  //Stores the vector of where our current enemy will be instantiated

        try
        {
            offense_params = myFile.text.Split(delimiters);

            for(int i = 0; i < offense_params.Length; i++)
            {
                if(i % 18 == 0)
                {
                    currentSpeed = double.Parse(offense_params[i]);
                }
                else if(i % 18 == 1)
                {
                    currentSpawnTime = double.Parse(offense_params[i]);
                }
                else if(i % 18 == 2)
                {
                    currentDamage = int.Parse(offense_params[i]);
                }
                else if(i % 18 == 3)
                {
                    currentHealth = int.Parse(offense_params[i]);
                }
                else if(i % 18 == 4)
                {
                    currentCivilian = int.Parse(offense_params[i]) != 0;
                }
                else if(i % 18 == 5)
                {
                    currentResistant = int.Parse(offense_params[i]) != 0;
                }
                else if(i % 18 == 6)
                {
                    currentStartVector = new Vector3((float)double.Parse(offense_params[i]), topBorder, 0);
                }
                else if(i % 18 != 17)
                {
                    currentEnemyRoute[enemyRouteCounter] = int.Parse(offense_params[i]);
                    enemyRouteCounter++;
                }
                else
                {
                    enemyRouteCounter = 0;
                    switch(int.Parse(offense_params[i]))
                    {
                        case 1:
                            Instantiate(Enemy1).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                        case 2:
                            Instantiate(Enemy2).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                        case 3:
                            Instantiate(Enemy3).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                        case 4:
                            Instantiate(Enemy4).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                        case 5:
                            Instantiate(Enemy5).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                        default:
                            Instantiate(Enemy6).GetComponent<StdOffenseEnemy>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentDamage, currentHealth, currentCivilian, currentResistant, currentStartVector, currentEnemyRoute);
                            break;
                    }
                    currentSpawnTime = 0;
                    currentSpeed = 0;
                    currentDamage = 0;
                    currentHealth = 0;
                    currentResistant = false;
                    currentCivilian = false;
                    currentStartVector = Vector3.zero;
                }
            }
        }
        catch(System.Exception e)
        {
            Debug.Log("File couldn't be read");
            Debug.Log(e.Message);
        }
    }
}
