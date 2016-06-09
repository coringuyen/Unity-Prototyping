using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class UIButtonEvent : UnityEvent
{

}
public class UIRoot : MonoBehaviour
{
 
    public static UIRoot instance;
    [SerializeField]
    GameObject PlayerInfo;

    [SerializeField]
    GameObject EnemyInfo;

    [SerializeField]
    GameObject CombatPanel;

    [SerializeField]
    GameObject AbilityInfo;
    
    [SerializeField]
    List<Button> buttons;
    void Awake()
    {
        instance = this;
    }
    public void Setup()
    {
        GameStateManager.PlayerChange.AddListener(SetPartyInfo);
        UnitMono.EventTakeDamage.AddListener(UpdateInfo);
    }
    
    void UpdateInfo(float amt, UnitMono um)
    {
        if (um.unit.unitType == UnitType.Player)
            PlayerInfo.GetComponent<UIPartyInfo>().SetPartyInfo(um.health, um);
        if (um.unit.unitType == UnitType.Enemy)
            EnemyInfo.GetComponent<UIPartyInfo>().SetPartyInfo(um.health, um);
    }

    void SetPartyInfo(UnitMono um)
    {
        if (um.unit.unitType == UnitType.Player)
            PlayerInfo.GetComponent<UIPartyInfo>().SetPartyInfo(um.health, um);
        if (um.unit.unitType == UnitType.Enemy)
            EnemyInfo.GetComponent<UIPartyInfo>().SetPartyInfo(um.health, um);
    }
    


}
