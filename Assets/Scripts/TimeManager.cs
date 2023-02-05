using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public GameObject PlantManagerGO;
    
    public GameObject StartScreen,HowToScreen,About1Screen,About2Screen;
    
    public GameObject CanvasHolder, GodHolder,MoonHolder,DevilHolder,GrubHolder;

    public GameObject FailScreen, WinScreen, ScoreTextGO;
    public float timeLimit;
    public GameObject moonTextGO;

    public int[] DevilDamageScale, MaxGrubScale;
    
    public float[] GrubSpawnMinScale, GrubSpawnMaxScale, DevilMovementMinScale, DevilMovementMaxScale;

    float timeLeft;

    TextMeshProUGUI moonText, scoreText;

    PlantManager pm;

    GodMovement God;
    DevilMovement Devil;
    MoonRise Moon;
    GrubManager Grub;

    AudioSource mainMusic;

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
        mainMusic = GetComponent<AudioSource>();
        timeLeft = timeLimit;

        FailScreen.SetActive(false);
        WinScreen.SetActive(false);

        HowToScreen.SetActive(false);
        About1Screen.SetActive(false);
        About2Screen.SetActive(false);

        StartScreen.SetActive(true);
        Pause();       
    }

    float Scale()
    {
        float timeElapsed = timeLimit - timeLeft;

        float scaled = timeElapsed/(timeLimit*0.8f);

        if(scaled > 1)
        {
            scaled = 1;
        }

        return scaled;
    }

    int ScaledInt(int[] scalar)
    {
        float scaled = Scale();

        float diff = scalar[1] - scalar[0];

        float toAdd = diff * scaled;

        return Mathf.RoundToInt(scalar[0] + toAdd);
    }

    float ScaledFloat(float[] scalar, bool increasing)
    {
        float scaled = Scale();
        
        float diff;
        
        if(increasing)
        {
            diff = scalar[1] - scalar[0];
        }
        else
        {
            diff = scalar[0] - scalar[1];
        }

        float toAdd = diff * scaled;

        if (increasing)
        {
            return scalar[0] + toAdd;
        }
        else
        {
            return scalar[1] + toAdd;
        }
    }

    public void PlayGame()
    {
        StartScreen.SetActive(false);
        UnPause();

        mainMusic.Stop();
        mainMusic.Play();
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

        mainMusic.Pause();
    }

    public void UnPause()
    {
        unpaused = true;

        God.UnPause();
        Devil.UnPause();
        Moon.UnPause();
        Grub.UnPause();

        mainMusic.UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        if(unpaused)
        {
            timeLeft -= Time.deltaTime;

            SetMoonText();

            SetScalars();

            if(timeLeft <= 0)
            {
                GameEnd(true);
            }
        }
    }

    void SetScalars()
    {
        Devil.ChangeDamage(ScaledInt(DevilDamageScale));
        Devil.ChangeMinMovement(ScaledFloat(DevilMovementMinScale,false));
        Devil.ChangeMaxMovement(ScaledFloat(DevilMovementMaxScale,false));

        Grub.ChangeMaxGrubs(ScaledInt(MaxGrubScale));
        Grub.ChangeMinSpawn(ScaledFloat(GrubSpawnMinScale,false));
        Grub.ChangeMaxSpawn(ScaledFloat(GrubSpawnMaxScale,false));
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

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
