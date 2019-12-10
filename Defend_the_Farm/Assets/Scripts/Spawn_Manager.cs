using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Spawn_Manager : MonoBehaviour
{

    public GameObject Monster;
    public GameObject itemBox;
    
    private float max_time_range = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CreateMonster");
        StartCoroutine("CreateKit");
        StartCoroutine("CreateGun");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void modifyDiff()
    {
        if (max_time_range > 0.7f) {
            max_time_range -= 0.1f;
        }
    }

    IEnumerator CreateMonster()
    {
        while(true)
        {
            float n = Random.Range(0.3f, max_time_range);
            // 좌표: -20 ~ +20
            

            float max_x_range = 20.0f;
            float max_z_range = 20.0f;

            Vector3 position = new Vector3();
            position.x = Random.Range(-max_x_range, max_x_range);
            position.z = Random.Range(-max_z_range, max_z_range);
            position.y = 1.0f;
           

            GameObject monster = Instantiate(Monster, position, Quaternion.identity);
            yield return new WaitForSeconds(n);
        }
    }

    IEnumerator CreateKit()
    {
        while (true)
        {
            float n = Random.Range(30, 40);
            yield return new WaitForSeconds(n);

            float max_x_range = 18.0f;
            float max_z_range = 18.0f;

            Vector3 position = new Vector3();
            position.x = Random.Range(-max_x_range, max_x_range);
            position.z = Random.Range(-max_z_range, max_z_range);
            position.y = 0.75f;


            GameObject item = Instantiate(itemBox, position, Quaternion.Euler(-90,0,Random.Range(0,360)));
            item.GetComponent<ItemBox>().type = 0;
        }
    }

    IEnumerator CreateGun()
    {
        while (true)
        {
            float n = Random.Range(50, 70);
            yield return new WaitForSeconds(n);

            float max_x_range = 18.0f;
            float max_z_range = 18.0f;

            Vector3 position = new Vector3();
            position.x = Random.Range(-max_x_range, max_x_range);
            position.z = Random.Range(-max_z_range, max_z_range);
            position.y = 0.75f;


            GameObject item = Instantiate(itemBox, position, Quaternion.Euler(-90, 0, Random.Range(0, 360)));
            item.GetComponent<ItemBox>().type = Random.Range(1,3);
        }
    }
}
