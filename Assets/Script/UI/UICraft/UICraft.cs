using Game.UI;
using Managers;
using System.Collections.Generic;
using UnityEngine.UI;
using Game.System;

namespace Game.UI
{
    public class UICraft : BasePanel,ICanUndo
    {
        Button btnBack;
        Button btnCraft;

        List<CraftSlot> normalMaterialSlots = new();
        List<CraftSlot> specialMaterialSlots = new();

        public override void Close()
        {
            UIManager.Close<UICraft>();
        }

        public override void InitPanel()
        {
            foreach (var normalSlot in transform.Find("BackGround").Find("NormalSlots").GetComponentsInChildren<CraftSlot>())
            {
                normalMaterialSlots.Add(normalSlot);
            }

            foreach (var specialSlot in transform.Find("BackGround").Find("SpecialSlots").GetComponentsInChildren<CraftSlot>())
            {
                specialMaterialSlots.Add(specialSlot);
            }

            btnBack = transform.Find("BtnBack").GetComponent<Button>();
            btnBack.onClick.AddListener(() => Close());
            btnCraft.onClick.AddListener(() => EventSystem.Send<CraftTriggerEvent>());
        }

        public override void Refresh()
        {
            //从ItemsSystem里刷新背包
        }

        public override void Show(IUiData uiData)
        {

        }

        public void Undo()
        {
            Close();
        }
    }
}

