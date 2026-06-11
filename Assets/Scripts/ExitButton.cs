using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ExitButton : MonoBehaviour
{
    public void voltaMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Start()
    {
        if (WindowManager.instance != null)
        {
            WindowManager.instance.hasMatched = false;
        }
    }

    

}
