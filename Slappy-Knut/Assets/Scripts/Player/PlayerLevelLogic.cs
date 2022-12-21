using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLevelLogic : MonoBehaviour
{
    public float XPEarned;

    private PlayerRage rageLogic;
    private PlayerAttack attackLogic;

    private float nextLevelXP; //Stores what the next levels required XP is
    public ParticleSystem levelUpVisualEffect;
    public int level;
    
    public Slider satisfactionBar;

    
    // Start is called before the first frame update
    void Start()
    {
        XPEarned = 0;
        level = 0;
        nextLevelXP = 100;
        rageLogic = GetComponent<PlayerRage>();
        attackLogic = GetComponent<PlayerAttack>();
        GameObject XPSlider = GameObject.Find("SatisfactionBar");
        satisfactionBar = XPSlider.GetComponent<Slider>();
        IncreaseXP(50);
    }

    // Update is called once per frame
    void Update()
    {
        //IncreaseXP(1*Time.deltaTime);
    }

    public void IncreaseXP(float addition)
    {
        XPEarned += addition;
        checkForLevelUp();
        satisfactionBar.value = XPEarned / nextLevelXP;
    }

    private void checkForLevelUp()
    {
        if (XPEarned > nextLevelXP)
        {
            XPEarned -= nextLevelXP;
            level++;
            nextLevelXP = nextLevelXP * 1.5f;
            rageLogic.IncreaseStats(2,1.2f);
            PlayerRage.CurrentRage = 0;
            attackLogic.IncreaseAttackPower(1.3f);
            levelUpVisualEffect?.Play();
        }
    }
}
