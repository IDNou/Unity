using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInfo
{
    public float HP;
    public float MAXHP;
    public float MP;
    public float MAXMP;
    public float ATK;
    public float DEF;
    public float SPD;

    public DummyInfo() {
        HP = 0;
        MAXHP = 0;
        MP = 0;
        MAXMP = 0;
        ATK = 0;
        DEF = 0;
        SPD = 0;
    }
    public DummyInfo(float _HP, float _MaxHP, float _MP, float _MaxMP, float _ATK, float _DEF, float _SPD)
    {
        HP = _HP;
        MAXHP = _MaxHP;
        MP = _MP;
        MAXMP = _MaxMP;
        ATK = _ATK;
        DEF = _DEF;
        SPD = _SPD;
    }

    public void zero()
    {
        HP = 0;
        MAXHP = 0;
        MP = 0;
        MAXMP = 0;
        ATK = 0;
        DEF = 0;
        SPD = 0;
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance = null;
    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject gObject = new GameObject("_GameManager");
                sInstance = gObject.AddComponent<GameManager>();
            }

            return sInstance;
        }
    }

    private ItemDatabase dbInstance;
    private LoadManager loadInstance;

    private GameObject pStorePanel;
    private GameObject pGoldPanel;
    private GameObject pStatusPanel;
    private int iGold;
    public int nGold
    {
        get
        {
            return iGold;
        }
        set
        {
            iGold = value;
        }
    }

    // 아이템 정보들 적용시키기?
    private List<ItemInfo> invenItem;
    public List<ItemInfo> ninvenItem
    {
        get
        {
            return invenItem;
        }
        set
        {
            invenItem = value;
        }
    }

    private Status PlayerStatus;
    public DummyInfo curStatus;
    public DummyInfo ItemStatus;

    private void Awake()
    {
        //여기다가 시작할때 필요한 매니져를 다올려놓고 시작한다
        if (sInstance == null)
            sInstance = this;

        //아이템 데이터베이스
        loadInstance = LoadManager.Instance;
        loadInstance.FileLoad();
        dbInstance = ItemDatabase.Instance;

        pStorePanel = GameObject.Find("StorePanel");
        pGoldPanel = GameObject.Find("GoldPanel");
        pStatusPanel = GameObject.Find("StatusInfo");

        pStorePanel.SetActive(false);
        pGoldPanel.GetComponentInChildren<UILabel>().text = "0";

        invenItem = new List<ItemInfo>();

        PlayerStatus = GameObject.Find("Prod").GetComponent<Status>();
        curStatus = new DummyInfo();
        ItemStatus = new DummyInfo();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(pStorePanel.activeSelf)
            {
                pStorePanel.SetActive(false);
            }
            else
            {
                pStorePanel.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            iGold += 500;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            PlayerStatus.HP -= 10.0f;
        }

        curStatus.ATK = (float)LoadManager.Instance.StatusJson["prod"]["ATK"] + (PlayerStatus.Level - 1) * 5;
        curStatus.DEF = (float)LoadManager.Instance.StatusJson["prod"]["DEF"] + (PlayerStatus.Level - 1) * 0.5f;
        curStatus.SPD = (float)LoadManager.Instance.StatusJson["prod"]["SPD"];

        ItemStatus.zero();
        foreach (ItemInfo Item in invenItem)
        {
            ItemStatus.MAXHP += Item.HP;
            ItemStatus.MAXMP += Item.MP;
            ItemStatus.ATK += Item.ATK;
            ItemStatus.DEF += Item.DEF;
            ItemStatus.SPD += Item.SPD;
        }

        pStatusPanel.transform.Find("LEV").GetComponent<UILabel>().text = "LEV:" + PlayerStatus.Level.ToString();
        if (invenItem.Count>0)
        {
            pStatusPanel.transform.Find("ATK").GetComponent<UILabel>().text = "ATK:" + curStatus.ATK.ToString() + "+" + ItemStatus.ATK.ToString();
            pStatusPanel.transform.Find("DEF").GetComponent<UILabel>().text = "DEF:" + curStatus.DEF.ToString() + "+" + ItemStatus.DEF.ToString();
            pStatusPanel.transform.Find("SPD").GetComponent<UILabel>().text = "SPD:" + curStatus.SPD.ToString() + "+" + ItemStatus.SPD.ToString();
        }
        else
        {
            pStatusPanel.transform.Find("ATK").GetComponent<UILabel>().text = "ATK:" + curStatus.ATK.ToString();
            pStatusPanel.transform.Find("DEF").GetComponent<UILabel>().text = "DEF:" + curStatus.DEF.ToString();
            pStatusPanel.transform.Find("SPD").GetComponent<UILabel>().text = "SPD:" + curStatus.SPD.ToString();
        }

      
        pGoldPanel.GetComponentInChildren<UILabel>().text = iGold.ToString();
    }

    public void PlusItemStat(ItemInfo item)
    {
        PlayerStatus.HP += item.HP;
        PlayerStatus.MP += item.MP;
        PlayerStatus.MAXHP += item.HP;
        PlayerStatus.MAXMP += item.MP;
        PlayerStatus.ATK += item.ATK;
        PlayerStatus.DEF += item.DEF;
        PlayerStatus.SPD += item.SPD;
    }

    public void MinusItemStat(ItemInfo item)
    {
        PlayerStatus.MAXHP -= item.HP;
        PlayerStatus.MAXMP -= item.MP;
        if (PlayerStatus.MAXHP < PlayerStatus.HP)
            PlayerStatus.HP = PlayerStatus.MAXHP;
        if (PlayerStatus.MAXMP < PlayerStatus.MP)
            PlayerStatus.MP = PlayerStatus.MAXMP;
        PlayerStatus.ATK -= item.ATK;
        PlayerStatus.DEF -= item.DEF;
        PlayerStatus.SPD -= item.SPD;
    }
}
