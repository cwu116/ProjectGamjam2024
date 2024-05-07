using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using Game.System;
using System.Collections.Generic;
using System;
using MainLogic.Manager;
using Managers;
using TMPro;

public class UIMain : BasePanel
{
    Button btnCraft;
    UIPotion[] potions;
    GameObject potionPrefab;
    Transform gameSuccessPanel;
    Transform gameOverPanel;

    Button btnExplain;
    TextMeshProUGUI turnCount;

    private HorizontalLayoutGroup hpBar;

    public UIPoitonDescription descriptionUI;

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
        btnExplain = transform.Find("BtnExplain").GetComponent<Button>();
        potions = transform.Find("Potions").GetComponentsInChildren<UIPotion>();
        descriptionUI = transform.Find("PotionDescription").GetComponent<UIPoitonDescription>();
        gameSuccessPanel = transform.Find("Success");
        gameOverPanel = transform.Find("Over");
        btnCraft.onClick.AddListener(() => UIManager.Show<UICraft>());
        btnExplain.onClick.AddListener(() => UIManager.Show<UIExplain>());
        turnCount = transform.Find("TurnCounterText").GetComponent<TextMeshProUGUI>();

        hpBar = transform.Find("HpBar").GetComponent<HorizontalLayoutGroup>();
        
        EventSystem.Register<RefreshBackpackUIEvent>(OnRefreshBackpackUI);
        EventSystem.Register<GameSuccessEvent>(v => gameSuccessPanel.gameObject.SetActive(true));
        EventSystem.Register<GameOverEvent>(v => gameOverPanel.gameObject.SetActive(true));
        EventSystem.Register<TurnCountEvent>(v => turnCount.text = v.count.ToString());
    }

    private void OnDestroy()
    {
        EventSystem.UnRegister<RefreshBackpackUIEvent>(OnRefreshBackpackUI);
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