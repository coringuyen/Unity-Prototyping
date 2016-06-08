﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
[Serializable]
public class PlayerEvent : UnityEvent<UnitMono>
{

}

public class DamageEvent : UnityEvent<float, UnitMono>
{

}

public class GameStateManager : MonoBehaviour
{

    public SpawnController playerSpawner;
    public SpawnController enemySpawner;
    static public PlayerEvent PlayerChange;
    static UnitMono currentUnit;
    [SerializeField]
    List<UnitMono> combatUnits;
    void Awake()
    {
        playerSpawner.Setup();
        enemySpawner.Setup();
        if(PlayerChange == null)
            PlayerChange = new PlayerEvent();

        combatUnits.Add(playerSpawner.unit);
        combatUnits.Add(enemySpawner.unit);
        
    }
    void Start()
    {
        UIRoot.instance.Setup();
        currentUnit = playerSpawner.unit;
        //janky right now but gets both ui to update
        PlayerChange.Invoke(playerSpawner.unit);
        PlayerChange.Invoke(enemySpawner.unit);
    }
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public AudioClip winAudio;
    public void OnWin()
    {
        GetComponent<AudioSource>().clip = winAudio;
        GetComponent<AudioSource>().Play();
    }

    public void Restart()
    {
        SceneManager.UnloadScene(0);
        SceneManager.LoadScene(0);
        
    }
}
