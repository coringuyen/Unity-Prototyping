﻿using UnityEngine;
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
        UnitMono.EventPlayerAttack.AddListener(OnHit);
	}
	
    void OnAnimatorMove()
    {
        m_anim.SetInteger("health", GetComponent<UnitMono>().health);
    }
	void OnHit(UnitMono um)
    {
        if(!m_anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            m_anim.SetTrigger("hit");
        
        
    }


}
