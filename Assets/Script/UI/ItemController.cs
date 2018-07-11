using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private Image slotImage;        // 슬롯 이미지
    private UISprite itemIcon;         // 아이템 아이콘(이미지)
    private UIButton itemButton;    // 아이템 클릭
    private UILabel itemInfoDiscrip;
    private ItemInfo itemInfo;      // 아이템 정보
    private ItemBuy itemBuyButton; // 내가 살 아이템 정보를 전송해준다.

    public ItemInfo sItemInfo
    {
        get
        {
            return itemInfo;
        }
        set
        {
            itemInfo = value;
        }
    }

    private bool isClick;
    private float ClickWaitTime = 0.0f;    
    public float dblClickSpeed = 0.5f;	

    protected void Awake()
    {
        slotImage = this.GetComponent<Image>();
        itemIcon = this.GetComponent<UISprite>();
        itemButton = this.GetComponent<UIButton>();
        itemInfoDiscrip = GameObject.Find("ItemInfo").GetComponentInChildren<UILabel>();
        itemBuyButton = GameObject.Find("BuyButton").GetComponent<ItemBuy>();

        isClick = false;
    }

    public void Update()
    {
        if (isClick)
        {
            if (Time.time > ClickWaitTime + dblClickSpeed)
            {
                isClick = false;
            }
        }
    }

    private void OnClick()
    {
        string distription = "이름: " + itemInfo.name + "\n"
            + "가격: " + itemInfo.price.ToString() + "\n";

        if (itemInfo.ATK > 0)
            distription += "공격력: " + itemInfo.ATK.ToString() + "\n";
        if (itemInfo.DEF > 0)
            distription += "방어력: " + itemInfo.DEF.ToString() + "\n";
        if (itemInfo.SPD > 0)
            distription += "스피드: " + itemInfo.SPD.ToString() + "\n";
        if (itemInfo.HP > 0)
            distription += "HP: " + itemInfo.HP.ToString() + "\n";
        if (itemInfo.MP > 0)
            distription += "MP: " + itemInfo.MP.ToString() + "\n";
        if (itemInfo.RecoveryHP > 0)
            distription += "HP회복력: " + itemInfo.RecoveryHP.ToString() + "\n";
        if (itemInfo.RecoveryMP > 0)
            distription += "MP회복력: " + itemInfo.RecoveryMP.ToString() + "\n";

        distription += "설명: " + itemInfo.comment;

        itemInfoDiscrip.text = distription;
        itemBuyButton.sItemInfo = itemInfo;

        if (isClick)
        {
            itemBuyButton.SendMessage("OnClick");
            isClick = false;
        }
        else
        {
            ClickWaitTime = Time.time;  //클릭 했을때의 시간 저장 
            isClick = true;
        }
    }

}