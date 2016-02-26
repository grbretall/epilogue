using UnityEngine;
using System.Collections;

public class WordGameSpawner : MonoBehaviour
{
    //Transforms to hold all the different prefabs for each letter
    public Transform letterA;
    public Transform letterB;
    public Transform letterC;
    public Transform letterD;
    public Transform letterE;
    public Transform letterF;
    public Transform letterG;
    public Transform letterH;
    public Transform letterI;
    public Transform letterJ;
    public Transform letterK;
    public Transform letterL;
    public Transform letterM;
    public Transform letterN;
    public Transform letterO;
    public Transform letterP;
    public Transform letterQ;
    public Transform letterR;
    public Transform letterS;
    public Transform letterT;
    public Transform letterU;
    public Transform letterV;
    public Transform letterW;
    public Transform letterX;
    public Transform letterY;
    public Transform letterZ;

    public Vector3 centerPoint;
    public float characterWidth;
    char[] delimiters = { ' ', '\n' };

    char[] currentLetterList = new char[1];

    public TextAsset myFile;
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
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //Parses the input file and 
    void parseFile()
    {
        double currentSpawnTime = 0;
        double currentLetterDuration = 0;

        float currentXPos = 0;
        Vector3 currentLetterPosition = Vector3.zero;
        string[] word_params;

        try
        {
            word_params = myFile.text.Split(delimiters);
            for(int i = 0; i < word_params.Length; i++)
            {
                if(i % 3 == 0)
                {
                    currentLetterList = word_params[i].ToCharArray();
                    
                }
                else if(i % 3 == 1)
                {
                    currentSpawnTime = double.Parse(word_params[i]);
                }
                else
                {
                    currentLetterDuration = double.Parse(word_params[i]);
                    if (currentLetterList.Length % 2 == 1)
                    {
                        currentXPos = centerPoint.x - ((currentLetterList.Length / 2) * characterWidth);
                    }
                    else
                    {
                        currentXPos = centerPoint.x - ((currentLetterList.Length / 2) * characterWidth) + (characterWidth/2);
                    }

                    foreach(char c in currentLetterList)
                    {
                        currentLetterPosition = new Vector3(currentXPos, centerPoint.y, centerPoint.z);

                        if(c == 'a')
                            Instantiate(letterA).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if(c == 'b')
                            Instantiate(letterB).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'c')
                            Instantiate(letterC).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'd')
                            Instantiate(letterD).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'e')
                            Instantiate(letterE).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'f')
                            Instantiate(letterF).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'g')
                            Instantiate(letterG).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'h')
                            Instantiate(letterH).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'i')
                            Instantiate(letterI).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'j')
                            Instantiate(letterJ).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'k')
                            Instantiate(letterK).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'l')
                            Instantiate(letterL).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'm')
                            Instantiate(letterM).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'n')
                            Instantiate(letterN).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'o')
                            Instantiate(letterO).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'p')
                            Instantiate(letterP).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'q')
                            Instantiate(letterQ).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'r')
                            Instantiate(letterR).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 's')
                            Instantiate(letterS).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 't')
                            Instantiate(letterT).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'u')
                            Instantiate(letterU).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'v')
                            Instantiate(letterV).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'w')
                            Instantiate(letterW).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'x')
                            Instantiate(letterX).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'y')
                            Instantiate(letterY).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);
                        else if (c == 'z')
                            Instantiate(letterZ).GetComponent<WordGameController>().initialize(currentLetterPosition, c, (float)currentSpawnTime, (float)currentLetterDuration);

                        currentXPos += characterWidth;
                    }
                }
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
