using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_move : MonoBehaviour
{
    public GameObject target;

    public float offsetX = 0;
    public float offsetY = 5;
    public float offsetZ = -2.5f;

    public float DelayTime = 5;

    // Update is called once per frame
    void Update()
    {
        Vector3 FixedPos =
            new Vector3(
            target.transform.position.x + offsetX,
            target.transform.position.y + offsetY,
            target.transform.position.z + offsetZ);
        transform.position = Vector3.Lerp(transform.position, FixedPos, Time.deltaTime * DelayTime);
    }
}
