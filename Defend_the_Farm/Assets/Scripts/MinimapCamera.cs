using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public GameObject target;

    public float offsetX = 0;
    public float offsetY = 14;
    public float offsetZ = 0.4f;

    // Update is called once per frame
    void Update()
    {
        Vector3 FixedPos =
               new Vector3(
               target.transform.position.x + offsetX,
               target.transform.position.y + offsetY,
               target.transform.position.z + offsetZ);

        transform.position = FixedPos;
    }
}
