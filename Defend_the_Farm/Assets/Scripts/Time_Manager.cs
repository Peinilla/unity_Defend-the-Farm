using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_Manager : MonoBehaviour
{

    private Text timeText;
    private GameObject infoWindow;
    int time;
    private int t = 90;

    private void Awake()
    {
        timeText = GetComponent<Text>();
        infoWindow = GameObject.Find("Canvas").transform.Find("Info").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        init();
        start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha6) )
        {
            time = 1;
        }
    }

    public void init()
    {
        time = t;
        timeText.text = time.ToString("D2");
        StopAllCoroutines();
    }

    public void modifyT(int n)
    {
        t = n;
        init();
    }

    public void pause()
    {
        StopAllCoroutines();
    }

    public void start()
    {
        StartCoroutine("timer");
    }
    
    IEnumerator timer()
    {
        while (time != 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            timeText.text = time.ToString("D2");
        }

        pause();
        //next Day

        infoWindow.SetActive(true);
    }


}
