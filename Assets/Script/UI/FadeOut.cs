using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    private UIButton DoneBtn;

    public bool isResultDone = false;
    private bool isDoneAlpha = false;

    private void Start()
    {
        DoneBtn = this.GetComponentInChildren<UIButton>();
        DoneBtn.enabled = false;
    }

    private void Update()
    {
       if(isResultDone)
        {
            this.GetComponent<UIPanel>().alpha += Time.deltaTime;
            if (this.GetComponent<UIPanel>().alpha >= 1)
                isDoneAlpha = true;
        }

        if(isDoneAlpha)
        {
            DoneBtn.enabled = true;
        }
    }

    public void DoneButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
