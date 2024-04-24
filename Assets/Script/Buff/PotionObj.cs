using System;
using System.Collections.Generic;
using UnityEngine;

namespace Potion
{
    // 读表数据结构体
    [Serializable]
    public struct Potion
    {
        public string name;                     // 药水名
        private string description;             // 药水描述
        private Sprite texture;                 // 贴图
        private List<string> needs;             // 需要的草药，暂定string
        private string special;                 // 特殊草药
        private List<Buff.Buff> containBuffs;   // 药剂buff集
    }
    public class PotionObj : MonoBehaviour
    {
        public Potion info { get; set; }


        
    }
}
