using UnityEngine;
using System.Collections;

public class Tran_InputController : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
	void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetTrigger("BattleReady");
	}
	
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.D))
        {
            // move forward
            //rb.velocity += new Vector3(1,0,0) * Time.deltaTime * 10;
            gameObject.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // move backward
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // jump
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // dodge
            anim.SetTrigger("Land");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Hikick
            anim.SetTrigger("HiKick");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Jab/Normal attack
            anim.SetTrigger("Jab");
        }

        if (Input.GetKey(KeyCode.W))
            anim.SetBool("Jab", false);

        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.L))
        {
            // Rising_P 
            anim.SetTrigger("RisingPunch");
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("HiKick", false);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.K))
        {
            // Rising_P 
            anim.SetTrigger("ScrewKick");
        }

        if (Input.GetKey(KeyCode.L))
            anim.SetBool("HiKick", false);
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.K))
        {
            // Rising_P 
            anim.SetTrigger("SamKick");
        }
    }
}
