using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class UpdateHighscores : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int maxLines;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        UpdateScores();
    }

    public void UpdateScores()
    {
        List<string> unsorted = GetAllHighScoreEntries();
        List<string> sorted = unsorted;
        sorted.Reverse();
        int numToSpawn = sorted.Count > maxLines ? maxLines : sorted.Count;
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i < numToSpawn; i++)
        {
            sb.Append(unsorted[i]);
            sb.Append("\n");
        }
        text.text = sb.ToString();
    }

    public string GetHighScoreEntryName(int i)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("hs");
        sb.Append(i);

        return sb.ToString();

    }

    private List<string> GetAllHighScoreEntries()
    {
        List<string> entries = new List<string>();
        int i = 1;
        bool shouldRun = true;
        while (shouldRun)
        {
            string entry = GetHighScoreEntryName(i);

            if (PlayerPrefs.HasKey(entry))
            {
                entries.Add(PlayerPrefs.GetString(entry));
            }
            else
            {
                shouldRun = false;
                break;
            }

            i += 1;
        }

        return entries;
    }

    private List<string> GetSortedScores(List<string> unsorted)
    {
        List<string> sorted = new List<string>();

        for(int i = 0; i < unsorted.Count; i++)
        {
            string s = unsorted[i];

            string[] split = s.Split(':');

            Debug.Log("Bleh");
        }

        return sorted;
    }
}
