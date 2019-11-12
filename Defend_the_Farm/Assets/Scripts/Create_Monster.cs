using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class Create_Monster : MonoBehaviour
{

    public GameObject Monster;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateMonster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreateMonster()
    {
        while(true)
        {
            float max_time_range = 2.0f;
            float n = Random.Range(0, max_time_range);
            // 좌표: -20 ~ +20
            

            float max_x_range = 20.0f;
            float max_z_range = 20.0f;

            Vector3 position = new Vector3();
            position.x = Random.Range(-20, max_x_range);
            position.z = Random.Range(-20, max_z_range);
            position.y = 1.0f;
           

            GameObject monster = Instantiate(Monster, position, Quaternion.identity);
            //monster.GetComponent<Monster_move>().target = Player;
            yield return new WaitForSeconds(n);
        }
    }
}
