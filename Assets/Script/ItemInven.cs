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

    private void Start()
    {
        ItemBox = new ItemInfo();
        sPlayerInfo = GameObject.Find("Prod").GetComponent<Status>();
    }

    public void OnClick()
    { 
        if(ItemBox.kind == itemkind.CONSUM)
        {
            //회복템 효과를 게임매니저로 넘겨줘야한다.
            if (sPlayerInfo.HP < sPlayerInfo.MAXHP)
            {
                emptyItemBox();
            }
        }
    }

    public void emptyItemBox()
    {
        this.GetComponent<UISprite>().atlas = Resources.Load<UIAtlas>("NGUI/Examples/Atlases/Fantasy/Fantasy Atlas");
        this.GetComponent<UISprite>().spriteName = "Dark";
        this.GetComponent<UIButton>().normalSprite = "Dark";
    }
}
