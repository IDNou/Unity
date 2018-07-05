using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuy : MonoBehaviour
{
    private ItemInfo Item;
    private UISprite[] invenItemSprite;

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
    }

    public void OnClick()
    {
        //1.금액이 빠져나가야한다
        //2.아이템이 템창으로 들어와야한다.
        if (Item != null)
        {
            if (GameManager.Instance.nGold >= Item.price)
            {
                foreach (UISprite inven in invenItemSprite)
                {
                    if(inven.spriteName == "Dark")
                    {
                        GameManager.Instance.nGold -= Item.price;
                        inven.atlas = Resources.Load<UIAtlas>("Textures/StoreAtlas");
                        inven.spriteName = Item.name;
                        inven.GetComponent<UIButton>().normalSprite = Item.name;
                        inven.GetComponent<ItemInven>().sItemBox = Item;
                        GameManager.Instance.ninvenItem.Add(Item);
                        break;
                    }
                }
            }
        }
    }

}
