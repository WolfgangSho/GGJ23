using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRise : MonoBehaviour
{
    public float speed;
    public float max;

    bool unpaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.y < max && unpaused)
        {
            transform.Translate(0,speed * Time.deltaTime,0, Space.World);
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
