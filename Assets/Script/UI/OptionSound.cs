using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionSound : MonoBehaviour
{
    private UISlider BGMVolume;
    private UISlider EFXVolume;

    private void Awake()
    {
        BGMVolume = this.transform.Find("BGMSoundSlider").GetComponent<UISlider>();
        EFXVolume = this.transform.Find("EFXSoundSlider").GetComponent<UISlider>();
    }

    private void Update()
    {
        SoundManager.Instance.SetBGMVolume(BGMVolume.value);
        SoundManager.Instance.SetEFXVolume(EFXVolume.value);
    }

    public void SetSoundVolume()
    {
        BGMVolume.value = SoundManager.Instance.GetBGMVolume();
        EFXVolume.value = SoundManager.Instance.GetEFXVolume();
    }

    public void DoneBtn()
    {
        SoundManager.Instance.EFXPlaySound("ButtonClick");
        this.gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name == "Chaos_Main")
            GameObject.Find("MainBackGround").GetComponent<MainButtonScript>().ActiveButton(true);
    }
}
