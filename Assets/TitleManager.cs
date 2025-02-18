using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;
    public void OnTouchStartButton()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            loadingPanel.SetActive(true);
            loadingSlider.value = operation.progress;
            loadingText.text = $"{operation.progress * 100} %";
            yield return null;
            operation.allowSceneActivation = true;
        }
    }
    public void OnTouchQuitButton()
    {
        Application.Quit();
    }
}
