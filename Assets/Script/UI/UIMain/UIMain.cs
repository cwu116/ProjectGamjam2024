using UnityEngine;
using UnityEngine.UI;
using Game.UI;
using Game.System;
using System.Collections.Generic;
using System;
using MainLogic.Manager;
using Managers;
using TMPro;

public class UIMain : MonoSingleton<UIMain>,IPanel
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



    private void Start()
    {
        InitPanel();
        Invoke("RefreshPlayerHp", 0.1f);
        EventSystem.Send<RefreshBackpackUIRequest>();
    }

    public void InitPanel()
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
        EventSystem.Register<GameSuccessEvent>(OnGameSuccess);
        EventSystem.Register<GameOverEvent>(OnGameOver);
        EventSystem.Register<TurnCountEvent>(OnTurnSwitch);
    }
    private void OnDestroy()
    {
        EventSystem.UnRegister<RefreshBackpackUIEvent>(OnRefreshBackpackUI);
        EventSystem.UnRegister<GameSuccessEvent>(OnGameSuccess);
        EventSystem.UnRegister<GameOverEvent>(OnGameOver);
        EventSystem.UnRegister<TurnCountEvent>(OnTurnSwitch);
    }

    private void OnTurnSwitch(TurnCountEvent obj)
    {
        turnCount.text = obj.count.ToString();
    }

    private void OnGameOver(GameOverEvent obj)
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    private void OnGameSuccess(GameSuccessEvent obj)
    {
        gameSuccessPanel.gameObject.SetActive(true);
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

    public void Refresh()
    {
        // RefreshPlayerHp();
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

    public void Close()
    {
    }


    //public override void Show(IUiData uiData)
    //{

    //}
}