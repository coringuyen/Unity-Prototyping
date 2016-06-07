using UnityEngine;

using UnityEngine.UI;

public class UIPartyInfo : MonoBehaviour {

    [SerializeField]
    Text playerText;

    [SerializeField]
    Text resourceText;

    [SerializeField]
    Text healthText;
    void Start()
    {
     
    }
    public void SetPartyInfo(float hp, UnitMono um)
    {
        playerText.text = "Name: " + um.unitName.ToString();
        healthText.text = "Health: " + hp.ToString();
        resourceText.text = "Resource: " + um.resource.ToString();
    }

}
