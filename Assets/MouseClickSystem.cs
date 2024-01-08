using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickSystem : MonoBehaviour
{
    public Camera cam;
    public LayerMask layerFilter;
    public bool systemEnabled = true;
    public float distance;
    public float camZPosition;
    private Vector2 currentMousePosition;
    private Vector3 currentMousePositionWorld;
    RaycastHit hit;


    private void Update()
    {
        if (!systemEnabled) return;
        currentMousePosition = Mouse.current.position.ReadValue();
        Vector3 direction = new Vector3(currentMousePosition.x, currentMousePosition.y, camZPosition);
        currentMousePositionWorld = cam.ScreenToWorldPoint(direction);
        
        Physics.Raycast(cam.transform.position, currentMousePositionWorld, out hit, distance, layerFilter);
        
        Debug.DrawRay(cam.transform.position, currentMousePositionWorld, Color.red);
    }

    public void CheckButton(Action<MainMenuState.ButtonType> OnComplete)
    {
        if (!systemEnabled) return;
        if (hit.transform == null)
        {
            OnComplete.Invoke(MainMenuState.ButtonType.None);
            return;
        }
        if (hit.transform != null && hit.transform.gameObject.CompareTag("Button"))
            OnComplete?.Invoke(hit.transform.GetComponent<ButtonData>().buttonID);
        else OnComplete?.Invoke(MainMenuState.ButtonType.None);
        Debug.Log(hit.transform.gameObject.tag);
    }
}
