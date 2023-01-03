using UnityEngine;
using UnityEngine.UI;

public class SpeedSpell : MonoBehaviour
{
    public float maxCoolDown = 10f;
    public float maxDuration = 3f;
    public float speedMultiplier = 2f;
    public Image coolDownImage;
    public Image inUseImage;

    private PlayerMotor _motor;
    private float _coolDown;
    private float _duration;
    private float _movementSpeed;
    private bool _spellActive;

    private void Start()
    {
        _motor = FindObjectOfType<PlayerMotor>();
        _movementSpeed = _motor.agent.speed;
        _duration = maxDuration;
        coolDownImage.fillAmount = _coolDown / maxCoolDown;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && _coolDown <= 0)
        {
            _spellActive = true;
            _motor.agent.speed *= speedMultiplier;
        }
        
        if (_spellActive) CastSpell();
        
        if (_coolDown < 0) _coolDown = 0;
        else _coolDown -= Time.deltaTime;
        coolDownImage.fillAmount = _coolDown / maxCoolDown;
    }

    private void CastSpell()
    {
        _duration -= Time.deltaTime;
        inUseImage.color = Color.cyan;
        if (_duration < 0)
        {
            inUseImage.color = Color.white;
            _motor.agent.speed = _movementSpeed;
            _duration = maxDuration;
            _spellActive = false;
            _coolDown = maxCoolDown;
        }
    }
}
