using UnityEngine;
using UnityEngine.UI;

public class SpeedSpell : MonoBehaviour
{
    public float maxCooldown = 10f;
    public float maxDuration = 3f;
    public float speedMultiplier = 2f;
    public Image cooldownImage;
    public Image inUseImage;
    public ParticleSystem speedUpParticle;

    private PlayerMotor _motor;
    private float _cooldown;
    private float _duration;
    private bool _spellActive;

    private void Start()
    {
        cooldownImage.fillAmount = _cooldown / maxCooldown;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) _cooldown = 0;
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && !PauseGame.IsPaused)
        {
            if (_cooldown <= 0 && _duration <= 0)
            {
                _duration = maxDuration;
                _motor = FindObjectOfType<PlayerMotor>();
                _spellActive = true;
                _motor.agent.speed *= speedMultiplier;
                Instantiate(speedUpParticle, _motor.transform);
            }
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
            _spellActive = false;
            _cooldown = maxCooldown;
        }
    }
}
