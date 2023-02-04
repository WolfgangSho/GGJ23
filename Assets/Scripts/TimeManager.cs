using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float timeLimit;
    public GameObject moonTextGO;

    float timeLeft;

    TextMeshProUGUI moonText;

    // Start is called before the first frame update
    void Start()
    {
        moonText = moonTextGO.GetComponent<TextMeshProUGUI>();
        timeLeft = timeLimit;
        
    }

    void SetMoonText()
    {
        moonText.text = TimeLeftString();
    }

    string TimeLeftString()
    {
        string s = "";

        int min = (int)timeLeft/60;
        int sec = (int)timeLeft%60;

        string extraZero = "";

        if(sec < 10)
        {
            extraZero = "0";
        }

        s += min + ":" + extraZero + sec;

        return s;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        SetMoonText();

        if(timeLeft <= 0)
        {
            //do something
        }


        
    }
}
