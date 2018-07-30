using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonScript : MonoBehaviour
{
    public void GameStart()
    {
        SceneLoadManager.Instance.SendMessage("NextScene");
    }

    public void GameOption()
    {

    }

    public void GameExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
