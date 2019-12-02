using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox_effect : MonoBehaviour
{

    //.002 ~ -.002
    // Update is called once per frame
    int direct = 1;
    float speed = 0.15f;
    void Update()
    {
        if(transform.localPosition.z > 0.001)
        {
            direct = -1;
        }
        else if(transform.localPosition.z < -0.001)
        {
            direct = 1;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime * direct);

    }
}
