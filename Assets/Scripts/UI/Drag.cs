using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public bool Dragging = false;
    private Vector3 offset;
    public SnapPoint[] snapPoints;

    public float TargetScale;
    

    void Update()
    {
        if(Dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            foreach(SnapPoint Snap in snapPoints)
            {
                if(Vector3.Distance(transform.position, Snap.transform.position) < Snap.Distance) Snap.TargetAlpha = 0.1f;
                else Snap.TargetAlpha = 0f;
            }
        }

        
        transform.localScale  = Vector3.Slerp(transform.localScale, new Vector3(TargetScale,TargetScale,TargetScale), 0.3f);
    }

    void OnMouseDown()
    {
        Dragging = true;
        transform.parent = null;

        snapPoints = FindObjectsOfType<SnapPoint>();
    
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        TargetScale = 2f;
    }

    void OnMouseUp()
    {
        Dragging = false;

        TargetScale = 1.8f;

        foreach(SnapPoint Snap in snapPoints)
        {
            if(Vector3.Distance(transform.position, Snap.transform.position) < Snap.Distance)
            {
                transform.position = Snap.transform.position;
                transform.parent = Snap.transform;
                TargetScale = 1f;
            }
        }
    }
}