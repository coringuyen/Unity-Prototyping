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
    GameObject PartyInfo;

    [SerializeField]
    GameObject CombatPanel;

    [SerializeField]
    GameObject AbilityInfo;

    [SerializeField]
    Text playerText;

    [SerializeField]
    Text resourceText;

    [SerializeField]
    Text healthText;

    [SerializeField]
    List<Button> buttons;

    
    

    public void printsomething()
    {
        print("something");
    }

    void Awake()
    {
        instance = this;
        SetTextLabels();
    }

    public void Setup()
    {
        GameStateManager.PlayerChange.AddListener(SetPartyInfo);
 

    }

    void SetPartyInfo(UnitMono um)
    {
        playerText.text = "Name: " + um.unitName.ToString();
        healthText.text = "Health: " + um.health.ToString();
        resourceText.text = "Resource: " + um.resource.ToString();        
    }        
 
    [ContextMenu("Set Labels")]
    void SetTextLabels()
    {
        if (PartyInfo == null)
            PartyInfo = GameObject.Find("PartyInfo");
        List<Text> texts = new List<Text>();
        texts.AddRange(PartyInfo.GetComponentsInChildren<Text>());
        foreach (Text t in texts)
        {
            switch (t.name)
            {
                case "Name":
                    playerText = t;
                    break;
                case "Health":
                    healthText = t;
                    break;
                case "Resource":
                    resourceText = t;
                    break;
                default:
                    break;
            }
        }
    }
}
