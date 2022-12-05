using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NPC : MonoBehaviour
{
    public bool canFlee;
    public bool canAttack;

    public float movementSpeed;
    public float attackSpeed;
    //public Item[] loot; TODO: uncomment this when items are finished

    private Vector3 targetPosition;
    private Rigidbody rigidbody;
    private Vector3 rigidbodyVelocity;
    Random rand = new Random();
    
    
    // Start is called before the first frame update
    void Start()
    {
        RandomizeValues();
        rigidbody = GetComponent<Rigidbody>();
        rigidbodyVelocity = rigidbody.velocity;
        Roam();
    }

    // Update is called once per frame
    void Update()
    {
        moveTowardsPositon();
    }

    void Flee()
    {
        
    }

    void Roam()
    {
        //Somehow we need to find a position to go to here....
        float x = rand.Next(-10, 10);
        float z = rand.Next(-10, 10);
        targetPosition = new Vector3(x, 0.5f, z);
        
        
        
    }

    void Attack()
    {
        
    }

    void moveTowardsPositon()
    {
        Vector3.SmoothDamp(transform.position, targetPosition, ref rigidbodyVelocity, 50);
        float xDiff = targetPosition.x - transform.position.x;
        float yDiff = targetPosition.y - transform.position.y;
        float zDiff = targetPosition.z - transform.position.z;
        if ((xDiff + yDiff + zDiff) < 1)
        {
            Roam();
        }
    }

    void RandomizeValues()
    {
        //set default values
        canAttack = false;
        canFlee = true;
        
        //Randomise values
        //Random rand = new Random();
        if (rand.NextDouble() > 0.8)
        {
            canAttack = true;
        }
        if (rand.NextDouble() > 0.8)
        {
            canFlee = false;
        }
    }

    void DropLoot()
    {
        
    }
}
