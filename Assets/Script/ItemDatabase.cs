using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public string name;
    public int ATK;
    public int DEF;
    public int SPD;
    public string comment;
    public int price;
    public Sprite icon;

    public ItemInfo() { }
    public ItemInfo(string _name, int _ATK, int _DEF, int _SPD, string _comment,int _price, Sprite _icon)
    {
        name = _name;
        ATK = _ATK;
        DEF = _DEF;
        SPD = _SPD;
        comment = _comment;
        price = _price;
        icon = _icon;
    }
}

public class ItemDatabase : MonoBehaviour
{
    private static ItemDatabase sInstance = null;
    public static ItemDatabase Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newObject = new GameObject("_ItemDatabase");
                sInstance = newObject.AddComponent<ItemDatabase>();
            }
            return sInstance;
        }
    }

    private Sprite[] itemIcons;
    private List<ItemInfo> lstItemInfo;

    private void Awake()
    {
        if (sInstance == null)
            sInstance = this;

        itemIcons = Resources.LoadAll<Sprite>("Textures/Items");

        lstItemInfo = new List<ItemInfo>();
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[0]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[1]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[2]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[3]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[4]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[5]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[6]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[7]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[8]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[9]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[10]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[11]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[12]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[13]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[14]));
        lstItemInfo.Add(new ItemInfo("1", 0, 0, 0, "1", 10, this.itemIcons[15]));
    }

    public ItemInfo this[int index]
    {
        get
        {
            return lstItemInfo[index];
        }
    }

    public ItemInfo RandomItem
    {
        get
        {
            return lstItemInfo[Random.Range(0, lstItemInfo.Count)];
        }
    }

    public int ItemCount
    {
        get
        {
            return lstItemInfo.Count;
        }
    }

}
