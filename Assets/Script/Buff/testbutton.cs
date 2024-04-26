using System.Collections.Generic;
using Buff.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Buff.Test
{
    public class testbutton : MonoBehaviour
    {
        public int Pointer;         // 实验性：以索引获得药剂
        public List<Potion.Potion> PotionDatas;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(delegate
            {
                BuffManager.Execution(PotionDatas[Pointer].containBuffs, GameObject.Find("Player"));
            });
        }
    }
}
