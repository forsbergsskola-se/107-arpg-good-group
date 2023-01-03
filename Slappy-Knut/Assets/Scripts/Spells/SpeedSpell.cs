using UnityEngine;
using UnityEngine.UI;

public class SpeedSpell : MonoBehaviour
{
    public float maxCooldown = 10f;
    public float maxDuration = 3f;
    public float speedMultiplier = 2f;
    public Image cooldownImage;
    public Image inUseImage;

    private PlayerMotor _motor;
    private float _cooldown;
    private float _duration;
    private bool _spellActive;

    private void Start()
    {
        _duration = maxDuration;
        cooldownImage.fillAmount = _cooldown / maxCooldown;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && _cooldown <= 0)
        {
            _motor = FindObjectOfType<PlayerMotor>();
            _spellActive = true;
            _motor.agent.speed *= speedMultiplier;
        }
        
        if (_spellActive) CastSpell();
        
        if (_cooldown < 0) _cooldown = 0;
        else
        {
            _cooldown -= Time.deltaTime;
            cooldownImage.fillAmount = _cooldown / maxCooldown;
        }
    }

    private void CastSpell()
    {
        _duration -= Time.deltaTime;
        inUseImage.color = Color.cyan;
        if (_duration < 0)
        {
            inUseImage.color = Color.white;
            _motor.agent.speed /= speedMultiplier;
            _duration = maxDuration;
            _spellActive = false;
            _cooldown = maxCooldown;
        }
    }
}
