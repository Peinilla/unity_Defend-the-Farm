using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public int type;
    // 0 : kit , 1 : rifle , 2 : shotgun
    
    void Start()
    {
        switch (type)
        {
            case 0:
                transform.Find("typeContainer").transform.Find("w_kit").GetComponent<MeshRenderer>().enabled = true;
                break;
            case 1:
                transform.Find("typeContainer").transform.Find("w_rifle").GetComponent<MeshRenderer>().enabled = true;
                break;
            case 2:
                transform.Find("typeContainer").transform.Find("w_flamethrower").GetComponent<MeshRenderer>().enabled = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }

        switch (type)
        {
            case 0:
                GameObject.FindWithTag("Player").gameObject.GetComponent<Player_state>().recovery();
                break;
            case 1:
                GameObject.FindWithTag("Player").gameObject.GetComponent<Player_gunManager>().ChangeGun(global::type.rifle);
                break;
            case 2:
                GameObject.FindWithTag("Player").gameObject.GetComponent<Player_gunManager>().ChangeGun(global::type.shotgun);
                break;
        }
        Destroy(gameObject);
    }
}
