using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D nhanvat)
    {
        Player nguoidk1 = nhanvat.GetComponent<Player>();
        if (nguoidk1 != null)
        {
            nguoidk1.changeHeal(-1);
        }
    }
}
