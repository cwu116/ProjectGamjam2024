using Game.UI;
using Managers;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Game.System;
using TMPro;
using System;

namespace Game.UI
{
    public class UICraft : BasePanel,ICanUndo
    {
        Button btnBack;
        Button btnCraft;

        Transform description;
        TextMeshProUGUI potionNameText;
        TextMeshProUGUI mainEffectText;
        TextMeshProUGUI sideEffectText;
        TextMeshProUGUI tipsText;

        //Transform normalMat;
        //Transform specialMat;
        Transform normalMat;
        Transform specialMat;
        List<UICraftMatSlot> normalMaterialSlots = new();
        List<UICraftMatSlot> specialMaterialSlots = new();
        UICraftIcon[] normalIcons;
        UICraftIcon specialIcon;

        GameObject craftPrefab;
        GameObject recipePrefab;
        UIRecipeElement[] recipeList;
        Transform recipeListContent;
        UICraftIcon currentCraftIcon;
        Transform flask;
        Transform spawnPoint;

        Sprite rcipeNormal;
        Sprite recipeActive;
        Sprite craftIconNormal;
        Sprite craftIconActive;
        Sprite craftIconSpeicalNormal;
        Sprite craftIconSpecialActive;

        public override void Close()
        {
            UIManager.Close<UICraft>();
        }
        private void Awake()
        {
            InitPanel();
        }

        public override void InitPanel()
        {
            btnBack = transform.Find("BtnBack").GetComponent<Button>();
            btnCraft = transform.Find("BtnCraft").GetComponent<Button>();
            //btnNormalMatIcon = transform.Find("CraftSlots").Find("Normal").GetComponentsInChildren<UICraftIcon>();
            //btnNormalMatIcon = transform.Find("CraftSlots").Find("Normal").GetComponentsInChildren<UICraftIcon>();
            description = transform.Find("Description");
            potionNameText = description.Find("Name").GetComponent<TextMeshProUGUI>();
            mainEffectText = description.Find("MainEffect").GetComponent<TextMeshProUGUI>();
            sideEffectText = description.Find("SideEffect").GetComponent<TextMeshProUGUI>();
            tipsText = description.Find("Tips").GetComponent<TextMeshProUGUI>();
            recipeListContent = transform.Find("RecipeList").GetComponent<ScrollRect>().content;
            recipeList = recipeListContent.GetComponentsInChildren<UIRecipeElement>();
            normalMat = transform.Find("Materials").Find("Normal");
            specialMat = transform.Find("Materials").Find("Special");
            normalIcons = transform.Find("CraftSlots").Find("Normal").GetComponentsInChildren<UICraftIcon>();
            specialIcon = transform.Find("CraftSlots").Find("SpecialSlot").GetComponent<UICraftIcon>();
            flask =transform.Find("Flask");
            spawnPoint = flask.Find("SpawnPoint");

            craftPrefab = Resources.Load<GameObject>("Prefabs/Prefab/UI/UICraftElement");
            recipePrefab = Resources.Load<GameObject>("Prefabs/Prefab/UI/UIRecipeElement");

            foreach (var normalSlot in transform.Find("Materials").Find("Normal").GetComponentsInChildren<UICraftMatSlot>())
            {
                normalMaterialSlots.Add(normalSlot);
            }

            foreach (var specialSlot in transform.Find("Materials").Find("Special").GetComponentsInChildren<UICraftMatSlot>())
            {
                specialMaterialSlots.Add(specialSlot);
            }




            btnBack.onClick.AddListener(() => Close());
            btnCraft.onClick.AddListener(() => EventSystem.Send<CraftTriggerEvent>());
            EventSystem.Register<UICraftIconClickEvent>(v => OnUICraftIconClicked(v.craftIcon));
            EventSystem.Register<UICraftMaterialClickEvent>(v => OnUICraftMaterialClicked(v.item));
            EventSystem.Register<RefreshBackpackUIEvent>(v => OnRefreshBackpackUI(v));
            EventSystem.Register<CraftResultEvent>(v => OnCraftResult(v));
        }


        private void OnUICraftIconClicked(UICraftIcon c)
        {
            currentCraftIcon = c;
            if(currentCraftIcon.isSpecial)
            {
                specialMat.gameObject.SetActive(true);
                normalMat.gameObject.SetActive(false);
            }
            else
            {
                specialMat.gameObject.SetActive(false);
                normalMat.gameObject.SetActive(true);
            }
            EventSystem.Send<RefreshBackpackUIRequest>();
        }

        Dictionary<UICraftIcon, GameObject> UICraftElements=new();
        private void OnUICraftMaterialClicked(Item_s item)
        {
            if (currentCraftIcon != null)
            {
                if (currentCraftIcon.item != null)
                {
                    //卸下材料 增加物品
                    if (UICraftElements.ContainsKey(currentCraftIcon))
                    {
                        Destroy(UICraftElements[currentCraftIcon]);
                        UICraftElements.Remove(currentCraftIcon);
                    }
                    EventSystem.Send(new CraftRemoveMaterialEvent() { item=item});
                }
                currentCraftIcon.item = item;
                currentCraftIcon.icon.sprite = item.sprite;
                EventSystem.Send(new CraftAddMaterialEvent() { item = item });
                //装上材料  减少物品
                GameObject go= GameObject.Instantiate(craftPrefab, flask);
                go.transform.position = spawnPoint.position;
                go.GetComponent<Image>().sprite = item.sprite;
                UICraftElements[currentCraftIcon] = go;
            }
            EventSystem.Send<RefreshBackpackUIRequest>();
        }

        public override void Refresh()
        {
            currentCraftIcon = null;
            UICraftElements = null;
            RefreshCraftIconState();
            EventSystem.Send<RefreshBackpackUIRequest>();
        }

        private void OnRefreshBackpackUI(RefreshBackpackUIEvent v)
        {
            for (int i = 0; i < normalMaterialSlots.Count; i++)
            {
                var slot = normalMaterialSlots[i];
                slot.icon.gameObject.SetActive(i < v.normalItems.Count );
                if(i<v.normalItems.Count)
                    slot.Refresh(v.normalItems[i]);
            }
            for (int i = 0; i < specialMaterialSlots.Count; i++)
            {
                var slot = specialMaterialSlots[i];
                slot.icon.gameObject.SetActive(i < v.specialItems.Count);
                if (i < v.specialItems.Count)
                    slot.Refresh(v.specialItems[i]);
            }
        }

        private void OnCraftResult(CraftResultEvent v)
        {
            //合成药水表现
            EventSystem.Send<RefreshBackpackUIRequest>();
        }


        void RefreshCraftIconState()
        {
            foreach(var i in normalIcons)
            {
                i.GetComponent<Image>().sprite = i.item == null ? craftIconNormal : craftIconActive;
            }
            specialIcon.GetComponent<Image>().sprite = specialIcon.item == null ? craftIconSpeicalNormal : craftIconSpecialActive;
        }

        void ShowDescription(UIRecipeElement element)
        {
            foreach(var e in recipeList)
            {
                if(e==element)
                {
                    e.GetComponent<Image>().sprite = recipeActive;
                }
                else
                    e.GetComponent<Image>().sprite = rcipeNormal;
            }
            description.gameObject.SetActive(true);
            //刷新Description
        }

        void CloseDescription()
        {
            description.gameObject.SetActive(false);
        }

        private void OnEnable()//各种回收
        {
            foreach(var iconSlot in normalIcons)
            {
                if(iconSlot.item!=null)
                {
                    EventSystem.Send(new CraftRemoveMaterialEvent() { item = iconSlot.item });
                    iconSlot.item = null;
                    iconSlot.icon.sprite = null;
                }
            }
            if(specialIcon.item!=null)
            {
                EventSystem.Send(new CraftRemoveMaterialEvent() { item = specialIcon.item });
                specialIcon.item = null;
                specialIcon.icon.sprite = null;
            }

            foreach(var element in UICraftElements)
            {
                Destroy(element.Value);
            }
            UICraftElements.Clear();
        }


        public void Undo()
        {
            Close();
        }
    }
}

