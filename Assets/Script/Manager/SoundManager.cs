using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager sInstance = null;
    public static SoundManager Instance
    {
        get
        {
           if(sInstance == null)
            {
                GameObject gameObject = new GameObject("_SoundManager");
                sInstance = gameObject.AddComponent<SoundManager>();
                //DontDestroyOnLoad(gameObject);
            }
            return sInstance;
        }
    }

    //public  List<AudioClip> bgmClips;
    public Dictionary<string, AudioClip> bgmClips;
    private AudioSource bgmSource;
    private AudioSource[] efxSource;

    private void Start()
    {
        if (sInstance == null)
                sInstance = this;

        DontDestroyOnLoad(gameObject);

        bgmClips = new Dictionary<string, AudioClip>();
        foreach (AudioClip audio in Resources.LoadAll<AudioClip>("Sound"))
        {
            bgmClips.Add(audio.name, audio);
        }

        bgmSource = this.gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        SetBGMVolume(0.1f);

        efxSource = new AudioSource[7];
        for (int i=0; i<7; i++)
        {
            efxSource[i] = this.gameObject.AddComponent<AudioSource>();
        }

        BGMPlay("MainScreen");
        //efxSource = this.gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {

    }


    public void EFXPlaySound(string efxName)
    {
        for(int i=0; i<7; i++)
        {
            if(!efxSource[i].isPlaying)
            {
                efxSource[i].clip = bgmClips[efxName];
                efxSource[i].Play();
                break;
            }
        }

        //efxSource.clip = bgmClips[efxName];
        //efxSource.Play();
    }

    public bool EFXPlayingSound(string EFXName)
    {
        bool isPlaying = false;

        for (int i = 0; i < 7; i++)
        {
            if(efxSource[i].isPlaying)
            {
                if (efxSource[i].clip.name == EFXName)
                {
                    isPlaying = true;
                    break;
                }
            }
        }

        return isPlaying;
    }

    public void BGMPlay(string bgmName)
    {
        bgmSource.clip = bgmClips[bgmName];
        bgmSource.Play();
    }

    public bool BGMPlayingSound(string BGMName)
    {
        bool isPlaying = false;
        
        if(bgmSource.isPlaying)
        {
            if(bgmSource.clip.name == BGMName)
            {
                isPlaying = true;
            }
        }

        return isPlaying;
    }

    public void BGMStopSound()
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        //efxSource.volume = volume;
    }

    public void SetEFXVolume(float volume)
    {
        for (int i = 0; i < 7; i++)
        {
            efxSource[i].volume = volume;
        }
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public float GetEFXVolume()
    {
        return efxSource[0].volume;
    }
    
}
