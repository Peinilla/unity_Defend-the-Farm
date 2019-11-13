using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{

    public int speed = 10;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Model").gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveObject();
        rotateObject();
    }

    void moveObject()

    {
        bool isKeyDown = false;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime, Space.World);
            anim.SetBool("isWalk", true);
            isKeyDown = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * -1, Space.World);
            anim.SetBool("isWalk", true);
            isKeyDown = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime * -1, Space.World);
            anim.SetBool("isWalk", true);
            isKeyDown = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.smoothDeltaTime, Space.World);
            anim.SetBool("isWalk", true);
            isKeyDown = true;
        }

        if (!isKeyDown)
        {
            anim.SetBool("isWalk", false);
        }
    }

    void rotateObject()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {

            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }
    }

    

}
