using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Move : MonoBehaviour
{
    public int start;
    public int end;

    private float angle_Time;
    private float angle;

    private int DayTime = 90;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle_Time += Time.deltaTime / DayTime;
        angle = Mathf.Lerp(start, end, angle_Time);
        transform.localRotation = Quaternion.Euler(0, angle, 0);
       
    }

    public void init()
    {
        angle_Time = 0;
    }

    public void modifyTime(int n)
    {
        DayTime = n;
        init();
    }
}
