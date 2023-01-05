using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelLogic : MonoBehaviour
{
    public float XPEarned;

    private PlayerRage rageLogic;
    private PlayerAttack attackLogic;

    private float nextLevelXP; //Stores what the next levels required XP is
    public ParticleSystem levelUpVisualEffect;
    public int level;
    public TextMeshProUGUI levelInfo;
    
    public Slider satisfactionBar;
    public Slider rageBar;

    
    void Start()
    {
        XPEarned = 0;
        levelInfo.text = level.ToString();
        nextLevelXP = 50;
        rageLogic = GetComponent<PlayerRage>();
        attackLogic = GetComponent<PlayerAttack>();
    }

    public void IncreaseXP(float addition)
    {
        XPEarned += addition;
        CheckForLevelUp();
        satisfactionBar.value = XPEarned / nextLevelXP;
    }

    private void CheckForLevelUp()
    {
        if (XPEarned >= nextLevelXP)
        {
            XPEarned -= nextLevelXP;
            level++;
            levelInfo.text = level.ToString();
            nextLevelXP *= 1.5f;
            rageLogic.IncreaseStats(2,1.2f);
            PlayerRage.CurrentRage = 0;
            attackLogic.IncreaseAttackPower(1.3f);

            rageBar.maxValue = rageLogic.maxRage;
            
            levelUpVisualEffect?.Play();
        }
    }
}
