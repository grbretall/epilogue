using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour
{
    public int shield_Game_Score = 0;
    public int rhythm_Game_Score = 0;

    public double rhythm_Game_Score_Mult = 1;
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void updateShieldGameScore(int scoreMod)
    {
        shield_Game_Score += scoreMod;
    }

    public void updateRhythmGameScore(int scoreMod, double scoreMultMod)
    {
        rhythm_Game_Score_Mult += scoreMultMod;
        rhythm_Game_Score += (scoreMod * (int)rhythm_Game_Score_Mult);
    }

    public void resetRhythmGameCombo()
    {
        rhythm_Game_Score_Mult = 1;
    }
}
