using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class LoadManager : MonoBehaviour
{
    private static LoadManager sInstance = null;
    public static LoadManager Instance
    {
        get
        {
            if(sInstance ==null)
            {
                GameObject gObject = new GameObject("_LoadManager");
                sInstance = gObject.AddComponent<LoadManager>();
                DontDestroyOnLoad(gObject);
            }
            return sInstance;
        }
    }

    public JObject ItemJson;
    public JObject StatusJson;

    public void FileLoad()
    {
        string Path = File.ReadAllText(Application.persistentDataPath +"/" + DeFine.prev + DeFine.item);
        //Application.streamingAssetsPath;
        //Application.dataPath;
        ItemJson = JObject.Parse(Path);
        Path = File.ReadAllText(Application.persistentDataPath + "/" + DeFine.prev + DeFine.status);
        StatusJson = JObject.Parse(Path);
    }
}
