using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour
{
    public int shield_Game_Score = 0;

    public int rhythm_Game_Score = 0;
    public double rhythm_Game_Score_Mult = 1;

    public int offense_Game_Damage = 0;
    public int offense_Game_Knockouts = 0;
    public int offense_Game_Enemy_Kills = 0;
    public int offense_Game_Civilian_Kills = 0;

    public int wordGameScore = 0;

	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void updateShieldGameScore(int scoreMod)
    {
        shield_Game_Score += scoreMod;
    }

    public void updateOffenseGameDamage(int damage)
    {
        offense_Game_Damage += damage;
    }

    public void updateOffenseGameKnockouts(int knockouts)
    {
        offense_Game_Knockouts += knockouts;
    }

    public void updateOffenseGameEnemyKills(int enemyKills)
    {
        offense_Game_Enemy_Kills += enemyKills;
    }

    public void updateOffenseGameCivilianKills(int civilianKills)
    {
        offense_Game_Civilian_Kills += civilianKills;
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

    public void updateWordGameScore(int scoreMod)
    {
        wordGameScore += scoreMod;
    }


}
