using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSide : MonoBehaviour
{
    public Side thisSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown()
    {
        //determine which direction its going in

        Vector3 pos = transform.localPosition;

        thisSide = Side.Error;

        if(pos.x > 0)
        {
            thisSide = Side.East;
        }
        else if(pos.x < 0)
        {
            thisSide = Side.West;
        }

        if(pos.y > 0)
        {
            thisSide = Side.Top;
        }
        else if(pos.y < 0)
        {
            thisSide = Side.Bottom;
        }

        if(pos.z > 0)
        {
            thisSide = Side.North;
        }
        else if(pos.z < 0)
        {
            thisSide = Side.South;
        }

        GM.gm.SelectSide(thisSide, transform.parent.position);

        transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Side
{
    Top,
    North,
    West,
    East,
    South,
    Bottom,
    Error
}


