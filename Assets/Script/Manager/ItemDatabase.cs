using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemkind { ITEMNULL ,CONSUM, EQUIP};

public class ItemInfo
{
    public itemkind kind;
    public string name;
    public int Count;
    public float ATK;
    public float DEF;
    public float SPD;
    public float RecoveryHP;
    public float HP;
    public float RecoveryMP;
    public float MP;
    public string comment;
    public int price;
    public Sprite icon;

    public ItemInfo() { }
    public ItemInfo(ItemInfo _item)
    {
        kind = _item.kind;
        name = _item.name;
        Count = _item.Count;
        ATK = _item.ATK;
        DEF = _item.DEF;
        SPD = _item.SPD;
        RecoveryHP = _item.RecoveryHP;
        HP = _item.HP;
        RecoveryMP = _item.RecoveryMP;
        MP = _item.MP;
        comment = _item.comment;
        price = _item.price;
        icon = _item.icon;
    }
    public ItemInfo(itemkind _kind , string _name,int _Count, float _ATK, float _DEF, float _SPD, float _RecoveryHP, float _HP, float _RecoveryMP, float _MP, string _comment,int _price, Sprite _icon)
    {
        kind = _kind;
        name = _name;
        Count = _Count;
        ATK = _ATK;
        DEF = _DEF;
        SPD = _SPD;
        RecoveryHP = _RecoveryHP;
        HP = _HP;
        RecoveryMP = _RecoveryMP;
        MP = _MP;
        comment = _comment;
        price = _price;
        icon = _icon;
    }
    //아이템 종류에따라 생성자도 달라지게 만들어볼수있겠다.
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
    private Dictionary<string,Sprite> dicItemIcons;
    private List<ItemInfo> lstItemInfo;

    private void Awake()
    {
        if (sInstance == null)
            sInstance = this;

        //나중에 json 쓸때는 한번에 읽어오는게 아니라 각각 읽어와서 써야한다. 그래야지 올바른 아이콘에 들어감
        itemIcons = Resources.LoadAll<Sprite>("Textures/Items");
        dicItemIcons = new Dictionary<string, Sprite>();
        for (int i=0; i<itemIcons.Length; i++)
        {
            dicItemIcons.Add(itemIcons[i].name, itemIcons[i]);
        }

        // json 으로 읽어와서 포문으로 16개 돌린다
        lstItemInfo = new List<ItemInfo>();
        for (int i=0; i<itemIcons.Length; i++)
        {
            lstItemInfo.Add(new ItemInfo((itemkind)((int)LoadManager.Instance.ItemJson["Item"][i]["kind"]),
                (string)LoadManager.Instance.ItemJson["Item"][i]["name"],
                (int)LoadManager.Instance.ItemJson["Item"][i]["count"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["ATK"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["DEF"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["SPD"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["RecoveryHP"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["HP"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["RecoveryMP"],
                (float)LoadManager.Instance.ItemJson["Item"][i]["MP"],
                (string)LoadManager.Instance.ItemJson["Item"][i]["comment"],
                (int)LoadManager.Instance.ItemJson["Item"][i]["price"],
                dicItemIcons[(string)LoadManager.Instance.ItemJson["Item"][i]["name"]]));
        }

        GameObject itemGrid = GameObject.Find("StorePanel").GetComponentInChildren<UIGrid>().gameObject;


        foreach (ItemInfo mlist in lstItemInfo)
        {
            GameObject itemSlot = Instantiate(Resources.Load<GameObject>("ItemSlot"));
            itemSlot.transform.parent = itemGrid.transform;
            itemSlot.transform.localScale = new Vector3(1, 1, 1);
            itemSlot.GetComponent<ItemController>().sItemInfo = mlist;
            itemSlot.GetComponent<UISprite>().enabled = true;
            itemSlot.GetComponent<UISprite>().atlas = Resources.Load<UIAtlas>("Textures/StoreAtlas");
            itemSlot.GetComponent<UISprite>().spriteName = mlist.name;
            itemSlot.GetComponent<UIButton>().normalSprite = mlist.name;
        }
    }
}
