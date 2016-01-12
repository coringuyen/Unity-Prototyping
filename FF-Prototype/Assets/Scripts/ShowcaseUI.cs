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

    GameObject previousMenu;
    public GameObject activeMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    MenuFlow();
	}

    public void MenuChange(string menu)
    {
        if(menu == "_controls")
        {
            activeMenu = ControlsMenu;
            previousMenu = PauseMenu;
            PauseMenu.SetActive(false);
            ControlsMenu.SetActive(true);
        }

        if(menu == "_pause")
        {
            activeMenu = PauseMenu;
            PauseMenu.SetActive(true);
            pauseButtons[0].Select();
        }
    }

    public void GoBack()
    {
        if(activeMenu != null)
        {
            if (previousMenu != null)
            {
                activeMenu.SetActive(false);
                previousMenu.SetActive(true);
                activeMenu = previousMenu;
                MenuChange(activeMenu.name);
            }
            if (previousMenu == null)
            {
                activeMenu.SetActive(false);
                activeMenu = null;
            }
        }

    }

    void MenuFlow()
    {
        if (activeMenu == PauseMenu)
            previousMenu = null;
        if (activeMenu == ControlsMenu)
            previousMenu = PauseMenu;
    }

    public void Webpage(string url)
    {
        Application.OpenURL(url);
    }

    public void SetInfo(string Model)
    {
        infoText[0].text = "Model: " + Model;
    }
}
