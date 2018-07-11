using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInven : MonoBehaviour
{
    private ItemInfo ItemBox = null;
    public ItemInfo sItemBox
    {
        get
        {
            return ItemBox;
        }
        set
        {
            ItemBox = value;
        }
    }

    private Status sPlayerInfo;
    private UILabel ItemCount;

    private void Start()
    {
        ItemBox = new ItemInfo();
        sPlayerInfo = GameObject.Find("Prod").GetComponent<Status>();
        ItemCount = this.GetComponentInChildren<UILabel>();
    }

    private void Update()
    {
        if (ItemBox != null)
        {
            if (ItemBox.kind == itemkind.CONSUM)
            {
                ItemCount.text = ItemBox.Count.ToString();
            }
            else
            {
                ItemCount.text = "";
            }
        }
    }

    public void OnClick()
    {
        if (ItemBox != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (ItemBox.kind == itemkind.CONSUM)
                {
                    //회복템 효과를 게임매니저로 넘겨줘야한다.
                    if (sPlayerInfo.HP < sPlayerInfo.MAXHP)
                    {
                        sPlayerInfo.isHealing = true;
                        sPlayerInfo.RecoveryMount = ItemBox.RecoveryHP;
                        if (--ItemBox.Count <= 0)
                        {
                            ItemBox.Count = 1;
                            emptyItemBox();
                        }
                    }
                }
            }
            else if(Input.GetMouseButtonUp(1))
            {
                //아이템 팔기
                GameManager.Instance.nGold += (int)((float)ItemBox.price * 0.71f);
                if (ItemBox.kind == itemkind.CONSUM)
                {
                    if(ItemBox.Count >1)
                    {
                        ItemBox.Count--;
                    }
                    else
                    {
                        emptyItemBox();
                    }
                }
                else
                {
                    emptyItemBox();
                }
            }
        }
    }

    public void emptyItemBox()
    {
        //소비아이템이랑 나눠야함
        GameManager.Instance.ninvenItem.Remove(ItemBox);
        ItemBox = null;
        this.GetComponent<UISprite>().atlas = Resources.Load<UIAtlas>("NGUI/Examples/Atlases/Fantasy/Fantasy Atlas");
        this.GetComponent<UISprite>().spriteName = "Dark";
        this.GetComponent<UIButton>().normalSprite = "Dark";
        ItemCount.text = "";
    }
}
