using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private CountDown countDown;
    public SnapPoint[] snapPoints;

    [Header("Completion States")]
    public bool NoseCheck;
    public bool HatCheck;
    public bool EyeLCheck;
    public bool EyeRCheck;


    void Awake()
    {
        countDown   = FindObjectOfType<CountDown>();
        snapPoints  = GetComponentsInChildren<SnapPoint>();
        foreach(SnapPoint Snap in snapPoints)
        {
            if(Snap.Empty == true)
            {
                if(Snap.Part == "Nose")  NoseCheck = true;
                if(Snap.Part == "Hat")   HatCheck  = true;
                if(Snap.Part == "EyeL")  EyeLCheck = true;
                if(Snap.Part == "EyeR")  EyeRCheck = true;
            }
        }
    }

    public void WinCheck()
    {
        if(NoseCheck && HatCheck && EyeLCheck && EyeRCheck)
        {
            countDown.CountDownTimer = 0;
            Win();
        }
        else
        {
            Lose();
        }
    }

    public void Win()
    {
        Debug.Log("Meow :)");
    }
    public void Lose()
    {
        Debug.Log("Meow >:(");
    }
}
