using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    //public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D nhanvat)
    {
        Player nguoidk = nhanvat.GetComponent<Player>();
        if (nguoidk != null)
        {
            if (nguoidk.currentHeal < nguoidk.maxHeal)
            {
                nguoidk.changeHeal(1);
                Destroy(gameObject);
                //nguoidk.PlaySound(collectedClip);
            }
        }
    }
}
