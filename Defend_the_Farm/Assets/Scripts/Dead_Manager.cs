using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dead_Manager : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0;

    }

    public void next()
    {
        GameObject.Find("DeadEnd").SetActive(false);

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

        //score
        int score = int.Parse(GameObject.Find("Score_Text").GetComponent<Text>().text);
        if (score <= 20)
        {
            score = 0;
        }
        else
        {
            score -= 20;
        }
        GameObject.Find("Score_Text").GetComponent<Text>().text = score.ToString("D5");
        //

        Time.timeScale = 1;
    }

    public void exit()
    {
        Application.Quit();
    }
}
