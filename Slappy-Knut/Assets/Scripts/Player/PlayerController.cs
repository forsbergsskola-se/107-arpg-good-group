using UnityEngine.EventSystems;
using UnityEngine;
//Control the player. Here we choose our "focus" and where to move
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus; //our current focus: Item
    public LayerMask walkableLayer; //Filter out everything not walkable
    public LayerMask interactableLayer;
    public int maxRayCastDistance = 100;
    
    private PlayerMotor _motor; //Reference to our motor
    private Animator _animator;
    private PlayerAudioManager _audioManager;
    public static bool MouseHeld;
    public static float TimeHeld = 1;
    
    // Start is called before the first frame update
    //Use this for initialization

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _audioManager = GetComponent<PlayerAudioManager>();
        _animator = GetComponent<Animator>();
    }
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
             Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hitInfo;
             //If the ray hits
             if (Physics.Raycast(rayOrigin, out hitInfo, maxRayCastDistance, walkableLayer))
             {
                 //Move our player to what we hit
                 _motor.MoveToPoint(hitInfo.point);
                 //Stop focusing any object
                 RemoveFocus();
             }
         }
        MouseHeld = Input.GetMouseButton(1);
        if (MouseHeld)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //Getting either item on focus or enemy to walk towards them
            if (Physics.Raycast(rayOrigin, out hitInfo, maxRayCastDistance, interactableLayer))
            {
                //Check if we hit and interactable
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                //If we did, set it as our focus
                if (Vector3.Distance(hitInfo.transform.position, transform.position) < Weapon.CurrEquippedWeapon.Range)
                {
                    TimeHeld += Time.deltaTime;
                } 
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if(TimeHeld -1 > Weapon.CurrEquippedWeapon.ChargeTime || !MouseHeld) AttackRelease();
        _animator.SetBool("isRunning", _motor.agent.velocity.magnitude >= .5);
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
            _motor.FollowTarget(newFocus);
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
        _motor.StopFollowingTarget();
    }
    public void PlayStepSound() //called as an event in the animator
    {
        _audioManager.AS_FootSteps.Play();
    }
    public void AttackRelease()
    {
        _animator.speed = 1;
        TimeHeld = 1;
    }
}
