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
        HitBoxTrigger.EventHit.AddListener(OnHit);
	}
	
	void OnHit()
    {
        m_anim.SetTrigger("hit");        
        
    }


}
