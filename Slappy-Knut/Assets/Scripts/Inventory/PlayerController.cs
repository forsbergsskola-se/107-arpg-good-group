using UnityEngine.EventSystems;
using UnityEngine;

//Control the player. Here we choose our "focus" and where to move

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus; //our current focus: Item
    
    public LayerMask movementMask; //Filter out everything not walkable
    
    private PlayerMotor motor; //Reference to our motor
    
    // Start is called before the first frame update
    //Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        //If we press the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            //We create a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If the ray hits
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Move our player to what we hit
                motor.MoveToPoint(hit.point);
                
                //Stop focusing any object
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check if we hit and interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                //If we did, set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        
        focus = null;
        motor.StopFollowingTarget();
    }
}
