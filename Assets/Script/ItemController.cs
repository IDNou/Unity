using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private Image slotImage;        // 슬롯 이미지
    private UISprite itemIcon;         // 아이템 아이콘(이미지)
    private ItemInfo itemInfo;      // 아이템 정보
    private UIButton itemButton;    // 아이템 클릭
    private UILabel itemInfoDiscrip;

    protected void Awake()
    {
        slotImage = this.GetComponent<Image>();
        itemIcon = this.GetComponent<UISprite>();
        itemButton = this.GetComponent<UIButton>();
        itemInfoDiscrip = GameObject.Find("ItemInfo").GetComponentInChildren<UILabel>();
    }

    private void Start()
    {
        SetItemInfo(ItemDatabase.Instance.RandomItem); // 랜덤 아이템 셋팅
    }

    public void SetItemInfo(ItemInfo newItem)
    {
        if(newItem !=null)
        {
            itemInfo = newItem;
            itemIcon.enabled = true;
            itemIcon.atlas = Resources.Load<UIAtlas>("Textures/StoreAtlas");
            itemIcon.spriteName = itemInfo.icon.name;
            itemButton.normalSprite = itemInfo.icon.name;
        }
        else
        {
            itemInfo = null;
            itemIcon.enabled = false;
        }
    }

    public void OnClick()
    {
        //print(itemInfoDiscrip.name);
        string distription = "이름: " + itemInfo.icon.name + "\n"
            + "가격: " + itemInfo.price + "\n"
            + "공격력: " + itemInfo.ATK + "\n"
            + "방어력: " + itemInfo.DEF + "\n"
            + "스피드: " + itemInfo.SPD + "\n"
            + "설명: " + itemInfo.comment;

        itemInfoDiscrip.text = distription.ToString();
    }
}