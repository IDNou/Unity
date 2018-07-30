using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager sInstance = null;
    public static SceneLoadManager Instance
    {
        get
        {
            if(sInstance == null)
            {
                GameObject gObject = new GameObject("_SceneLoadManager");
                sInstance = gObject.AddComponent<SceneLoadManager>();
            }
            return sInstance;
        }
    }

    private string nextScene;

    [SerializeField]
    private UIProgressBar progressBar;
    private UILabel LoadingLabel;

    private void NextScene()
    {
        GameObject MainPanel = GameObject.Find("MainBackGround");

        foreach (Transform gObject in MainPanel.GetComponentsInChildren<Transform>())
        {
            if(gObject.GetComponent<UIButton>())
            {
                gObject.gameObject.SetActive(false);
            }
        }

        progressBar = Instantiate(Resources.Load<UIProgressBar>("ect_/LoadingBar"), MainPanel.transform);
        LoadingLabel = Instantiate(Resources.Load<UILabel>("ect_/LoadingLabel"), MainPanel.transform);
        progressBar.name = "LoadingBar";
        LoadingLabel.name = "LoadingLabel";
        progressBar.value = 0.0f;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync("_Scene/Chaos");
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (op.progress >= 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);

                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
        }
    }

}