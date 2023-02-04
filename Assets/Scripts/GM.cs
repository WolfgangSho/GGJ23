using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    public GameObject RootBlock;

    public float offset;

    public static GM gm { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (gm!= null && gm != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            gm = this; 
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSide(Side s, Vector3 prevPos)
    {
        Vector3 posOffset = new Vector3(0,0,0);

        switch(s)
        {
            case Side.East:
                posOffset = new Vector3(offset,0,0);
                break;
            case Side.West:
                posOffset = new Vector3(-offset,0,0);
                break;
            case Side.Top:
                posOffset = new Vector3(0,offset,0);
                break;
            case Side.Bottom:
                posOffset = new Vector3(0,-offset,0);
                break;
            case Side.North:
                posOffset = new Vector3(0,0,offset);
                break;
            case Side.South:
                posOffset = new Vector3(0,0,-offset);
                break;
        }

        posOffset = posOffset + prevPos;

        Instantiate(RootBlock,posOffset,Quaternion.identity);
    }
}
