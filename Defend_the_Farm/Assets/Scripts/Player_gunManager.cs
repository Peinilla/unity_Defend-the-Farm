using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type { handgun = 0, rifle = 1, shotgun = 2 }


public class Player_gunManager : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePos;

    private GameObject gunContainer;
    private Animator gun_anim;
    private type myGun;
    private float delay;
    private bool isReady;
    
    private void Awake()
    {
        gunContainer = GameObject.Find("R_hand_container").gameObject;
        gun_anim = gunContainer.GetComponent<Animator>();
        myGun = type.shotgun;
        delay = 0.8f;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        if (Input.GetMouseButtonDown(0) && isReady)
        {
            gun_anim.Play("shoot");
            switch (myGun)
            {
                case type.handgun:
                    Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                    break;
                case type.rifle:
                    Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
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
            isReady = false;
            StartCoroutine(WaitForIt());
        }

    }

    void ChangeGun(type t)
    {
        switch (t)
        {
            case type.handgun:
                myGun = type.handgun;
                delay = 0.8f;
                break;
            case type.rifle:
                myGun = type.rifle;
                delay = 0.1f;
                break;
            case type.shotgun:
                myGun = type.shotgun;
                delay = 1f;
                break;
        }
    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(delay);
        isReady = true;
    }
}
