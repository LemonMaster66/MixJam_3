using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [Header("Properties")]
    public string Part;
    public string Variant;

    [Header("States")]
    public bool Dragging   = false;
    public bool Snapped    = false;
    public bool HasSnapped = false;
    public bool Resetting  = false;

    [Header("Smooth Transforms")]
    public float TargetScale;
    public Vector3 TargetPosition;
    public Quaternion TargetRotation;
    public Vector3 OriginalPos;

    //Debug Stats
    private GameObject SnappedPoint;
    private Vector3 offset;
    private SnapPoint[] snapPoints;

    void Awake()
    {
        Snapped    = false;
        HasSnapped = false;
        Dragging   = false;

        transform.localScale = new Vector3(0,0,0);
        TargetPosition = transform.position;
        TargetScale    = 2f;
        OriginalPos = transform.position;
    }
    

    void Update()
    {
        if(Resetting)
        {
            TargetScale = 0;
            if(transform.localScale.magnitude <= new Vector3(0.05f,0.05f,0.05f).magnitude)
            {
                Destroy(this.gameObject);
            }
        }

        if(Dragging)
        {
            TargetPosition = transform.position;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            foreach(SnapPoint Snap in snapPoints)
            {
                if(Vector3.Distance(transform.position, Snap.transform.position) < Snap.Distance) Snap.TargetAlpha = 0.1f;
                else Snap.TargetAlpha = 0f;
            }
        }
        if(SnappedPoint != null) TargetPosition = SnappedPoint.transform.position;

        transform.position    = Vector3.Slerp(transform.position, TargetPosition, 0.4f);
        transform.rotation    = Quaternion.Slerp(transform.rotation, TargetRotation, 0.3f);
        transform.localScale  = Vector3.Slerp(transform.localScale, new Vector3(TargetScale,TargetScale,TargetScale), 0.3f);
    }

    void OnMouseEnter()
    {
        if(Snapped) TargetScale = 1.05f;
        else TargetScale = 2.05f;
    }
    void OnMouseExit()
    {
        if(Snapped) TargetScale = 1f;
        else TargetScale = 2f;
    }

    void OnMouseDown()
    {
        Dragging = true;
        Snapped  = false;

        SnappedPoint     = null;
        transform.parent = null;

        snapPoints = FindObjectsOfType<SnapPoint>();
    
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TargetRotation = Quaternion.Euler(0,0,0);
        TargetScale = 2.2f;
    }

    void OnMouseUp()
    {
        Dragging = false;
        TargetScale = 2.05f;

        foreach(SnapPoint Snap in snapPoints)
        {
            //Snapped
            if(Vector3.Distance(transform.position, Snap.transform.position) < Snap.Distance)
            {
                if(!HasSnapped) Instantiate(this, OriginalPos, Quaternion.identity);

                Snapped      = true;
                HasSnapped   = true;
                SnappedPoint = Snap.gameObject;

                transform.parent  = Snap.transform;
                TargetPosition    = Snap.transform.position;
                TargetRotation    = Snap.transform.rotation;
                TargetScale       = 1.05f;
            }
        }
    }
}