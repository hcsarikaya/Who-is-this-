using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SubtitleManager : MonoBehaviour
{
    public GameObject subtitlePanel;
    public TMP_Text subtitleText;  // Or use TMP_Text for TextMeshPro

    public static SubtitleManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ShowSubtitle(string text, float duration)
    {
        StopAllCoroutines(); // In case a subtitle is already showing
        StartCoroutine(ShowSubtitleCoroutine(text, duration));
    }

    IEnumerator ShowSubtitleCoroutine(string text, float duration)
    {
        subtitlePanel.SetActive(true);
        subtitleText.text = text;
        yield return new WaitForSeconds(duration);
        subtitleText.text = "";
        subtitlePanel.SetActive(false);
    }
}
