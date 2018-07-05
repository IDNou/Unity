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

    public void OnClick()
    { 
        //널 처리 여기서부터
        if(ItemBox.kind == itemkind.CONSUM)
        {
            emptyItemBox();
        }
    }

    public void emptyItemBox()
    {
        this.GetComponent<UISprite>().atlas = Resources.Load<UIAtlas>("NGUI/Examples/Atlases/Fantasy/Fantasy Atlas");
        this.GetComponent<UISprite>().spriteName = "Dark";
        this.GetComponent<UIButton>().normalSprite = "Dark";
    }
}
