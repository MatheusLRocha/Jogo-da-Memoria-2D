using UnityEngine.SceneManagement;
using UnityEngine;

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
