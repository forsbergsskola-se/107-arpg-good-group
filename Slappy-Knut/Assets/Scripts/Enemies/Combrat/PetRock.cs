using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRock : MonoBehaviour
{
    private LineRenderer _line;
    private Combrat _combrat;
    public Transform combratHand;
    
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _combrat = FindObjectOfType<Combrat>();
    }

    // Update is called once per frame
    void Update()
    {
        Collar();
    }

    void Collar()
    {
       _line.SetPosition(0, transform.position);
       _line.SetPosition(1, combratHand.transform.position);
    }
}

