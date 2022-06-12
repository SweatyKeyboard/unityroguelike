using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public InputField LinkedTextInput;
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void SaveAndOpenMenu()
    {
		int score = GameController.Score;
		string name = LinkedTextInput.text;

		if (!PlayerPrefs.HasKey("RecordsCount"))
        {
			PlayerPrefs.SetInt("RecordsCount",1);
			PlayerPrefs.SetInt("Score0", score);
			PlayerPrefs.SetString("Name0", name);
        }
		else
        {
			int count = PlayerPrefs.GetInt("RecordsCount");
			if (count < 10)
            {
				PlayerPrefs.SetInt("Score"+count, score);
				PlayerPrefs.SetString("Name"+count, name);
				PlayerPrefs.SetInt("RecordsCount", count+1);
			}
			else
            {
				int min = int.MaxValue;
				int index = 0;
				for (int c = 0; c < count; c++)
                {
					if (PlayerPrefs.GetInt("Score" + c) < min)
					{
						min = PlayerPrefs.GetInt("Score" + c);
						index = c;
					}
				}
				PlayerPrefs.SetInt("Score" + index, score);
				PlayerPrefs.SetString("Name" + index, name);
			}
        }

		SceneManager.LoadScene("MainMenu");
	}

	public void Clear()
    {
		PlayerPrefs.DeleteAll();
		FindObjectOfType<RecordGetter>().UpdateList();
    }

	public void Exit()
	{
		Application.Quit();
	}
}
