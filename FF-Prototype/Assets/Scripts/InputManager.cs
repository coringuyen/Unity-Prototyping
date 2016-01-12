using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class InputManager : MonoBehaviour
{
    public GameObject ActiveModel;

    public ShowcaseUI GUIManager;

    bool time;
    float animTimer = 0;

    static bool Pause = false;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        AnimationControls();
        GeneralControls();
        Left(h);
        Right(h);
        Timer();
        if (GUIManager.activeMenu == null)
            Pause = false;
        else
            Pause = true;

        SetTheInfo();
    }

    void Timer()
    {
        if(time == true)
        {
            animTimer += Time.deltaTime * 1;
        }
    }

    public static bool Left(float h)
    {
        if (h > 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Right(float h)
    {
        if (h < -0.5)
        {
            return true;
        }
        else
        {
            return false;
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
        GUIManager.SetInfo(ActiveModel.name);
    }
}
