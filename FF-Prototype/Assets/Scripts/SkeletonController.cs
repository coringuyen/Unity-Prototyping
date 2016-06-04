using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SkeletonController : MonoBehaviour
{
    Animator m_anim;
    
    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start ()
    {
        UnitMovement.PlayerAttack.AddListener(OnHit);
	}
	
	void OnHit()
    {
        if(!m_anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            m_anim.SetTrigger("hit");        
        
    }


}
