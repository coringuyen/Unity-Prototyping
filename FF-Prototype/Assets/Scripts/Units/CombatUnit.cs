﻿using UnityEngine;
using System.Collections;

namespace Unit
{
    public enum State
    {
        idle,
        ready,
        attack,
        defend,
        dead,
    }

    public enum Direction
    {
        forward,
        back,
    }

    public class CombatUnit : MonoBehaviour, IUnit
    {
        Animator anim;
        void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            if (_active)
            {
                Combat.CombatSystem.OnAbilitySelected += Attack;
            }

            source = transform.position;
        }

        public Vector3 source;
        void Attack()
        {
            print("move " + name);
            StartCoroutine(Move(target.position));
        }
        
        public Transform target;
        public float offset = .02f;
        IEnumerator Move(Vector3 destination)
        { 
            anim.SetTrigger("run");
            while (transform.position != destination)
            {
                float distance = Vector3.Magnitude(transform.position - destination);
                if (distance < offset)
                    break;
                print("distance from target = " + distance);
                transform.position = Vector3.MoveTowards(transform.position, destination, .03f);
                yield return null;

            }
            anim.SetTrigger("uppercut");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            print("anim finished");
            RunBack();
            


        }

        void RunBack()
        {
            StartCoroutine(Move(source));
        }
        void OnActive()
        {

            anim.SetTrigger("idle");
        }

        void OnIdle()
        {
            Animator anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("idle");
        }

        void OnAbilitySelected()
        {

        }
        public void SetState(State state)
        {
            Animator anim = GetComponentInChildren<Animator>();

            switch (state)
            {
                case State.idle:
                    anim.SetTrigger("idle");
                    break;
                case State.ready:
                    anim.SetTrigger("ready");
                    break;
                case State.attack:
                    anim.SetTrigger("attack");
                    break;
                case State.defend:
                    anim.SetTrigger("defend");
                    break;
                case State.dead:
                    anim.SetTrigger("dead");
                    break;
            }
        }



        #region Variables
        [SerializeField]
        int _health;
        [SerializeField]
        float _attack;
        [SerializeField]
        float _defense;
        [SerializeField]
        bool _active;


        public float attack { get { return _attack; } set { _attack = value; } }

        public float defense { get { return _defense; } set { _defense = value; } }

        public int health { get { return _health; } set { _health = value; } }
        #endregion Variables
    }
}
