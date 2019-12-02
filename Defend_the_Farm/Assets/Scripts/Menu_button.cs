using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_button : MonoBehaviour
{
    public void start()
    {
        StartCoroutine("LoadScene");
    }

    public void exit()
    {
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Stage");
        op.allowSceneActivation = false;

        while (true)
        {

            yield return new WaitForSeconds(0.1f);

            Debug.Log(op.progress);
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                break;
            }
        }
    }


}
