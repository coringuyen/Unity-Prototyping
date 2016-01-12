using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InputManager : MonoBehaviour
{
    public static GameObject ActiveModel
    {
        get
        {
            return activeModel;
        }
        set
        {
            activeModel = value;
            
        }
    }

    private static GameObject activeModel;


    public ShowcaseUI GUIManager;

    bool time;
    float animTimer = 0;

    public static bool rotateLeft;
    public static bool rotateRight;

    bool MenuDirty = false;

    void Update()
    {
        AnimationControls();
        GeneralControls();
        
        Timer();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        RotateControls(h);
    }

    void Timer()
    {
        if(time == true)
        {
            animTimer += Time.deltaTime * 1;
        }
    }
 
    void RotateControls(float h)
    {
        
        if (h > 0.5)
        {
            rotateLeft = true;
        }
        else
        {
            rotateLeft = false;
        }
        if(h < -0.5)
        {
            rotateRight = true;
        }
        else
        {
            rotateRight = false;
        }
        
    }

    void GeneralControls()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            GUIManager.PauseMenu.SetActive(!MenuDirty);
            MenuDirty = !MenuDirty;
            GUIManager.MenuActivated(GUIManager.PauseMenu);
        }
    }

    void AnimationControls()
    {
        if (MenuDirty != true)
        {
            //Jab
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                ActiveModel.GetComponent<Animator>().SetBool("Jab", true);
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                ActiveModel.GetComponent<Animator>().SetBool("Hikick", true);
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                ActiveModel.GetComponent<Animator>().SetBool("Run", true);
                time = true;
            }
            if (animTimer >= 5)
            {
                ActiveModel.GetComponent<Animator>().SetBool("Run", false);
                time = false;
                animTimer = 0;
            }
        }

    }
}
