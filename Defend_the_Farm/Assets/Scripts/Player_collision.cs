using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_collision : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
}
