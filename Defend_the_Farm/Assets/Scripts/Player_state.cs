using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_state : MonoBehaviour
{
    public float damagedDelay = 2.0f;

    private int hp = 3;
    private Image Ui_damagedImage;
    private GameObject[] Ui_hpObj;
    private Rigidbody rb;

    private Sprite hp0;
    private Sprite hp1;

    private float time;
    private float alpa;
    private bool isDamaged;
    private bool isDelay;

    void Start()
    {
        Ui_damagedImage = GameObject.Find("UI_Damaged").gameObject.GetComponent<Image>();
        Ui_hpObj = GameObject.FindGameObjectsWithTag("Hp_image");

        hp0 = Resources.Load<Sprite>("Image/hp_02") as Sprite;
        hp1 = Resources.Load<Sprite>("Image/hp_01") as Sprite;

        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDamaged)
        {
            time += Time.deltaTime / 1.5f;
            alpa = Mathf.Lerp(1, 0, time);
            Ui_damagedImage.color = new Color(1, 1, 1, alpa);
        }   
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;

        if (collision.gameObject.tag == "Monster")
        {
            if (!isDamaged)
            {
                damaged();
            }
        }
    }

    public void damaged()
    {
        if (!isDelay)
        {
            hp--;
            switch (hp)
            {
                case 0:
                    Ui_hpObj[0].GetComponent<Image>().sprite = hp0;
                    Ui_hpObj[1].GetComponent<Image>().sprite = hp0;
                    Ui_hpObj[2].GetComponent<Image>().sprite = hp0;
                    break;
                case 1:
                    Ui_hpObj[0].GetComponent<Image>().sprite = hp1;
                    Ui_hpObj[1].GetComponent<Image>().sprite = hp0;
                    Ui_hpObj[2].GetComponent<Image>().sprite = hp0;
                    break;
                case 2:
                    Ui_hpObj[0].GetComponent<Image>().sprite = hp1;
                    Ui_hpObj[1].GetComponent<Image>().sprite = hp1;
                    Ui_hpObj[2].GetComponent<Image>().sprite = hp0;
                    break;
                case 3:
                    Ui_hpObj[0].GetComponent<Image>().sprite = hp1;
                    Ui_hpObj[1].GetComponent<Image>().sprite = hp1;
                    Ui_hpObj[2].GetComponent<Image>().sprite = hp1;
                    break;
            }
            if (hp != 0)
            {
                StartCoroutine("damaged_modifyUI");
            }
            else
            {
                //Game End
            }
        }

    }

    IEnumerator damaged_modifyUI()
    {
        isDelay = true;

        time = 0;
        isDamaged = true;
        Ui_damagedImage.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1.2f);
        isDamaged = false;
        Ui_damagedImage.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(damagedDelay - 1.2f);
        isDelay = false;
    }
}
