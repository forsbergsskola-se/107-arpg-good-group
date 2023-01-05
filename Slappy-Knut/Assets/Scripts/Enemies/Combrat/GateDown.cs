
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
        if (transform.position.y > 4f)
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
        if (!(transform.position.y < 4f)) return;
        //Should only run once
        FindObjectOfType<PlayerAttack>().GetComponent<CapsuleCollider>().isTrigger = true;
        canGateGoDown = false;
    }
}
