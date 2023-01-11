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

    
    void Start()
    {
        _satisfactionBar = GameObject.FindGameObjectWithTag("SatisfactionBar");
        _slider = _satisfactionBar.GetComponent<Slider>();
        _levelInfo = _satisfactionBar.GetComponentInChildren<TextMeshProUGUI>();
        XPEarned = 0;
        _levelInfo.text = level.ToString();
        nextLevelXP = 50;
        rageLogic = GetComponent<PlayerRage>();
        attackLogic = GetComponent<PlayerAttack>();
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
