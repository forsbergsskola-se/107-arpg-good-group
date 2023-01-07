
using UnityEngine;

public class GateDown : MonoBehaviour
{
    public bool canGateGoDown;
    void Update()
    {
     if(canGateGoDown)   
         SandGateDown();
    }

    void SandGateDown()
    {
        // Makes the sandcastles gate come down until it reaches 4f then we change players collider to trigger again so he cant walk through the gate until its down
        //canGateDown then changes to false so its only called once
        if (transform.position.y > 4f)
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
        if (!(transform.position.y < 4f)) return;
        //Should only run once
        FindObjectOfType<PlayerAttack>().GetComponent<CapsuleCollider>().isTrigger = true;
        canGateGoDown = false;
    }
}
