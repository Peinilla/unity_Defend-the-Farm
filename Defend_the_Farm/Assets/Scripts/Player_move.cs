using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    public Transform FirePos;
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveObject();
        rotateObject();
        shoot();
    }

    void moveObject()

    {

        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        
        transform.Translate(Vector3.right * speed * Time.smoothDeltaTime * keyHorizontal, Space.World);
        transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime * keyVertical, Space.World);

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

    void shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
        }
        
    }
}
