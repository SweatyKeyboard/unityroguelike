using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int Salt;
    public bool Pause = false;
    public int Score;
    public int Timer;
    public int EnemiesKilled;
    public int BonusesCollected;
    public int RoomsCleared;
    public int FloorsCompleted;
    public int currentLevel = 1;

    public GameObject PauseMenu;
    public GameObject EndMenu;

    bool pauseClicked = false;
    float nextActTime = 0f;


    void Update()
    {
        ButtonClick b = FindObjectOfType<ButtonClick>();
        if (!pauseClicked &&
            ((Application.isMobilePlatform && b.IsCliked && b.Key == KeyCode.Escape) ||
            (!Application.isMobilePlatform) && Input.GetKey(KeyCode.Escape)))
        {
            pauseClicked = true;
            PauseGame();
        }

        if (Time.timeSinceLevelLoad > nextActTime)
        {
            nextActTime += 1f;
            Score -= 1;
            Timer++;
        }
    }

    void Start()
    {
        if (!Application.isMobilePlatform)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Android"))
                Destroy(g);
        }
        NewGame();
    }

    public void NewGame()
    {
        nextActTime = 0f;
        Score = 1000;
        float k = 4f / 3f;
        float camera_width_new = (int)(Screen.currentResolution.height * k);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().pixelRect = new Rect(
                Screen.width * 0.5f - camera_width_new * 0.5f,
                0f,
                camera_width_new,
                Screen.currentResolution.height);

        PauseMenu.SetActive(false);
        EndMenu.SetActive(false);
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
        FindObjectOfType<HPPos>().ForceStart();
    }

    void PauseIsCliked()
    {
        pauseClicked = false;
    }

    public void EndGame()
    {
        EndMenu.SetActive(true);
        Pause = true;
        Time.timeScale = 0f;
        Text txt = GameObject.Find("StatsText").GetComponent<Text>();
        txt.text = $"Пройдено этажей: {FloorsCompleted - 1}\n"+
                   $"Зачищено комнат: {RoomsCleared}\n"+
                   $"Убито врагов: {EnemiesKilled}\n"+
                   $"Собрано бонусов: {BonusesCollected}\n"+                 
                   $"Время: {TimeSpan.FromSeconds(Timer).ToString(@"mm\:ss")}\n"+
                   $"Итоговый счет: {Score}\n";
    }
}
