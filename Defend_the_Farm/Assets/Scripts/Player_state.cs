using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_state : MonoBehaviour
{
    public float damagedDelay = 2.0f;

    private int hp = 3;
    private Image Ui_damagedImage;
    private GameObject Ui_hp0;
    private GameObject Ui_hp1;
    private GameObject Ui_hp2;

    private Rigidbody rb;
    private Material material;

    private Sprite hp0;
    private Sprite hp1;

    private float time;
    private float alpa;
    private bool isDamaged;
    private bool isDelay;
    private bool isTwinkle;
    private Color c0 = new Color(0, 0, 0);
    private Color c1 = new Color(0.2f, 0.2f, 0.2f);
    private Vector3 initPos;

    private void Awake()
    {
        Ui_damagedImage = GameObject.Find("UI_Damaged").gameObject.GetComponent<Image>();
        Ui_hp0 = GameObject.Find("hp_Image0").gameObject;
        Ui_hp1 = GameObject.Find("hp_Image1").gameObject;
        Ui_hp2 = GameObject.Find("hp_Image2").gameObject;


        hp0 = Resources.Load<Sprite>("Image/hp_02") as Sprite;
        hp1 = Resources.Load<Sprite>("Image/hp_01") as Sprite;

        rb = gameObject.GetComponent<Rigidbody>();

        material = transform.Find("Model").gameObject.transform.Find("Body_09_jacket").gameObject.GetComponent<Renderer>().sharedMaterial;
        initPos = transform.transform.position;
        init();
    }

    private void Update()
    {

        //
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            recovery();
        }
        //

        if (isDamaged)
        {
            time += Time.deltaTime / 1.5f;
            alpa = Mathf.Lerp(0.6f, 0, time);
            Ui_damagedImage.color = new Color(1, 1, 1, alpa);
        }

        if (isDelay && !isTwinkle)
        {
            StartCoroutine("damaged_twinkle");
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

    public void init()
    {
        material.SetColor("_EmissionColor", c0);
        hp = 3;
        Ui_hp0.GetComponent<Image>().sprite = hp1;
        Ui_hp1.GetComponent<Image>().sprite = hp1;
        Ui_hp2.GetComponent<Image>().sprite = hp1;

        transform.position = initPos;
        //GameObject.Find("Score_Text").GetComponent<Text>().text = "00000";

    }

    public void damaged()
    {
        if (!isDelay)
        {
            hp--;
            switch (hp)
            {
                case 0:
                    Ui_hp0.GetComponent<Image>().sprite = hp0;
                    break;
                case 1:
                    Ui_hp1.GetComponent<Image>().sprite = hp0;
                    break;
                case 2:
                    Ui_hp2.GetComponent<Image>().sprite = hp0;
                    break;
            }
            if (hp != 0)
            {
                StartCoroutine("damaged_modifyUI");
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("DeadEnd").gameObject.SetActive(true);

                //Game End
            }
        }
    }
    IEnumerator damaged_twinkle()
    {
        isTwinkle = true;
        material.SetColor("_EmissionColor", c1);
        yield return new WaitForSeconds(0.2f);
        material.SetColor("_EmissionColor", c0);
        yield return new WaitForSeconds(0.2f);

        isTwinkle = false;
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

    public void recovery()
    {
        if (hp == 3)
        {
            return;
        }
        hp++;
        switch (hp)
        {
            case 1:
                Ui_hp0.GetComponent<Image>().sprite = hp1;
                break;
            case 2:
                Ui_hp1.GetComponent<Image>().sprite = hp1;
                break;
            case 3:
                Ui_hp2.GetComponent<Image>().sprite = hp1;
                break;
        }
    }
}
