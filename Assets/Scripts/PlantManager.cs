using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlantManager : MonoBehaviour
{

    public int plantNum;

    public int maxLevel;

    public int plantXPAfterLevelUp, plantXPAfterLevelDown;

    public List<GameObject> holders;

    public List<GameObject> textGOs;

    public List<GameObject> prefabs;

    public GameObject audioLevelUpGO, audioDeathGO;

    public int[] plantLevel, plantXP;

    List<TextMeshProUGUI> xpText;

    AudioSource audioLevelUp, audioDeath;

    // Start is called before the first frame update
    void Start()
    {
        audioLevelUp = audioLevelUpGO.GetComponent<AudioSource>();
        audioDeath = audioDeathGO.GetComponent<AudioSource>();

        plantLevel = new int[plantNum];
        plantXP = new int[plantNum];

        xpText = new List<TextMeshProUGUI>();

        for(int i=0; i<textGOs.Count; i++)
        {
            xpText.Add(textGOs[i].GetComponent<TextMeshProUGUI>());
        }

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
        if(plantLevel[plant] == maxLevel && plantXP[plant] > (100 * plantLevel[plant]))
        {
            plantXP[plant] = 100 * plantLevel[plant];
        }
        else
        {
            plantXP[plant] += val;
        }
        
        if(plantXP[plant] > (100 * plantLevel[plant]) && plantLevel[plant] != maxLevel)
        {
            plantXP[plant] = plantXPAfterLevelUp;
            AlterPlantLevel(plant,true);
        }
        else if(plantXP[plant] <= 0 && plantLevel[plant] >= 0)
        {
            plantXP[plant] = plantXPAfterLevelDown;
            AlterPlantLevel(plant,false);
        }

        UpdateText(plant, plantLevel[plant]);
    }

    void AlterPlantLevel(int plant, bool increase)
    {
        if(increase)
        {
            if(plantLevel[plant] > 0)
            {
                audioLevelUp.Stop();
                audioLevelUp.Play();
            }
    
            plantLevel[plant] += 1;
        }
        else
        {
            plantLevel[plant] -= 1;

            if(plantLevel[plant] == 0)
            {
                audioDeath.Stop();
                audioDeath.Play();
            }
            
        }

        UpdatePlantPrefab(plant, plantLevel[plant]);
    }

    void UpdatePlantPrefab(int plant, int level)
    {
        if(level >= 0)
        {
            Destroy(holders[plant].transform.GetChild(0).gameObject);

            
            GameObject p = Instantiate(prefabs[level],holders[plant].transform.position,Quaternion.identity) as GameObject;

            p.transform.SetParent(holders[plant].transform);

            //for now
            p.transform.localEulerAngles = new Vector3(0,180,0);

            if(level == 0)
            {
                 plantLevel[plant] = -1;
            }
        }

        UpdateText(plant, level);
    }

    void UpdateText(int plant, int level)
    {
        string s = plantXP[plant].ToString() + "/" + (100*level).ToString();

        if(level < 1)
        {
            s = "";
        }

        xpText[plant].text = s;
    }
}
