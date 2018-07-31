using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscButtonScript : MonoBehaviour
{
    private GameObject OptionPanel;

    private void Awake()
    {
        OptionPanel = GameObject.Find("UI Root/Camera/OptionPanel");
    }

    public void OptionButton()
    {
        this.gameObject.SetActive(false);
        OptionPanel.SetActive(true);
        OptionPanel.GetComponent<OptionSound>().SetSoundVolume();
    }

    public void ExitButton()
    {
        this.gameObject.SetActive(false);
        GameObject FadePanel = GameObject.Find("UI Root/Camera/FadeOutPanel");
        FadePanel.GetComponentInChildren<UILabel>().text = "You Lose";
        FadePanel.GetComponent<FadeOut>().isResultDone = true;
    }

}
