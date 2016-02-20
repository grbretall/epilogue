using UnityEngine;
using System.Collections;
using System.IO;

public class Projectile_Spawner : MonoBehaviour
{

    /*public float[] waveTimes;   //Stores all the times at which projectiles spawn
    public int numWaves = 2;    //The number of times at which projectiles spawn
    public int currentWave = 0; //Stores what the current spawning wave is
    */
    public Transform Projectile1;
    public Transform Projectile2;
    public Transform Projectile3;
    public Transform Projectile4;
    public Transform Projectile5;
    public Transform Projectile6;

    public string fileName = "ExampleLevelProj";
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
        checkWave();
        EnemyController[] projectiles = FindObjectsOfType<EnemyController>();
        foreach (EnemyController ec in projectiles)
        {
            
            if (ec.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                
                if (ec.spawnTime == Time.time)
                {
                    //Debug.Log("We're in the spawn checkin loop");
                    ec.GetComponentInParent<SpriteRenderer>().enabled = true;
                    ec.moving = true;
                }
            }
        }
    }

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
        string[] projectile_params;
        double currentSpawnTime = 0;
        double currentSpeed = 0;
        double currentStartPos = 0;
        Vector3 currentStartVector = Vector3.zero;
        EnemyController.direction currentDir = EnemyController.direction.UP;
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
                            //Debug.Log("We're instantiating");
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
