using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Info_Manager : MonoBehaviour
{
    private int day;

    private void OnEnable()
    {
        Time.timeScale = 0;
        string str = GameObject.Find("Day_Text").gameObject.GetComponent<Text>().text;
        day = int.Parse(Regex.Replace(str, @"\D", ""));
        Debug.Log(day);
        GetComponent<Text>().text = "Day " + day.ToString() + "\nSurvive...";
    }

    public void nextDay()
    {
        day++;
        GameObject.Find("Info").SetActive(false);

        // init
        GameObject.FindWithTag("Player").gameObject.GetComponent<Player_state>().init();
        GameObject.FindWithTag("Player").gameObject.GetComponent<Player_gunManager>().init();

        GameObject[] monster = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject inx in monster)
        {
            Destroy(inx);
        }
        GameObject[] bullet = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject inx in bullet)
        {
            Destroy(inx);
        }

        GameObject.Find("MoonLight").gameObject.GetComponent<Light_Move>().init();
        GameObject.Find("SunLight").gameObject.GetComponent<Light_Move>().init();

        GameObject.Find("Time_Text").gameObject.GetComponent<Time_Manager>().init();
        GameObject.Find("Time_Text").gameObject.GetComponent<Time_Manager>().start();

        GameObject.Find("Day_Text").gameObject.GetComponent<Text>().text = "DAY " + day.ToString();

        //score
        int score = int.Parse(GameObject.Find("Score_Text").GetComponent<Text>().text);
        score += 100;
        GameObject.Find("Score_Text").GetComponent<Text>().text = score.ToString("D5");
        //
        Time.timeScale = 1;
    }
}
