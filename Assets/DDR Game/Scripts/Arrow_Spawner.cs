using UnityEngine;
using System.Collections;
using System.IO;


public class Arrow_Spawner : MonoBehaviour {

    public Transform UpArrow;
    public Transform LeftArrow;
    public Transform DownArrow;
    public Transform RightArrow;

    public Vector3 upArrowDest = new Vector3(-38, 3.5f, 0);
    public Vector3 leftArrowDest = new Vector3(-38, 1f, 0);
    public Vector3 downArrowDest = new Vector3(-38, -1, 0);
    public Vector3 rightArrowDest = new Vector3(-38, -4, 0);

    public string fileName = "ExampleLevelArrow";
    public TextAsset myFile;
    char[] delimiters = { ' ', '\n' };
    
     

	// Use this for initialization
	void Start ()
    {
        string[] arrow_params;
        float currentSpawnTime = 0;
        float currentSpeed = 0;
        Arrow_Movement.arrowType currentDir = Arrow_Movement.arrowType.UP;
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
                    am.GetComponentInParent<SpriteRenderer>().enabled = true;
                    am.moving = true;
                }
            }
        }
    }
}
