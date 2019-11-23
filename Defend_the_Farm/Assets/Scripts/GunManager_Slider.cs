using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager_Slider : MonoBehaviour
{
    public Slider slider;

    private float gage;
    private float time;
    private void OnEnable()
    {
        gage = 0;
        time = 0;
        slider.value = 0;
    }
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / 1.5f; 
        gage = Mathf.Lerp(0, 1, time);
        if (gage < 1)
        {
            slider.value = gage;
        }
    }
}
