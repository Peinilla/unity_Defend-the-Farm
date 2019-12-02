using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public enum type { handgun = 0, rifle = 1, shotgun = 2 }


public class Player_gunManager : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos;
    public Text bulletUI_loaded;
    public Text bulletUI_total;


    private GameObject gunContainer;
    private Animator gun_anim;
    private AudioSource reload_sound;
    private type myGun;
    private float delay;
    private bool isReady;

    private int bullet_Total;
    private int bullet_Loaded;

    private int default_Handgun = 6;
    private int default_Rifle = 50;
    private int default_Shotgun = 8;


    private void Awake()
    {
        gunContainer = GameObject.Find("R_hand_container").gameObject;
        gun_anim = gunContainer.GetComponent<Animator>();
        reload_sound = GetComponent<AudioSource>();

        init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) { return; }

        //
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeGun(type.handgun);
        }else if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeGun(type.rifle);
        }else if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeGun(type.shotgun);
        }
        //

        if (Input.GetMouseButton(0) && isReady)
        {
            if (bullet_Loaded != 0)
            {
                shoot();
            }
            else
            {
                reload();
            }

        }
        if (Input.GetKey(KeyCode.R) && isReady)
        {
            reload();
        }
    }

    public void init()
    {
        isReady = true;
        bulletUI_loaded.GetComponent<Text>().text = bullet_Loaded + "";
        bulletUI_total.GetComponent<Text>().text = "";

        ChangeGun(type.handgun);
    }

    void shoot()
    {
        bullet_Loaded--;
        bulletUI_loaded.GetComponent<Text>().text = bullet_Loaded + "";
        gun_anim.Play("shoot");

        switch (myGun)
        {
            case type.handgun:
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                break;
            case type.rifle:
                Vector3 randomAngle = transform.eulerAngles;
                randomAngle.y += Random.Range(-10, 10);
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(randomAngle));
                break;
            case type.shotgun:
                Vector3 angle = transform.eulerAngles;
                float a = 10;
                angle.y -= 2*a;
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(angle));
                angle.y += a;
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(angle));
                angle.y += a;
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(angle));
                angle.y += a;
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(angle));
                angle.y += a;
                Instantiate(Bullet, FirePos.transform.position, Quaternion.Euler(angle));
                break;
        }

        if(bullet_Loaded == 0 && bullet_Total == 0)
        {
            ChangeGun(type.handgun);
        }

        if (bullet_Loaded == 0)
        {
            isReady = false;
            StartCoroutine(autoReload());
        }
        else
        {
            isReady = false;
            StartCoroutine(WaitForIt());

        }
    }

    public void ChangeGun(type gun)
    {
        switch (myGun)
        {
            case type.handgun:
                gunContainer.transform.Find("w_handgun").gameObject.GetComponent<MeshRenderer>().enabled = false;
                break;
            case type.rifle:
                gunContainer.transform.Find("w_rifle").gameObject.GetComponent<MeshRenderer>().enabled = false;
                break;
            case type.shotgun:
                gunContainer.transform.Find("w_shotgun").gameObject.GetComponent<MeshRenderer>().enabled = false;
                break;
        }
        switch (gun)
        {
            case type.handgun:
                myGun = type.handgun;
                delay = 0.5f;
                bullet_Total = -1;
                bullet_Loaded = default_Handgun;
                FirePos.transform.localPosition = new Vector3(0.21f, 0.091f, 0.5f);
                gunContainer.transform.Find("w_handgun").gameObject.GetComponent<MeshRenderer>().enabled = true;
                break;
            case type.rifle:
                myGun = type.rifle;
                delay = 0.1f;
                bullet_Total = default_Rifle * 2;
                bullet_Loaded = default_Rifle;
                FirePos.transform.localPosition = new Vector3(0.21f,0.091f,0.9f);
                gunContainer.transform.Find("w_rifle").gameObject.GetComponent<MeshRenderer>().enabled = true;
                break;
            case type.shotgun:
                myGun = type.shotgun;
                delay = 1f;
                bullet_Total = default_Shotgun * 2;
                bullet_Loaded = default_Shotgun;
                FirePos.transform.localPosition = new Vector3(0.21f, 0.091f, 0.85f);
                gunContainer.transform.Find("w_shotgun").gameObject.GetComponent<MeshRenderer>().enabled = true;
                break;
        }
        bulletUI_loaded.GetComponent<Text>().text = bullet_Loaded + "";
        if (bullet_Total != -1)
        {
            bulletUI_total.GetComponent<Text>().text = bullet_Total + "";
        }
        else
        {
            bulletUI_total.GetComponent<Text>().text = "";
        }
    }

    void reload()
    {
        if(bullet_Loaded == getMaxBullet(myGun)){ return; }

        isReady = false;
        //
        //reload_sound.Play();
        StartCoroutine(WaitForReload());

    }

    IEnumerator autoReload()
    {
        yield return new WaitForSeconds(0.5f);
        reload();
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(delay);
        isReady = true;
    }

    IEnumerator WaitForReload()
    {
        GameObject.Find("Canvas").transform.Find("Reload_Slider").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Reload_Text").gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        
        switch (myGun)
        {
            case type.handgun:
                bullet_Loaded = default_Handgun;
                break;
            case type.rifle:
                int r = default_Rifle - bullet_Loaded;
                if (bullet_Total >= r)
                {
                    bullet_Total -= r;
                    bullet_Loaded = default_Rifle;
                }
                else
                {
                    bullet_Loaded += bullet_Total;
                    bullet_Total = 0;
                }
                break;
            case type.shotgun:
                int s = default_Shotgun - bullet_Loaded;
                if (bullet_Total >= s)
                {
                    bullet_Total -= s;
                    bullet_Loaded = default_Shotgun;
                }
                else
                {
                    bullet_Loaded += bullet_Total;
                    bullet_Total = 0;
                }
                break;
        }

        bulletUI_loaded.GetComponent<Text>().text = bullet_Loaded + "";
        if (bullet_Total != -1)
        {
            bulletUI_total.GetComponent<Text>().text = bullet_Total + "";
        }
        else
        {
            bulletUI_total.GetComponent<Text>().text = "";
        }

        GameObject.Find("Canvas").transform.Find("Reload_Slider").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Reload_Text").gameObject.SetActive(false);

        isReady = true;
    }

    int getMaxBullet(type gun)
    {
        int r = 0;

        switch (gun)
        {
            case type.handgun:
                r = default_Handgun;
                break;
            case type.rifle:
                r = default_Rifle;
                break;
            case type.shotgun:
                r = default_Shotgun;
                break;
        }

        return r;
    }
}
