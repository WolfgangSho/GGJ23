using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilMovement : MonoBehaviour
{
    public float moveAmount;

    public float minTimeToMove, maxTimeToMove;

    public int centreIndex;

    public int maxIndex;

    int devilIndex;

    float timer;


    // Start is called before the first frame update
    void Start()
    {

        devilIndex = centreIndex;
        UpdatePosition();
        timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = 0;
            }
        }
        else if (timer == 0)
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

            timer = Random.Range(minTimeToMove,maxTimeToMove);
        }
    }

    void UpdatePosition()
    {
        float pos = devilIndex * moveAmount;
        transform.localPosition = new Vector3(pos,0,0);
    }
}
