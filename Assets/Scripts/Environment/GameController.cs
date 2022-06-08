using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int Salt;
    public bool Pause = false;

    GameObject PauseMenu;
    bool pauseClicked = false;


    void Update()
    {
        if (!pauseClicked && Input.GetKey(KeyCode.Escape))
        {
            pauseClicked = true;
            PauseGame();
        }
    }

    void Start()
    {
        float k = 4f / 3f;
        float camera_width_new = (int)(Screen.currentResolution.height * k);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().pixelRect = new Rect(
                Screen.width * 0.5f - camera_width_new * 0.5f,
                0f,
                camera_width_new,
                Screen.currentResolution.height);

        PauseMenu = GameObject.Find("PauseCanvas");
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (Pause == true)
        {

            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        Pause = !Pause;
        Invoke("PauseIsCliked", 0.5f);
    }

    void PauseIsCliked()
    {
        pauseClicked = false;
    }
}
