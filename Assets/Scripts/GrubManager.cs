using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrubManager : MonoBehaviour
{
    
    public GameObject[] grubGOs;

    public GameObject PlantManagerGO;
    
    public float minTime, maxTime;

    public float grubSpeed;

    public float delayBeforeFirstGrub;

    public int maxGrubs;

    public int rewardAmount;

    public float maxGrubHeight, validGrubHeight;

    bool[] activeGrubs;

    float timer;

    PlantManager pm;

    bool unpaused;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlantManagerGO.GetComponent<PlantManager>();

        timer = delayBeforeFirstGrub;
        activeGrubs = new bool[grubGOs.Length];
        
        for(int i=0; i<activeGrubs.Length; i++)
        {
            activeGrubs[i] = false;
        }
    }

    public void ChangeMaxGrubs(int grubs)
    {
        maxGrubs = grubs;
    }

    public void ChangeMinSpawn(float m)
    {
        minTime = m;
    }

    public void ChangeMaxSpawn(float m)
    {
        maxTime = m;
    }

    int movingGrubs()
    {
        int num = 0;

        for(int i=0; i<activeGrubs.Length; i++)
        {
            if(activeGrubs[i])
            {
                num++;
            }
        }

        return num;
    }

    public int killGrub(int lane)
    {
        int reward = -1;

        if(grubGOs[lane].transform.localPosition.y > validGrubHeight)
        {
            reward = rewardAmount;

            grubGOs[lane].transform.localPosition = Vector3.zero;
            activeGrubs[lane] = false;
        }

        return reward;
    }

    // Update is called once per frame
    void Update()
    {
        if(unpaused)
        {
            if(movingGrubs() != maxGrubs)
            {
                timer -= Time.deltaTime;
            }

            if(timer <= 0)
            {
                //pick a currently inactive grub

                bool grubFound = false;
                int iterations = 0;
                int chosenGrub = -1;

                while(!grubFound)
                {
                    chosenGrub = Random.Range(0,activeGrubs.Length);

                    if(!activeGrubs[chosenGrub])
                    {
                        grubFound = true;
                    }

                    iterations++;

                    if(iterations > 10000)
                    {
                        break;
                    }
                }

                activeGrubs[chosenGrub] = true;

                timer = Random.Range(minTime,maxTime);
            }

            for(int i=0; i<activeGrubs.Length;i++)
            {
                if(activeGrubs[i])
                {
                    grubGOs[i].transform.Translate(new Vector3(0,Time.deltaTime * grubSpeed,0));
                }

                if(grubGOs[i].transform.localPosition.y > maxGrubHeight)
                {
                    //call damage effect
                    pm.AlterPlantXP(i,-500);
                    //reset grub
                    grubGOs[i].transform.localPosition = Vector3.zero;
                    activeGrubs[i] = false;   
                }
            }
        }
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
