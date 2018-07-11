using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemkind { ITEMNULL ,CONSUM, EQUIP};

public class ItemInfo
{
    public itemkind kind;
    public string name;
    public int Count;
    public int ATK;
    public int DEF;
    public int SPD;
    public int RecoveryHP;
    public int HP;
    public int RecoveryMP;
    public int MP;
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
    public ItemInfo(itemkind _kind , string _name,int _Count, int _ATK, int _DEF, int _SPD,int _RecoveryHP,int _HP,int _RecoveryMP,int _MP, string _comment,int _price, Sprite _icon)
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
    private List<ItemInfo> lstItemInfo;

    private void Awake()
    {
        if (sInstance == null)
            sInstance = this;

        //나중에 json 쓸때는 한번에 읽어오는게 아니라 각각 읽어와서 써야한다. 그래야지 올바른 아이콘에 들어감
        itemIcons = Resources.LoadAll<Sprite>("Textures/Items");

        // json 으로 읽어와서 포문으로 16개 돌린다
        lstItemInfo = new List<ItemInfo>();
        lstItemInfo.Add(new ItemInfo(itemkind.CONSUM, this.itemIcons[0].name, 1, 0, 0, 0, 100, 0, 0, 0, "1", 10, this.itemIcons[0]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[1].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[1]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[2].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[2]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[3].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[3]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[4].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[4]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[5].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[5]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[6].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[6]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[7].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[7]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[8].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[8]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[9].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[9]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[10].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[10]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[11].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[11]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[12].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[12]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[13].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[13]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[14].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[14]));
        lstItemInfo.Add(new ItemInfo(itemkind.EQUIP, this.itemIcons[15].name, 1, 0, 0, 0, 0, 0, 0, 0, "1", 10, this.itemIcons[15]));

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
