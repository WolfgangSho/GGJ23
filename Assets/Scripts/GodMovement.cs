using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMovement : MonoBehaviour
{
    public GameObject PlantsGO;
    
    public int healAmount;

    public float healTick;

    public float healDuration;
    
    public float moveAmount;

    public float moveRepeatSeconds;

    public int centreIndex;

    public int maxIndex;

    int godIndex;

    bool ableToMove;

    float moveTimer, healTimer, powerTimer;

    PlantManager pm;

    GameObject particles;

    

    // Start is called before the first frame update
    void Start()
    {
        pm = PlantsGO.GetComponent<PlantManager>();
        particles = transform.GetChild(0).gameObject;
        particles.SetActive(false);

        godIndex = centreIndex;
        UpdatePosition();
        ableToMove = true;
    }

    // Update is called once per frame
    void Update()
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

        if(Input.GetKeyDown("space") && !particles.activeSelf)
        {
            particles.SetActive(true);
            powerTimer = healDuration;
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
}
