using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp = 1;
    public float attackDelay = 0.05f;

    private GameObject target;
    private Vector3 direction;
    private float velocity;
    private bool isMoveable;
    private bool isPlayAttack;
    private bool isAttackable;
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

        if (distance <= 30.0f && isMoveable)
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
        if (other.gameObject.tag == "Bullet")
        {
            hp--;
            if(hp == 0)
            {
                //dead
                anim.SetBool("isDead", true);
                isMoveable = false;
                Destroy(gameObject, 1);
            }
            else
            {
                //damaged
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isPlayAttack)
        {
            if (other.gameObject.tag == "Player")
            {
                anim.Play("Z_attack_A");
                isMoveable = false;
                isAttackable = true;
                isPlayAttack = true;
                StartCoroutine("attack");
                StartCoroutine("attackDone");
                //Attack
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isAttackable = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.velocity = Vector3.zero;
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(attackDelay);
        if (isAttackable)
        {
            Debug.Log("aaaaa");
            GameObject.FindWithTag("Player").gameObject.GetComponent<Player_state>().damaged();
        }
    }

    IEnumerator attackDone()
    {
        yield return new WaitForSeconds(1.0f);
        isPlayAttack = false;
        if (!isAttackable)
        {
            isMoveable = true;
        }
    }
}
