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
            rb.velocity += new Vector3(1,0,0) * Time.deltaTime * 10;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // move backward
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // move jump
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // move dodge
            anim.SetTrigger("Land");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // move Hikick
            anim.SetTrigger("HiKick");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            // move Jab/Normal attack
            anim.SetTrigger("Jab");
        }
    }
}
