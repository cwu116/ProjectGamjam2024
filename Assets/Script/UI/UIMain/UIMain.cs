using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using Game.System;
using System.Collections.Generic;
using System;
using MainLogic.Manager;
using Managers;

public class UIMain : BasePanel
{
    Button btnCraft;
    UIPotion[] potions;
    GameObject potionPrefab;

    private HorizontalLayoutGroup hpBar;

    public UIPoitonDescription descriptionUI;
    public Item_Data currentPotion;

    // Single Instance Mode Construstion
    public static UIMain Instance;

    private void Awake()
    {
        Instance = this;
    }

    public override void Close()
    {
    }


    private void Start()
    {
        InitPanel();
        Invoke("RefreshPlayerHp", 0.1f);
        EventSystem.Send<RefreshBackpackUIRequest>();
    }

    public override void InitPanel()
    {
        btnCraft = transform.Find("BtnCraft").GetComponent<Button>();
        potions = transform.Find("Potions").GetComponentsInChildren<UIPotion>();
        descriptionUI = transform.Find("PotionDescription").GetComponent<UIPoitonDescription>();
        //btnCraft.onClick.AddListener(() => EventSystem.Send<OpenCraftUITrigger>());//在CraftSystem添加Undo
        btnCraft.onClick.AddListener(() => UIManager.Show<UICraft>());

        hpBar = transform.Find("HpBar").GetComponent<HorizontalLayoutGroup>();
        
        EventSystem.Register<RefreshBackpackUIEvent>(v => OnRefreshBackpackUI(v));
    }

    private void OnRefreshBackpackUI(RefreshBackpackUIEvent v)
    {
        for (int i = 0; i < potions.Length; i++)
        {
            potions[i].gameObject.SetActive(i < v.potions.Count);
        }

        for (int i = 0; i < v.potions.Count; i++)
        {
            potions[i].Init(v.potions[i], this);
        }
    }

    public override void Refresh()
    {
        RefreshPlayerHp();
    }

    public void RefreshPlayerHp()
    {
        foreach (Transform child in hpBar.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < FindObjectOfType<Player>().Hp; i++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/Heart"), hpBar.transform).GetComponent<Heart>()
                .isPlayer = true;
        }
    }


    //public override void Show(IUiData uiData)
    //{

    //}
}