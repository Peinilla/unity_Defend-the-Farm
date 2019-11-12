using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_move : MonoBehaviour
{
    public GameObject target;
    private Vector3 direction;
    private float velocity;
    private bool isTriger;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        velocity = 1f * Time.smoothDeltaTime;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= 30.0f && !isTriger)
        {
            this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                                   transform.position.y + (direction.y * velocity),
                                                     transform.position.z+ (direction.z * velocity));
            Vector3 FixedPos =
               new Vector3(
               target.transform.position.x,
               target.transform.position.y,
               target.transform.position.z);
            transform.LookAt(FixedPos);

        }
        else if(isTriger){
            Vector3 FixedPos =
               new Vector3(
               target.transform.position.x,
               target.transform.position.y,
               target.transform.position.z);
            transform.LookAt(FixedPos);
        }
        else
        {
            velocity = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriger = true;
            StartCoroutine(WaitForIt());
        }

        if (other.gameObject.tag == "Bullet")
        {
            //dead
            isTriger = true;
            Destroy(gameObject, 1);

        }
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(1.0f);
        isTriger = false;
    }
}
