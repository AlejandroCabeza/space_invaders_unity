using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{
    bool paused = false;

    private GameObject optionsScreen;

    void Start()
    {
        optionsScreen = gameObject.transform.Find("OptionsScreen").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            paused = togglePause();
            optionsScreen.SetActive(paused);
        }
    }

    void OnGUI()
    {
        /**
        * If the game is paused a button appears on the top left corner of the screen to unpause it (you can also use Q to unpause).
        */
        /*
        if (paused)
        {
            if (GUILayout.Button("Unpause"))
            {
                paused = togglePause();
                optionsScreen.SetActive(paused);
            }    
        }
        */
    }

    bool togglePause()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            return false;
        }
        else
        {
            Time.timeScale = 0.0f;
            return true;
        }
    }
}
