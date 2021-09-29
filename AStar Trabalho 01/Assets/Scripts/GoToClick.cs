using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToClick : MonoBehaviour
{
    public LayerMask hitLayers;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers)) ;
            {
                this.transform.position = hit.point;
            }
        }
    }
}