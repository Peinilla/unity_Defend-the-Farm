using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public int hp = 1;
    public float attackDelay = 0.2f;

    public GameObject head;

    private float speed;
    

    private GameObject target;
    private Vector3 direction;
    private float velocity;
    private bool isWaking;
    private bool isMoveable;
    private bool isPlayAttack;
    private bool isAttackable;
    private Rigidbody rb;
    private Animator anim;
    private Material body_Material;
    private Material head_Material;

    public ParticleSystem blood;

    // Start is called before the first frame update
    void Start()
    {
        isMoveable = true;
        target = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        anim = transform.Find("Z_Model").gameObject.GetComponent<Animator>();
        body_Material = transform.Find("Z_Model").gameObject.transform.Find("Body_01_tanktop").gameObject.GetComponent<Renderer>().material;
        head_Material = head.gameObject.GetComponent<Renderer>().material;
        Color c = new Color(Random.Range(0.7f, 1), Random.Range(0.7f, 1), Random.Range(0.7f, 1), 1);
        body_Material.color = c;
        head_Material.color = c;
        speed = Random.Range(1.4f, 2f);

        blood.Stop();
        blood.Clear();
        wake();

    }

    // Update is called once per frame
    void Update()
    {
        direction = (target.transform.position - transform.position).normalized;
        velocity = speed * Time.smoothDeltaTime;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance <= 50.0f && isMoveable)
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
        else if(!isWaking && hp != 0)
        {
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
                gameObject.GetComponent<BoxCollider>().enabled = false;
                transform.Find("collider").gameObject.GetComponent<BoxCollider>().enabled = false;
                blood.Play();
                rb.velocity = Vector3.zero;
                StopAllCoroutines();

                //score
                int score = int.Parse(GameObject.Find("Score_Text").GetComponent<Text>().text);
                score++;
                GameObject.Find("Score_Text").GetComponent<Text>().text = score.ToString("D5");

                //destroy
                Destroy(gameObject, 2);
            }
            else
            {
                //damaged
            }

        }
    }

    private void wake()
    {
        anim.Play("Z_wake");
        StartCoroutine("waking");
    
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

    IEnumerator waking()
    {
        isPlayAttack = true;
        isMoveable = false;
        isWaking = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        transform.Find("collider").gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2f);
        isPlayAttack = false;
        isMoveable = true;
        isWaking = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        transform.Find("collider").gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
