using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject ActiveCat;

    public float CountDownTimer = 10;
    public float PropResetTimer = 10;

    private UIManager uIManager;

    void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    private void CountDownFunction()
    {
        CountDownTimer -= Time.deltaTime;
        if(CountDownTimer <= 0f) ActiveCat.GetComponent<Cat>().Lose();
        return;
    }
    private void PropResetFunction()
    {
        CountDownTimer -= Time.deltaTime;
        if(CountDownTimer <= 0f) ActiveCat.GetComponent<Cat>().Lose();
        return;
    }

    void Update()
    {
        if(CountDownTimer >= 0) CountDownFunction();
        if(CountDownTimer >= 0) PropResetFunction();
    }
}
