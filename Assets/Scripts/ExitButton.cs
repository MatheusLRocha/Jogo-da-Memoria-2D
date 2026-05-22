using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ExitButton : MonoBehaviour
{
    public void Start()
    {
        WindowManager.instance.hasMatched = false;
    }

}
