using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentLevel = 0;
    public float CurrentXP = 0;
    public float nextLevelXP = 50;
    void Awake()
    {
        nextLevelXP = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
