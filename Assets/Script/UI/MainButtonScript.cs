using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonScript : MonoBehaviour
{
    private UIButton StartBtn;
    private UIButton OptionBtn;
    private UIButton ExitBtn;
    private GameObject OptionPanel;

    private void Start()
    {
        StartBtn = this.transform.Find("Start").GetComponent<UIButton>();
        OptionBtn = this.transform.Find("Option").GetComponent<UIButton>();
        ExitBtn = this.transform.Find("Exit").GetComponent<UIButton>();
        OptionPanel = GameObject.Find("UI Root/Camera/OptionPanel");
    }

    public void GameStart()
    {
        SoundManager.Instance.EFXPlaySound("ButtonClick");
        ActiveButton(false);
        SceneLoadManager.Instance.SendMessage("NextScene");
    }

    public void GameOption()
    {
        SoundManager.Instance.EFXPlaySound("ButtonClick");
        ActiveButton(false);
        OptionPanel.SetActive(true);
        OptionPanel.GetComponent<OptionSound>().SetSoundVolume();
    }

    public void GameExit()
    {
        SoundManager.Instance.EFXPlaySound("ButtonClick");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }


    public void ActiveButton(bool isActive)
    {
        StartBtn.gameObject.SetActive(isActive);
        OptionBtn.gameObject.SetActive(isActive);
        ExitBtn.gameObject.SetActive(isActive);
    }
}
