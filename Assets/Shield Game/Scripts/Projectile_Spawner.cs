using UnityEngine;
using System.Collections;
using System.IO;

public class Projectile_Spawner : MonoBehaviour
{

    /*public float[] waveTimes;   //Stores all the times at which projectiles spawn
    public int numWaves = 2;    //The number of times at which projectiles spawn
    public int currentWave = 0; //Stores what the current spawning wave is
    */
    public string fileName = "ExampleLevelProj";
    public Transform Projectile_Template;
    char[] delimiters = { ' ', '\n' };
    public TextAsset myFile;

    public float upBorder = 2;
    public float leftBorder = -2;
    public float downBorder = -2;
    public float rightBorder = 2;

    // Use this for initialization
    void Start ()
    {
        
        string[] projectile_params;
        float currentSpawnTime = 0;
        float currentSpeed = 0;
        float currentStartPos = 0;
        int currentDir = 0;
        try
        {
            //TextAsset myFile = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
            projectile_params = myFile.text.Split(delimiters);
                for (int i = 0; i < projectile_params.Length; i++)
                {
                    if (i % 4 == 0)
                    {
                        currentSpeed = float.Parse(projectile_params[i]);
                    }
                    else if (i % 4 == 1)
                    {
                        currentSpawnTime = float.Parse(projectile_params[i]);
                    }
                    else if(i % 4 == 2)
                    {
                        currentStartPos = float.Parse(projectile_params[i]);
                    }
                    else
                    {
                        currentDir = int.Parse(projectile_params[i]);
                        switch (currentDir)
                        {
                            case 1:
                                Instantiate(Projectile_Template).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, EnemyController.direction.UP, 
                                    new Vector3(currentStartPos, upBorder, 0));
                                break;
                            case 2:
                                Instantiate(Projectile_Template).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, EnemyController.direction.LEFT, 
                                    new Vector3(leftBorder, currentStartPos, 0));
                                break;
                            case 3:
                                Instantiate(Projectile_Template).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, EnemyController.direction.DOWN, 
                                    new Vector3(currentStartPos, downBorder, 0));
                                break;
                            default:
                                Instantiate(Projectile_Template).GetComponent<EnemyController>().Initialize(currentSpeed, currentSpawnTime, EnemyController.direction.RIGHT, 
                                    new Vector3(rightBorder, currentStartPos, 0));
                                break;
                        }
                        currentSpawnTime = 0;
                        currentSpeed = 0;
                        currentDir = 0;
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
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        EnemyController[] projectiles = FindObjectsOfType<EnemyController>();
        foreach (EnemyController ec in projectiles)
        {
            if (ec.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                if(ec.spawnTime == Time.time)
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
