using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_move : MonoBehaviour
{
    private GameObject target;
    private Vector3 direction;
    private float velocity;
    private bool isMoveable;
    private bool isAttackable;
    private bool isLock;
    private Rigidbody rb;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;
        target = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        anim = transform.Find("Z_Model").gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        velocity = 1f * Time.smoothDeltaTime;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= 30.0f && isMoveable && !isLock)
        {
            anim.SetBool("isWalk", true);
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
        else{
            anim.SetBool("isWalk", false);

            Vector3 FixedPos =
               new Vector3(
               target.transform.position.x,
               target.transform.position.y,
               target.transform.position.z);
            transform.LookAt(FixedPos);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.Play("Z_attack_A");
            isMoveable = false;
            isAttackable = true;
            //Attack
        }

        if (other.gameObject.tag == "Bullet")
        {
            //dead
            anim.SetBool("isDead", true);
            isMoveable = false;
            Destroy(gameObject, 1);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        isMoveable = true;
        isAttackable = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }
    IEnumerator WaitForIt()
    {
        isLock = true;
        yield return new WaitForSeconds(1.0f);
        isLock = false;
    }
}
