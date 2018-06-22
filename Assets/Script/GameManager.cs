using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject pStorePanel;
    private GameObject pGoldPanel;
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


    private void Awake()
    {
        //여기다가 시작할때 필요한 매니져를 다올려놓고 시작한다
        if (sInstance == null)
            sInstance = this;

        pStorePanel = GameObject.Find("StorePanel");
        pGoldPanel = GameObject.Find("GoldPanel");

        pStorePanel.SetActive(false);
        pGoldPanel.GetComponentInChildren<UILabel>().text = "0";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
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

        pGoldPanel.GetComponentInChildren<UILabel>().text = iGold.ToString();
    }

    public void Print()
    {
        print("dd");
    }

}
