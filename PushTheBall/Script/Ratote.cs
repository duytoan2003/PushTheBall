using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratote : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, Time.deltaTime*speed);
    }
}
