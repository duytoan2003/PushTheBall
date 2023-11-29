using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform posA,posB;
    public int speed;
    public Vector2 vec;    // Start is called before the first frame update
    void Start()
    {
        vec = posA.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position,posB.position)<1.1f) vec = posA.position;
        if(Vector2.Distance(transform.position,posA.position)<1.1f) vec = posB.position;
        transform.position = Vector2.MoveTowards(transform.position,vec,speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D (Collider2D collison){
        if(collison.CompareTag("Player")){
            collison.transform.SetParent(this.transform);
            
        }
    }
    private void OnTriggerExit2D (Collider2D collison){
        if(collison.CompareTag("Player")){
            collison.transform.SetParent(null);
        }
    }
}
