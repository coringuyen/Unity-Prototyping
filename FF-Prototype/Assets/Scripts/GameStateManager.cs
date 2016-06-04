using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerEvent : UnityEvent<UnitMono>
{

}



public class GameStateManager : MonoBehaviour
{

    public static PlayerEvent PlayerChange;
    static UnitMono currentUnit;
    [SerializeField]
    List<UnitMono> combatUnits;
    void Awake()
    {
        if(PlayerChange == null)
            PlayerChange = new PlayerEvent();

        combatUnits.AddRange(FindObjectsOfType<UnitMono>());
        UIRoot.instance.Setup();
    }
    void Start()
    {
        currentUnit = combatUnits.Find(x => x.name.Contains("Chan"));
        PlayerChange.Invoke(currentUnit);
    }
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
