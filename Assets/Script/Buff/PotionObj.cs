using System;
using System.Collections.Generic;
using UnityEngine;

namespace Potion
{
    // 读表数据结构体
    [Serializable]
    public struct Potion
    {
        public string name;                    // 药水名
        public string description;             // 药水描述
        public Sprite texture;                 // 贴图
        public List<string> needs;             // 需要的草药，暂定string
        public string special;                 // 特殊草药
        public List<string> containBuffs;   // 药剂buff集
    }
    public class PotionObj : MonoBehaviour
    {
        public Potion info { get; set; }


        
    }
}
