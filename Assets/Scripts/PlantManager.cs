using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{

    public int plantNum;

    public int maxLevel;

    public int plantXPAfterLevelUp, plantXPAfterLevelDown;

    public List<GameObject> holders;

    public List<GameObject> prefabs;

    public int[] plantLevel, plantXP;

    // Start is called before the first frame update
    void Start()
    {
        plantLevel = new int[plantNum];
        plantXP = new int[plantNum];

        for(int i=0; i<plantLevel.Length; i++)
        {
            plantLevel[i] = 1;
        }

        for(int i=0; i<plantXP.Length; i++)
        {
            plantXP[i] = 50;
        }

        for(int i=0; i<plantNum; i++)
        {
            UpdatePlantPrefab(i, plantLevel[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlterPlantXP(int plant, int val)
    {
        plantXP[plant] += val;

        if(plantXP[plant] > 100)
        {
            plantXP[plant] = plantXPAfterLevelUp;
            AlterPlantLevel(plant,true);
        }
        else if(plantXP[plant] < 0)
        {
            plantXP[plant] = plantXPAfterLevelDown;
            AlterPlantLevel(plant,false);
        }
    }

    void AlterPlantLevel(int plant, bool increase)
    {
        if(increase)
        {
            if(plantLevel[plant] != maxLevel)
            {
                plantLevel[plant] += 1;
            }
        }
        else
        {
            plantLevel[plant] -= 1;

            if(plantLevel[plant] == 0)
            {
                Debug.Log("uhoh");
            }
            
        }

        if(plantLevel[plant] > 0)
        {
            UpdatePlantPrefab(plant, plantLevel[plant]);
        }
    }

    void UpdatePlantPrefab(int plant, int level)
    {
        Destroy(holders[plant].transform.GetChild(0).gameObject);

        GameObject p = Instantiate(prefabs[level-1],holders[plant].transform.position,Quaternion.identity) as GameObject;

        p.transform.SetParent(holders[plant].transform);

        //for now
        p.transform.localEulerAngles = new Vector3(0,180,0);

    }
}
