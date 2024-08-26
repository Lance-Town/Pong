using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScreen : MonoBehaviour
{
    public GameObject helpPanel;
    private bool helpScreenShowing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H)) {
            ToggleHelp();
        }   
    }

    void ToggleHelp() {
        if (helpScreenShowing) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    void ResumeGame() {
        Time.timeScale = 1f;
        helpScreenShowing = false;
        helpPanel.SetActive(false);
    }

    void PauseGame() {
        Time.timeScale = 0;
        helpScreenShowing = true;
        helpPanel.SetActive(true);
    }
}
