using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilMovement : MonoBehaviour
{
    public GameObject PlantsGO;
    
    public float moveAmount;

    public float minTimeToMove, maxTimeToMove;

    public int damageAmount;

    public float damageTick;

    public int centreIndex;

    public int maxIndex;

    int devilIndex;

    float moveTimer, damageTimer;

    PlantManager pm;

    bool unpaused;


    // Start is called before the first frame update
    void Start()
    {
        pm = PlantsGO.GetComponent<PlantManager>();

        devilIndex = centreIndex;
        UpdatePosition();
        moveTimer = 0;


        
    }

    public void ChangeDamage(int d)
    {
        damageAmount = d;
    }

    public void ChangeMinMovement(float m)
    {
        minTimeToMove = m;
    }

    public void ChangeMaxMovement(float m)
    {
        maxTimeToMove = m;
    }

    // Update is called once per frame
    void Update()
    {
        if(unpaused)
        {
            if(moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;

                if(moveTimer <= 0)
                {
                    moveTimer = 0;
                }
            }
            else if (moveTimer == 0)
            {
                int desiredIndex = Random.Range(0,maxIndex+1);

                if(desiredIndex < devilIndex)
                {
                    devilIndex--;
                }
                else if(desiredIndex > devilIndex)
                {
                    devilIndex++;
                }

    //            Debug.Log(desiredIndex);

                UpdatePosition();

                moveTimer = Random.Range(minTimeToMove,maxTimeToMove);
            }

            if(damageTimer == 0)
            {
                pm.AlterPlantXP(devilIndex,-damageAmount);
                damageTimer = damageTick;
            }
            else
            {
                damageTimer -= Time.deltaTime;

                if(damageTimer < 0)
                {
                    damageTimer = 0;
                }
            }
        } 
    }

    void UpdatePosition()
    {
        float pos = devilIndex * moveAmount;
        transform.localPosition = new Vector3(pos,0,0);
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
