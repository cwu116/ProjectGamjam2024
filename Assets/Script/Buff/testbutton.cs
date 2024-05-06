using System.Collections.Generic;
using Buff.Tool;
using Game;
using Game.Model;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Buff.Test
{
    public class testbutton : MonoBehaviour
    {
        public int Pointer;         // 实验性：以索引获得药剂
        public List<Potion> PotionDatas;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(delegate
            {
                Debug.Log(GameBody.GetModel<CompoundModel>().Item_Data.Count);
                Debug.LogError(GameBody.GetModel<CompoundModel>().Item_Data[Pointer].Name);
                EventSystem.Send<CraftResultEvent>(new CraftResultEvent{ result = GameBody.GetModel<CompoundModel>().Item_Data[Pointer]});
            });
            // _button.onClick.AddListener(delegate
            // {
            //     StateSystem.Execution(PotionDatas[Pointer].containBuffs, GameObject.Find("Player"));
            // });
            
        }
    }
}
