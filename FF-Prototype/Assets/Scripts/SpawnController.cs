﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{

    
    GameObject prefab;
    [SerializeField]
    GameObject UIPrefab;

    [SerializeField]
    Transform target;

    public enum Character
    {
        Liam,
        Chan,
        Nugget,
        Skeleton,
    }

    private UnitMono m_unit;
    public UnitMono unit
    {
        get { return m_unit; }
    }
 
    public Character playerCharacter;
 
    public void Setup()
    {
        switch (playerCharacter)
        {
            case Character.Liam:
                prefab = Resources.Load("FightingNinja") as GameObject;
                break;
            case Character.Chan:
                prefab = Resources.Load("FightingChan") as GameObject;
                break;
            case Character.Nugget:
                prefab = Resources.Load("FightingNugget") as GameObject;
                break;
            case Character.Skeleton:
                prefab = Resources.Load("FightingSkeleton") as GameObject;
                break;
        }

        GameObject pawn = Instantiate(prefab, transform.localPosition, Quaternion.identity) as GameObject;
        UnitMovement um = pawn.GetComponent<UnitMovement>();
        pawn.transform.SetParent(transform);
        m_unit = pawn.GetComponent<UnitMono>();

        um.SetOrigin(transform);
        um.SetTarget(target);
        pawn.transform.LookAt(target);
        if (UIPrefab != null)
        {
            GameObject ap = Instantiate(UIPrefab) as GameObject;
            var buttons = ap.GetComponentsInChildren<Button>();

            foreach (var b in buttons)
            {
                if (b.name.Contains("Attack"))
                {
                    b.onClick.AddListener(
                      delegate { um.Action(GameObject.Find("EnemySpawn").transform); }
                      );
                }
                else if (b.name.Contains("Endturn"))
                    b.onClick.AddListener(
                      delegate { print("u pressed endturn"); }
                      );
            }



            if (pawn.GetComponent<UnitMono>() == null)
                throw new UnityException("SpawnController is trying to spawn an object that does not have a unit mono");
         
           
        }
    }




}
