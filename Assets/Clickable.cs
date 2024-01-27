using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    private Camera cam;
    private Ray _ray;
    private RaycastHit _hit;
    public LayerMask clickable;
    void Start() {
        cam = Camera.main;
    }
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        _ray = Camera.main.ScreenPointToRay(mousePos);
        if (Input.GetMouseButtonDown(0)) {
        if(Physics.Raycast(_ray, out _hit, 10000f)) {
            Debug.Log(_hit.transform.gameObject.name);
        }
        }
    }
}
