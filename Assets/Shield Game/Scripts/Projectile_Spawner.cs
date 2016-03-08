using UnityEngine;
using System.Collections;
using System.IO;

public class Projectile_Spawner : MonoBehaviour
{
    //Where all the different prefabs for projectiles are stored
    public Transform Projectile1;
    public Transform Projectile2;
    public Transform Projectile3;
    public Transform Projectile4;
    public Transform Projectile5;
    public Transform Projectile6;

    public string fileName = "ExampleLevelProj";
    char[] delimiters = { ' ', '\n', '\t' };
    public TextAsset myFile;

    public float[] waveTimes;               //Tracks the times are which various waves are decided
    public int currentWave = 0;             //The current index in the waveTimes array
    public bool primaryWaveChoice = true;   //Indicates whether or not you are on the A or B wave (true is A, false is B)
    public TextAsset wave1File;             //The various different files to be loaded for each wave
    public TextAsset wave2FileA;
    public TextAsset wave2FileB;
    public TextAsset wave3FileA;
    public TextAsset wave3FileB;

    public float upBorder = 7;
    public float leftBorder = -7;
    public float downBorder = -7;
    public float rightBorder = 7;

    // Use this for initialization
    void Start()
    {
        parseFile();
        waveTimes = new float[1];
        waveTimes[0] = .2f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        checkWave();                                                            //Checks if a new wave has occurred
        EnemyController[] projectiles = FindObjectsOfType<EnemyController>();
        //Checks all enemycontroller prefabs currently in the scene and checks
        //if it's their spawn time, their sprite renderer is off,
        //and they aren't moving.
        //If these conditions are met, the sprite renderer is turned on and moving is set to true;
        foreach (EnemyController ec in projectiles)
        {
            
            if (ec.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                
                if (ec.spawnTime == Time.time)
                {
                    ec.GetComponentInParent<SpriteRenderer>().enabled = true;
                    ec.moving = true;
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
        string[] projectile_params;                 //Stores all the strings from the level file
        double currentSpawnTime = 0;                //The spawn time for the current projectile
        double currentSpeed = 0;                    //The speed of the current projectile
        double currentStartPos = 0;                 //The position on the border where the projectile will spawn
        Vector3 currentStartVector = Vector3.zero;  //The current vector for where the projectile will be instantiated
        try
        {
            projectile_params = myFile.text.Split(delimiters);
            for (int i = 0; i < projectile_params.Length; i++)
            {
                if (i % 5 == 0)
                {
                    currentSpeed = double.Parse(projectile_params[i]);
                }
                else if (i % 5 == 1)
                {
                    currentSpawnTime = double.Parse(projectile_params[i]);
                }
                else if (i % 5 == 2)
                {
                    currentStartPos = double.Parse(projectile_params[i]);
                }
                else if (i % 5 == 3)
                {
                    switch (int.Parse(projectile_params[i]))
                    {
                        case 1:
                            currentStartVector = new Vector3((float)currentStartPos, upBorder, 0);
                            break;
                        case 2:
                            currentStartVector = new Vector3(leftBorder, (float)currentStartPos, 0);
                            break;
                        case 3:
                            currentStartVector = new Vector3((float)currentStartPos, downBorder, 0);
                            break;
                        default:
                            currentStartVector = new Vector3(rightBorder, (float)currentStartPos, 0);
                            break;

                    }
                }
                else
                {
                    switch (int.Parse(projectile_params[i]))
                    {
                        case 1:
                            Instantiate(Projectile1).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                        case 2:
                            Instantiate(Projectile2).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                        case 3:
                            Instantiate(Projectile3).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                        case 4:
                            Instantiate(Projectile4).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                        case 5:
                            Instantiate(Projectile5).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                        default:
                            Instantiate(Projectile6).GetComponent<EnemyController>().Initialize((float)currentSpeed, (float)currentSpawnTime, currentStartVector);
                            break;
                    }
                    currentSpawnTime = 0;
                    currentSpeed = 0;
                    currentStartPos = 0;
                    currentStartVector = Vector3.zero;
                }


            }
        }
        catch (System.Exception e)
        {
            Debug.Log("File Couldn't be read");
            Debug.Log(e.Message);
        }
    }
}
