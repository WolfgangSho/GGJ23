using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject PlantManagerGO;
    
    public GameObject StartScreen,HowToScreen,About1Screen,About2Screen;
    
    public GameObject CanvasHolder, GodHolder,MoonHolder,DevilHolder,GrubHolder;

    public GameObject FailScreen, WinScreen, ScoreTextGO;
    public float timeLimit;
    public GameObject moonTextGO;

    float timeLeft;

    TextMeshProUGUI moonText, scoreText;

    PlantManager pm;

    GodMovement God;
    DevilMovement Devil;
    MoonRise Moon;
    GrubManager Grub;

    bool unpaused;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlantManagerGO.GetComponent<PlantManager>();

        God = GodHolder.GetComponent<GodMovement>();
        Devil = DevilHolder.GetComponent<DevilMovement>();
        Moon = MoonHolder.GetComponent<MoonRise>();
        Grub = GrubHolder.GetComponent<GrubManager>();

        moonText = moonTextGO.GetComponent<TextMeshProUGUI>();
        scoreText = ScoreTextGO.GetComponent<TextMeshProUGUI>();
        timeLeft = timeLimit;

        FailScreen.SetActive(false);
        WinScreen.SetActive(false);

        HowToScreen.SetActive(false);
        About1Screen.SetActive(false);
        About2Screen.SetActive(false);

        StartScreen.SetActive(true);
        Pause();       
    }

    public void PlayGame()
    {
        StartScreen.SetActive(false);
        UnPause();
    }

    public void BackButton()
    {
        HowToScreen.SetActive(false);
        About1Screen.SetActive(false);
        About2Screen.SetActive(false);
    }

    public void HowToButton()
    {
        HowToScreen.SetActive(true);
    }

    public void AboutButton()
    {
        About1Screen.SetActive(true);
    }

    public void AboutBack()
    {
        About2Screen.SetActive(false);
    }

    public void AboutNext()
    {
        About2Screen.SetActive(true);
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

    public void Pause()
    {
        unpaused = false;

        God.Pause();
        Devil.Pause();
        Moon.Pause();
        Grub.Pause();
    }

    public void UnPause()
    {
        unpaused = true;

        God.UnPause();
        Devil.UnPause();
        Moon.UnPause();
        Grub.UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(unpaused)
        {
            timeLeft -= Time.deltaTime;

            SetMoonText();

            if(timeLeft <= 0)
            {
                GameEnd(true);
            }
        }
    }

    public void GameEnd(bool win)
    {
        Pause();

        if(win)
        {
            scoreText.text = pm.GetScore() + "%";
            WinScreen.SetActive(true);

        }
        else
        {
            FailScreen.SetActive(true);
        }
    }
}
