using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cammeramain : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    float playerY,cameraY;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;

       playerY = target.position.y;
       cameraY = transform.position.y;
    }
    void FixedUpdate()
    {
       Vector3 targetCampos = target.position + offset;
        transform.position = Vector3.Lerp( transform.position, targetCampos,smoothing*Time.deltaTime);
        if (target.position.y < playerY + 5)
        {
            transform.position = new Vector3(transform.position.x, cameraY, transform.position.z);
        }
        //else playerY = target.position.y;
       
    }
}
