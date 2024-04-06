using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TMP_Text loadText;

    public void LoadingScreenProgress(int SceneIndex)
    {
        StartCoroutine(LoadAsync(SceneIndex));
    }
    public void LoadingScreenContinueProgress(int SceneIndex)
    {
        StartCoroutine(LoadContinue(SceneIndex));
    }

    IEnumerator LoadAsync(int Index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingSlider.value = progress;
            loadText.text = progress * 100f + "%";

            GameManager.Instance.ChangeStateInteractUI(false);
            ///GameManager.GameLoad = 0;
           // GameManager.instance.newGame = 1;
            //PlayerPrefs.SetInt("newGame", GameManager.instance.newGame);
            //PlayerPrefs.SetInt("Load", GameManager.GameLoad);

            yield return null;
        }
    }

    IEnumerator LoadContinue(int Index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Index);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingSlider.value = progress;

            //GameManager.GameLoad = 1;
            //PlayerPrefs.SetInt("Load", GameManager.GameLoad);
            yield return null;
        }
    }
}
