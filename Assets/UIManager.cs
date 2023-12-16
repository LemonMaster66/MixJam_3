using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    public void ResetProps()
    {
        Drag[] drags = FindObjectsOfType<Drag>();
        foreach(Drag drag in drags)
        {
            if(drag.transform.position != drag.OriginalPos)
            {
                if(!drag.HasSnapped) Instantiate(drag.gameObject, drag.OriginalPos, Quaternion.identity);
                drag.Resetting = true;
            }
        }

        SnapPoint[] snapPoints = FindObjectsOfType<SnapPoint>();
        foreach(SnapPoint Snap in snapPoints) Snap.TargetAlpha = 0;
    }
}