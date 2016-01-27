using UnityEngine;
using System.Collections;
using System.IO;


public class Arrow_Spawner : MonoBehaviour {

    public string fileName = "ExampleLevelArrow";
    public Transform Arrow_Template;
    public TextAsset myFile;
    char[] delimiters = { ' ', '\n' };
    
     

	// Use this for initialization
	void Start ()
    {
        string[] arrow_params;
        float currentSpawnTime = 0;
        float currentSpeed = 0;
        int currentDir = 0;
        try
        {
            //TextAsset myFile = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
            arrow_params = myFile.text.Split(delimiters);
                for(int i = 0; i < arrow_params.Length; i++)
                {
                    if(i%3 == 0)
                    {
                        currentSpeed = float.Parse(arrow_params[i]);
                    }
                    else if(i%3 == 1)
                    {
                        currentSpawnTime = float.Parse(arrow_params[i]);
                    }
                    else
                    {
                        currentDir = int.Parse(arrow_params[i]);
                        switch (currentDir)
                        {
                            case 1:
                                Instantiate(Arrow_Template).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.UP, new Vector3(-38,3.5f,0));
                                break;
                            case 2:
                                Instantiate(Arrow_Template).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.LEFT, new Vector3(-38, 1f, 0));
                                break;
                            case 3:
                                Instantiate(Arrow_Template).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.DOWN, new Vector3(-38, -1, 0));
                                break;
                            default:
                                Instantiate(Arrow_Template).GetComponent<Arrow_Movement>().Initialize(currentSpeed, currentSpawnTime, Arrow_Movement.arrowType.RIGHT, new Vector3(-38, -4, 0));
                                break;
                        }
                        currentSpawnTime = 0;
                        currentSpeed = 0;
                        currentDir = 0;
                     }
            }
        }
        catch(System.Exception e)
        {
            Debug.Log("File Couldn't be read");
            Debug.Log(e.Message);
        }

	}
	


    void FixedUpdate()
    {
        Arrow_Movement[] projectiles = FindObjectsOfType<Arrow_Movement>();
        foreach (Arrow_Movement am in projectiles)
        {
            if (am.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                if (am.spawnTime == Time.time)
                {
                    Debug.Log("Hello World");
                    am.GetComponentInParent<SpriteRenderer>().enabled = true;
                    am.moving = true;
                }
            }
        }
    }
}
