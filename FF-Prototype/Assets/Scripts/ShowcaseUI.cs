using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShowcaseUI : MonoBehaviour
{
    public GameObject InfoMenu;
        public List<Text> infoText;

    public GameObject PauseMenu;
        public List<Button> pauseButtons;

    public GameObject ControlsMenu;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MenuActivated(GameObject menu)
    {
        if(menu == PauseMenu)
        {
            
        }
    }
}
