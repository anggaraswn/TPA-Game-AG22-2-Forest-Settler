using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    //public RectTransform particle;
    //public Image circle;
    //[SerializeField] [Range(0,1)] float progress = 0f;

    public static Loading instance;
    private string targetScene;
    public GameObject loadingPanel;
    public float minLoadTime;
    public GameObject loadingGIF;
    public float loadingSpeed;
    public bool isLoading;
    public Image fadeImage;
    public float FadeTime;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(loadingPanel);
        }
        loadingPanel.SetActive(false);
        fadeImage.gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;
        fadeImage.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0);

        while (!Fade(1))
        {
            yield return null;
        }

        loadingPanel.SetActive(true);
        StartCoroutine(SpinLoadingGIF());

        while (!Fade(0))
        {
            yield return null;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        float elapsedLoadTime = 0f;

        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while (elapsedLoadTime < minLoadTime)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        while (!Fade(1))
        {
            yield return null;
        }

        while (!Fade(0))
        {
            yield return null;
        }

        loadingPanel.SetActive(false);

        isLoading = false;

    }

    private IEnumerator SpinLoadingGIF()
    {
        while (isLoading)
        {
            loadingGIF.transform.Rotate(0, 0, -loadingSpeed);
            yield return null;
        }
    }

    private bool Fade(float target)
    {
        fadeImage.CrossFadeAlpha(target, FadeTime, true);

        if (Mathf.Abs(fadeImage.canvasRenderer.GetAlpha() - target) <= 0.05f)
        {
            fadeImage.canvasRenderer.SetAlpha(target);

            return true;
        }

        return false;
    }


}
