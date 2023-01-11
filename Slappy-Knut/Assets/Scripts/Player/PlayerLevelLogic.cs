using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelLogic : MonoBehaviour
{
    public float XPEarned;

    private PlayerRage rageLogic;
    private PlayerAttack attackLogic;

    private float nextLevelXP; //Stores what the next levels required XP is
    public ParticleSystem levelUpParticle;
    public int level;
    
    private GameObject _satisfactionBar;
    private TextMeshProUGUI _levelInfo;
    private Slider _slider;
    private LevelInformation _levelInformation;

    
    void Start()
    {
        _satisfactionBar = GameObject.FindGameObjectWithTag("SatisfactionBar");
        _slider = _satisfactionBar.GetComponent<Slider>();
        _levelInfo = _satisfactionBar.GetComponentInChildren<TextMeshProUGUI>();
        _levelInfo.text = level.ToString();
        _levelInformation = _satisfactionBar.GetComponent<LevelInformation>();
        level = _levelInformation.CurrentLevel;
        nextLevelXP = _levelInformation.nextLevelXP;
        XPEarned = _levelInformation.CurrentXP;
        rageLogic = GetComponent<PlayerRage>();
        attackLogic = GetComponent<PlayerAttack>();
        _slider.value = XPEarned / nextLevelXP;
        _levelInfo.text = level.ToString();

        rageLogic.IncreaseStats(MathF.Pow(2, level), MathF.Pow(1.2f, level));
    }

    public void IncreaseXP(float addition)
    {
        XPEarned += addition;
        CheckForLevelUp();
        _slider.value = XPEarned / nextLevelXP;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            XPEarned = nextLevelXP;
            CheckForLevelUp();
            
        }
        _levelInformation.CurrentLevel = level;
        _levelInformation.CurrentXP = XPEarned;
        _levelInformation.nextLevelXP = nextLevelXP;
    }

    private void CheckForLevelUp()
    {
        if (XPEarned >= nextLevelXP)
        {
            XPEarned -= nextLevelXP;
            level++;
            _levelInfo.text = level.ToString();
            nextLevelXP *= 1.5f;
            rageLogic.IncreaseStats(2,1.2f);
            PlayerRage.CurrentRage = 0;
            attackLogic.IncreaseAttackPower(1.3f);
            

            rageLogic.rageBar.maxValue = rageLogic.maxRage;
            
            Instantiate(levelUpParticle, transform);
        }
    }
}
