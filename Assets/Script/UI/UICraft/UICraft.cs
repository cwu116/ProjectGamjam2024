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

        Dictionary<UICraftIcon, GameObject> UICraftElements;

        public override void Close()
        {
            UIManager.Close<UICraft>();
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

            craftPrefab = Resources.Load<GameObject>("Prefabs/UI/UICraftElement");
            recipePrefab = Resources.Load<GameObject>("Prefabs/UI/UIRecipeElement");

            normalMaterialSlots.AddRange(transform.Find("Materials").Find("Normal").GetComponentsInChildren<UICraftMatSlot>());
            //foreach (var normalSlot in transform.Find("Materials").Find("Normal").GetComponentsInChildren<UICraftMatSlot>())
            //{
            //    normalMaterialSlots.Add(normalSlot);
            //}
            specialMaterialSlots.AddRange(transform.Find("Materials").Find("Special").GetComponentsInChildren<UICraftMatSlot>());
            //foreach (var specialSlot in transform.Find("Materials").Find("Special").GetComponentsInChildren<UICraftMatSlot>())
            //{
            //    //specialMaterialSlots.Add(specialSlot);
            //}

            for (int i = 0; i < normalMaterialSlots.Count; i++)
            {
                var slot = normalMaterialSlots[i];
                slot.icon = slot.transform.Find("Icon").GetComponent<Image>();
                slot.countText = slot.transform.Find("Count").GetComponent<TextMeshProUGUI>();
                slot.matName = slot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            }
            for (int i = 0; i < specialMaterialSlots.Count; i++)
            {
                var slot = specialMaterialSlots[i];
                slot.icon = slot.transform.Find("Icon").GetComponent<Image>();
                slot.countText = slot.transform.Find("Count").GetComponent<TextMeshProUGUI>();
                slot.matName = slot.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            }
            craftIconNormal = Resources.Load<Sprite>(UIImagePath.ImagePath + "材料框");
            craftIconSpeicalNormal = Resources.Load<Sprite>(UIImagePath.ImagePath + "特殊材料框");
            craftIconActive = Resources.Load<Sprite>(UIImagePath.ImagePath + "材料框空");
            craftIconSpecialActive = Resources.Load<Sprite>(UIImagePath.ImagePath + "特殊材料框空");
            UICraftElements = new Dictionary<UICraftIcon, GameObject>();



            btnBack.onClick.AddListener(() => Close());
            btnCraft.onClick.AddListener(() => { EventSystem.Send<CraftTriggerEvent>(); });
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

        
        private void OnUICraftMaterialClicked(Item_s item)
        {
            if (currentCraftIcon != null)
            {
                if (currentCraftIcon.item != null)
                {
                    //ж�²��� ������Ʒ
                    if (UICraftElements.ContainsKey(currentCraftIcon))
                    {
                        Destroy(UICraftElements[currentCraftIcon]);
                        UICraftElements.Remove(currentCraftIcon);
                    }
                    EventSystem.Send(new CraftRemoveMaterialEvent() { item=item});
                }
                currentCraftIcon.item = item;
                //currentCraftIcon.icon.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + item.Id);
                EventSystem.Send(new CraftAddMaterialEvent() { item = item });
                //װ�ϲ���  ������Ʒ
                GameObject go= GameObject.Instantiate(craftPrefab, flask);
                go.transform.position = spawnPoint.position;
                go.GetComponent<Image>().sprite =Resources.Load<Sprite>(UIImagePath.ImagePath+item.Id);
                UICraftElements[currentCraftIcon] = go;
            }
            EventSystem.Send<RefreshBackpackUIRequest>();
            RefreshCraftIconState();
            RefreshCraftIcon();
        }

        public override void Refresh()
        {
            currentCraftIcon = null;
            RefreshCraftIconState();
            RefreshCraftIcon();
            Dictionary<UICraftIcon, GameObject> UICraftElements = new Dictionary<UICraftIcon, GameObject>();
            EventSystem.Send<RefreshBackpackUIRequest>();
        }

        private void OnRefreshBackpackUI(RefreshBackpackUIEvent v)
        {
            if (v.normalItems == null && v.specialItems == null)
                return;
            for (int i = 0; i < normalMaterialSlots.Count; i++)
            {
                var slot = normalMaterialSlots[i];
                slot.SetActive(i < v.normalItems.Count );
                if(i<v.normalItems.Count)
                    slot.Refresh(v.normalItems[i]);
            }
            for (int i = 0; i < specialMaterialSlots.Count; i++)
            {
                var slot = specialMaterialSlots[i];
                slot.SetActive(i < v.specialItems.Count);
                if (i < v.specialItems.Count)
                    slot.Refresh(v.specialItems[i]);
            }
        }

        private void RefreshCraftIcon()
        {
            foreach (var i in normalIcons)
            {
                i.icon.gameObject.SetActive(!(i.item == null));
                if (i.item != null)
                    i.icon.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + i.item.Id);
            }
            specialIcon.icon.gameObject.SetActive(!(specialIcon.item == null));
            if (specialIcon.item != null)
                specialIcon.icon.sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + specialIcon.item.Id);
        }

        private void OnCraftResult(CraftResultEvent v)
        {
            if (v.result != null)
            {
                foreach (var i in normalIcons)
                {
                    i.item = null;
                }
            }
            RefreshCraftIcon();
            foreach (var element in UICraftElements)
            {
                Destroy(element.Value);
            }
            UICraftElements.Clear();
            RefreshCraftIconState();


            //�ϳ�ҩˮ����
            EventSystem.Send<RefreshBackpackUIRequest>();
        }


        void RefreshCraftIconState()
        {
            //foreach(var i in normalIcons)
            //{
            //    i.icon.sprite = i.item==null?null:Resources.Load<Sprite>(UIImagePath.ImagePath+i.item.Id);
            //}
            //specialIcon.icon.sprite = specialIcon.item == null ? null : Resources.Load<Sprite>(UIImagePath.ImagePath + specialIcon.item.Id);
            foreach (var i in normalIcons)
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
            //ˢ��Description
        }

        void CloseDescription()
        {
            description.gameObject.SetActive(false);
        }

        private void OnDisable()//���ֻ���
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

