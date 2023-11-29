using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOc : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D nhanvat)
    {
        Frog nguoidk = nhanvat.GetComponent<Frog>();
        if (nguoidk != null)
        {
           nguoidk.IsJumping = true;
        }
    }
}