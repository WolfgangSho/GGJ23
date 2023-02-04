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

    float moveTimer, healTimer;

    PlantManager pm;

    

    // Start is called before the first frame update
    void Start()
    {
        pm = PlantsGO.GetComponent<PlantManager>();
        godIndex = centreIndex;
        UpdatePosition();
        ableToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        if(ableToMove)
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

        if(healTimer == 0)
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
