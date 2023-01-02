using UnityEngine;

public class SpeedSpell : MonoBehaviour
{
    public float maxCoolDown = 10f;
    public float maxDuration = 3f;
    public float speedMultiplier = 2f;

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
    }

    private void CastSpell()
    {
        _duration -= Time.deltaTime;
        if (_duration < 0)
        {
            _motor.agent.speed = _movementSpeed;
            _duration = maxDuration;
            _spellActive = false;
            _coolDown = maxCoolDown;
        }
    }
}
