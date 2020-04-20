using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class HighScoreInput : MonoBehaviour
{
	public PlayerScript player;
	public GameManager gm;
    public Button button;
    public TMP_InputField input;

	void Start()
	{ 
		button.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		int i = 1;
		bool shouldRun = true;

		while(shouldRun)
		{
			string entry = GetHighScoreEntryName(i);

			if(!PlayerPrefs.HasKey(entry))
			{
				PlayerPrefs.SetString(entry, GetHighScoreText());
				shouldRun = false;
				break;
			}

			i += 1;
		}
		LoadMainMenu();
	}

	public string GetHighScoreText()
	{
		StringBuilder sb = new StringBuilder();

		sb.Append(input.text);
		sb.Append(" : ");
		sb.Append(player.score.score);
		return sb.ToString();
	}

	public string GetHighScoreEntryName(int i)
	{
		StringBuilder sb = new StringBuilder();

		sb.Append("hs");
		sb.Append(i);

		return sb.ToString();
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
