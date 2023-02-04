using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRise : MonoBehaviour
{
    public float speed;
    public float max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y < max)
        {
            transform.Translate(0,speed * Time.deltaTime,0, Space.World);
        }
    }
}
