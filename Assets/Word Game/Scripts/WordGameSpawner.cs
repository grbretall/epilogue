using UnityEngine;
using System.Collections.Generic;

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

    WordContainer currentWord = new WordContainer();

    public Vector3 centerPoint;
    public float characterWidth;
    char[] delimiters = { ' ', '\n', '\t' };

    char[] currentLetterList = new char[1];

    public float letterStartTime;

    public float[] waveTimes;
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
	void FixedUpdate ()
    {
        checkWave();
        WordGameController[] characters = FindObjectsOfType<WordGameController>();
        //Checks all StdOffenseEnemy prefabs currently in the scene and checks
        //if it's their spawn time, their sprite renderer is off,
        //and they aren't moving.
        //If these conditions are met, the sprite renderer is turned on and moving is set to true;
        foreach (WordGameController character in characters)
        {
            if (character.GetComponentInParent<SpriteRenderer>().enabled == false)
            {
                if (character.spawnTime == Time.time)
                {
                    character.GetComponentInParent<SpriteRenderer>().enabled = true;
                    if (currentWord.spawnTime != Time.time || currentWord.spawnTime == 0)
                    {
                        currentWord.clear();
                        currentWord.setSpawnTime(character.spawnTime);
                        currentWord.add(character);
                        currentWord.setCurrentCharActive(true);
                        letterStartTime = Time.time;
                    }
                    else
                    {
                        currentWord.add(character);
                    }
                }
            }
        }
        if (currentWord.spawnTime != 0)
        {
            if (Time.time - letterStartTime >= currentWord.getCurrentChar().letterDuration && currentWord.currentWordPos < currentWord.getWordListSize()-1)
            {
				Debug.Log("Shifting to new letter");
                currentWord.setCurrentCharActive(false);
                currentWord.currentWordPos++;
                currentWord.setCurrentCharActive(true);
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

public class WordContainer
{
    List<WordGameController> wordList = new List<WordGameController>();
    public float spawnTime;
    public int currentWordPos;
    

    public WordContainer()
    {
        currentWordPos = 0;
        spawnTime = 0;
    }

    public void add(WordGameController newChar)
    {
        wordList.Add(newChar);
    }

    public void setSpawnTime(float spawnTime)
    {
        this.spawnTime = spawnTime;
    }

    public WordGameController getCurrentChar()
    {
        return wordList[currentWordPos];
    }

    public void setCurrentCharActive(bool state)
    {
        wordList[currentWordPos].currentlyActive = state;
    }

    public void clear()
    {
        foreach(WordGameController character in wordList)
        {
            character.destroy();
        }
        wordList.Clear();
    }

    public int getWordListSize()
    {
        return wordList.Count;
    }

}
