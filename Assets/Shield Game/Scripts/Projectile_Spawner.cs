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

    public float upBorder = 2;
    public float leftBorder = -2;
    public float downBorder = -2;
    public float rightBorder = 2;

    // Use this for initialization
    void Start()
    {

        string[] projectile_params;
        float currentSpawnTime = 0;
        float currentSpeed = 0;
        float currentStartPos = 0;
        Vector3 currentStartVector = Vector3.zero;
        EnemyController.direction currentDir = EnemyController.direction.UP;
        try
        {
            //TextAsset myFile = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
            projectile_params = myFile.text.Split(delimiters);
            for (int i = 0; i < projectile_params.Length; i++)
            {
                if (i % 5 == 0)
                {
                    currentSpeed = float.Parse(projectile_params[i]);
                }
                else if (i % 5 == 1)
                {
                    currentSpawnTime = float.Parse(projectile_params[i]);
                }
                else if (i % 5 == 2)
                {
                    currentStartPos = float.Parse(projectile_params[i]);
                }
                else if (i % 5 == 3)
                {
                    switch (int.Parse(projectile_params[i]))
                    {
                        case 1:
                            currentDir = EnemyController.direction.UP;
                            currentStartVector = new Vector3(currentStartPos, upBorder, 0);
                            break;
                        case 2:
                            currentDir = EnemyController.direction.LEFT;
                            currentStartVector = new Vector3(leftBorder, currentStartPos, 0);
                            break;
                        case 3:
                            currentDir = EnemyController.direction.DOWN;
                            currentStartVector = new Vector3(currentStartPos, downBorder, 0);
                            break;
                        default:
                            currentDir = EnemyController.direction.RIGHT;
                            currentStartVector = new Vector3(rightBorder, currentStartPos, 0);
                            break;

                    }
                }
                else
                {
                    switch (int.Parse(projectile_params[i]))
                    {
                        case 1:
                            Instantiate(Projectile1).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
                            break;
                        case 2:
                            Instantiate(Projectile2).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
                            break;
                        case 3:
                            Instantiate(Projectile3).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
                            break;
                        case 4:
                            Instantiate(Projectile4).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
                            break;
                        case 5:
                            Instantiate(Projectile5).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
                            break;
                        default:
                            Instantiate(Projectile6).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, currentDir, currentStartVector);
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

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        EnemyController[] projectiles = FindObjectsOfType<EnemyController>();
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
        /*if( Time.time == waveTimes[currentWave])
        {
            SpriteRenderer[] sprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer sr in sprites)
            {
                if(sr.tag.Equals("Projectile"))
                {
                    if(!sr.enabled && sr.GetComponentInParent<EnemyController>().spawnTime == waveTimes[currentWave])
                    {
                        sr.enabled = true;
                        sr.GetComponentInParent<EnemyController>().moving = true;
                    }
                }
            }
            currentWave++;
        }*/
    }
}
