using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Monster")
        {

        }
    }
}
