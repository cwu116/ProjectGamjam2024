using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using Game.System;
using System.Collections.Generic;
using System;

public class UIMain : BasePanel
{
    Button btnCraft;
    UIPotion[] potions;
    GameObject potionPrefab;

    public UIPoitonDescription descriptionUI;
    public Item_Data currentPotion;
    public override void Close()
    {
        
    }

    public override void InitPanel()
    {
        btnCraft = transform.Find("BtnCraft").GetComponent<Button>();
        potions = transform.Find("Potions").GetComponentsInChildren<UIPotion>();
        descriptionUI = transform.Find("PotionDescription").GetComponent<UIPoitonDescription>();
        btnCraft.onClick.AddListener(() => EventSystem.Send<OpenCraftUITrigger>());//ÔÚCraftSystemÌí¼ÓUndo

        EventSystem.Register<RefreshBackpackUIEvent>(v => OnRefreshBackpackUI(v));
    }

    private void OnRefreshBackpackUI(RefreshBackpackUIEvent v)
    {
        for(int i=0;i<potions.Length;i++)
        {
            potions[i].gameObject.SetActive(i < v.potions.Count);
        }
        for(int i=0;i< v.potions.Count;i++)
        {
            potions[i].Init(v.potions[i],this);
        }
    }

    public override void Refresh()
    {
       
    }



    //public override void Show(IUiData uiData)
    //{
        
    //}
}
