using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelLogic : MonoBehaviour
{
    private float XPEarned;

    private PlayerRage rageLogic;
    private PlayerAttack attackLogic;

    private float nextLevelXP; //Stores what the next levels required XP is
    public ParticleSystem levelUpVisualEffect;
    public int level;

    
    // Start is called before the first frame update
    void Start()
    {
        XPEarned = 0;
        level = 0;
        nextLevelXP = 100;
        rageLogic = GetComponent<PlayerRage>();
        attackLogic = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseXP(float addition)
    {
        XPEarned += addition;
        checkForLevelUp();
    }

    private void checkForLevelUp()
    {
        if (XPEarned > nextLevelXP)
        {
            XPEarned -= nextLevelXP;
            level++;
            nextLevelXP = nextLevelXP * 1.5f;
            rageLogic.IncreaseStats(2,1.2f);
            attackLogic.IncreaseAttackPower(1.3f);
            levelUpVisualEffect?.Play();
        }
    }
}
