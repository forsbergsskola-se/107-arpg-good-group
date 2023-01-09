using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class ChangeableCursor : MonoBehaviour
{
    public LayerMask interactableLayer;
    public int maxRayCastDistance = 100;
    public Texture2D attackCursor;
    public Texture2D interactableCursor;

    private Vector2 _attackOffset;
    private Vector2 _interactableOffset;

    private void Start()
    {
        _attackOffset = new Vector2(attackCursor.width / 2f, attackCursor.height / 2f);
        _interactableOffset = new Vector2(interactableCursor.width / 2f, interactableCursor.height / 2f);
    }


    // Update is called once per frame
    void Update()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //Getting either item on focus or enemy to walk towards them
        if (Physics.Raycast(rayOrigin, out hitInfo, maxRayCastDistance, interactableLayer))
        {
            IDamagable damageicon = hitInfo.collider.GetComponent<IDamagable>();
            if (damageicon != null)
            {
                Debug.Log("Mouse on damageable");
                Cursor.SetCursor(attackCursor, _attackOffset, CursorMode.Auto);
            }
            else
            {
                Debug.Log("Mouse on interactable");
                Cursor.SetCursor(interactableCursor, _interactableOffset, CursorMode.Auto);
            }
        }
        else
        {
            Debug.Log("Default cursor");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
