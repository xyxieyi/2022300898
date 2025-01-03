using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    private Vector3 pos;
    void Start()
    {
        pos = gameObject.transform.localPosition;
    }
    void Update()
    {
        transform.Translate(Vector3.forward*1*Time.deltaTime);
        if(transform.localPosition.y<12)
        {
            gameObject.transform.localPosition = pos;
        }
    }
}
