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
            }
            return sInstance;
        }
    }

    public JObject ItemJson;
    public JObject StatusJson;

    public void FileLoad()
    {
        string Path = File.ReadAllText("JSON/item.json" );
        ItemJson = JObject.Parse(Path);
        Path = File.ReadAllText("JSON/status.json");
        StatusJson = JObject.Parse(Path);
    }
}
