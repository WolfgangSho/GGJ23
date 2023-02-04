using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public GameObject rotateTarget;

    public float rotationAmount;

    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(targetPos,Vector3.left,rotationAmount*Time.deltaTime);
        transform.LookAt(targetPos,Vector3.up);
    }
}
