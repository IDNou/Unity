using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    private ItemInfo Item;
    private UISprite[] invenItemSprite;
    private List<UISprite> lstItemSprite;

    public ItemInfo sItemInfo
    {
        get
        {
            return Item;
        }
        set
        {
            Item = value;
        }
    }

    private void Start()
    {
        var grid = GameObject.Find("InvenGrid");
        invenItemSprite = grid.GetComponentsInChildren<UISprite>();
        lstItemSprite = new List<UISprite>();
        grid.GetComponentsInChildren<UISprite>(lstItemSprite);
    }

    private void OnClick()
    {
        //1.금액이 빠져나가야한다
        //2.아이템이 템창으로 들어와야한다.
        if (Item != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (GameManager.Instance.nGold >= Item.price)
                {
                    //소비아이템일 경우
                    if (Item.kind == itemkind.CONSUM)
                    {
                        if (Item.name == "치즈" || Item.name == "범위 체력 문서")
                        {
                            AddItemToInven();
                        }
                        else
                        {
                            bool isSame = false;
                            foreach (UISprite inven in invenItemSprite)
                            {
                                //아이템 이름이 같은게 있는가?
                                if (inven.spriteName == Item.icon.name && inven.GetComponent<ItemInven>().sItemBox.Count < 3)
                                {
                                    isSame = true;
                                    GameManager.Instance.nGold -= Item.price;
                                    inven.GetComponent<ItemInven>().sItemBox.Count++;
                                    break;
                                }
                            }

                            if (!isSame)
                            {
                                AddItemToInven();
                            }
                        }
                    }
                    //소비 아이템이 아닐경우
                    else
                    {
                        AddItemToInven();
                    }
                }
            }
        }
    }

    private void AddItemToInven()
    {
        foreach (UISprite inven in invenItemSprite)
        {
            if (inven.spriteName == "Dark")
            {
                GameManager.Instance.nGold -= Item.price;
                inven.atlas = Resources.Load<UIAtlas>("Textures/StoreAtlas");
                ItemInfo lItem = new ItemInfo(Item);
                inven.spriteName = lItem.name;
                inven.GetComponent<UIButton>().normalSprite = lItem.name;
                inven.GetComponent<ItemInven>().sItemBox = lItem;
                GameManager.Instance.ninvenItem.Add(lItem);
                GameManager.Instance.PlusItemStat(lItem);
                break;
            }
        }
    }
}
