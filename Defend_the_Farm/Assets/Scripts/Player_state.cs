using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_state : MonoBehaviour
{

    public float damagedDelay = 2.0f;

    private Image img;
    private Rigidbody rb;
    private float time;
    private float alpa;
    private bool isDamaged;
    private bool isDelay;

    void Start()
    {
        img = GameObject.Find("UI_Damaged").gameObject.GetComponent<Image>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDamaged)
        {
            time += Time.deltaTime / 1.5f;
            alpa = Mathf.Lerp(1, 0, time);
            img.color = new Color(1, 1, 1, alpa);
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
            StartCoroutine("damaged_modifyUI");
        }

    }

    IEnumerator damaged_modifyUI()
    {
        isDelay = true;

        time = 0;
        isDamaged = true;
        img.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(1.2f);
        isDamaged = false;
        img.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(damagedDelay - 1.2f);
        isDelay = false;
    }
}
