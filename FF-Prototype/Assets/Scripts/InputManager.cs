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

    static bool Pause = false;
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        AnimationControls();
        GeneralControls();
        AxisDir(h);
        Timer();        
        if (GUIManager.activeMenu == null)
            Pause = false;
        else
            Pause = true;

        SetTheInfo();
    }

    void Timer()
    {
        if (time == true)
        {
            animTimer += Time.deltaTime * 1;
        }
    }
    public static bool Left, Right;
    private void AxisDir(float h)
    {
        if (h > 0.5)
        {
            Left = true;
            Right = false;
        }
        else
        {
            Left = false;
        }
    
        
        if (h < -0.5)
        {
            Left = false;
            Right = true;    
        }
        else
        {
            Right = false;
        }
      
      
        
        
        
    }
 

    void GeneralControls()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            GUIManager.MenuChange("_pause");
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            GUIManager.GoBack();
        }
    }

    void AnimationControls()
    {
        if (Pause == false && ActiveModel != null)
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

    void SetTheInfo()
    {
        if(activeModel != null)
            GUIManager.SetInfo(ActiveModel.name);
    }
}
