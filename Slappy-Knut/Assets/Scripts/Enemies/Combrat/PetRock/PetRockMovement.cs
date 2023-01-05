
using UnityEngine;

public class PetRockMovement : MonoBehaviour
{
    private LineRenderer _line;
    private Combrat _combrat;
    private Rigidbody _rb;
    private bool _isPetDead;
    public Transform combratHand;
    
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _combrat = FindObjectOfType<Combrat>();
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (!_isPetDead)
        {
            Collar();
            FollowCombrat();
        }
    }

    private void Collar()
    {
       _line.SetPosition(0, transform.position);
       _line.SetPosition(1, combratHand.transform.position);
    }

    void FollowCombrat()
    {
        // PetRock goes to x pos and y 0 to be grounded and z+5 to hide behind combrat
        var position = _combrat.transform.position;
        if (Vector3.Distance(_rb.position, new Vector3(position.x, 0, position.z+5)) < 5f) 
        {
            //if petRock reaches desired pos do nothing but wait
        }
        else
            _rb.MovePosition(Vector3.MoveTowards(_rb.position, new Vector3(position.x, 0, position.z+5), 4f * Time.deltaTime));
        
    }

    public void PetIsDead()
    {
        _isPetDead = true;
        _line.enabled = false;
    }
}

