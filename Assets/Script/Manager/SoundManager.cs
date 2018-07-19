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
           if(Instance == null)
            {
                GameObject gameObject = new GameObject("_SoundManager");
                sInstance = gameObject.AddComponent<SoundManager>();
            }
            return sInstance;
        }
    }

}
