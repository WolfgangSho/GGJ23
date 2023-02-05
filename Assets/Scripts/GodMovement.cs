using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GodMovement : MonoBehaviour
{
    public GameObject PlantsGO;

    public GameObject GrubsGO;

    public GameObject audioHealGO;
    
    public int healAmount;

    public float healTick;

    public float healDuration;

    public int maxHeals;

    public int startingHeals;
    
    public float moveAmount;

    public float moveRepeatSeconds;

    public int centreIndex;

    public int maxIndex;

    int godIndex;

    bool ableToMove;

    int healsLeft;

    float moveTimer, healTimer, powerTimer;

    PlantManager pm;

    GrubManager grub;

    GameObject particles,healsTestGO;

    TextMeshProUGUI healsText;

    AudioSource audioHeal;

    bool unpaused;

    

    // Start is called before the first frame update
    void Start()
    {
        pm = PlantsGO.GetComponent<PlantManager>();
        grub = GrubsGO.GetComponent<GrubManager>();
        particles = transform.GetChild(0).gameObject;
        healsTestGO = transform.GetChild(1).GetChild(0).gameObject;
        healsText = healsTestGO.GetComponent<TextMeshProUGUI>();
        audioHeal = audioHealGO.GetComponent<AudioSource>();
        particles.SetActive(false);

        godIndex = centreIndex;
        ableToMove = true;
        healsLeft = startingHeals;

        UpdatePosition();
        UpdateHealsText();
    }

    // Update is called once per frame
    void Update()
    {
        if(unpaused)
        {
            float direction = Input.GetAxisRaw("Horizontal");

            if(ableToMove && powerTimer == 0)
            {
                if(direction > 0.5f)
                {
                    moveGod(false);
                }
                else if (direction < -0.5f)
                {
                    moveGod(true);
                }
            }

            if(moveTimer > 0)
            {
                moveTimer-= Time.deltaTime;

                if(moveTimer <= 0)
                {
                    ableToMove = true;
                    moveTimer = 0;
                }
            }

            if(Input.GetButtonDown("Fire1") && !particles.activeSelf && healsLeft > 0)
            {
                particles.SetActive(true);
                powerTimer = healDuration;

                healsLeft += grub.killGrub(godIndex);

                if(healsLeft > maxHeals)
                {
                    healsLeft = maxHeals;
                }

                UpdateHealsText();

                audioHeal.Stop();
                audioHeal.Play();
            }

            if(healTimer == 0 && powerTimer > 0)
            {
                pm.AlterPlantXP(godIndex,healAmount);
                healTimer = healTick;
                
            }
            else
            {
                healTimer -= Time.deltaTime;

                if(healTimer < 0)
                {
                    healTimer = 0;
                }
            }

            powerTimer -= Time.deltaTime;

            if(powerTimer <= 0)
            {
                powerTimer = 0;
                particles.SetActive(false);
            }
        }
    }

    void moveGod(bool left)
    {
        ableToMove = false;
        moveTimer = moveRepeatSeconds;

        if(left)
        {
            godIndex--;

            if(godIndex < 0)
            {
                godIndex = 0;
            }
        }
        else
        {
            godIndex++;

            if(godIndex > maxIndex)
            {
                godIndex = maxIndex;
            }
        }

        UpdatePosition();
    }

    void UpdatePosition()
    {
        float pos = godIndex * moveAmount;
        transform.localPosition = new Vector3(pos,0,0);
    }

    void UpdateHealsText()
    {
        healsText.SetText(healsLeft.ToString());
    }

    public void Pause()
    {
        unpaused = false;
    }

    public void UnPause()
    {
        unpaused = true;
    }
}
