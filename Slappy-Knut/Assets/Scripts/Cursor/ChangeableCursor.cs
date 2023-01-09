using Interfaces;
using UnityEngine;

public class ChangeableCursor : MonoBehaviour
{
    public LayerMask interactableLayer;
    public int maxRayCastDistance = 100;
    public Texture2D attackCursor;
    public Texture2D interactableCursor;

    private Vector2 _attackOffset = new (14,2);
    private Vector2 _interactableOffset = new (19,6);

    


    // Update is called once per frame
    void Update()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //Getting either item on focus or enemy to walk towards them
        if (Physics.Raycast(rayOrigin, out hitInfo, maxRayCastDistance, interactableLayer) && !PauseGame.IsPaused)
        {
            IDamagable damageicon = hitInfo.collider.GetComponent<IDamagable>();
            if (damageicon != null)
            {
                Cursor.SetCursor(attackCursor, _attackOffset, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(interactableCursor, _interactableOffset, CursorMode.Auto);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
