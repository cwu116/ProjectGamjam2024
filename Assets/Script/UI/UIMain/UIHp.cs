using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.GameBody;
using Game.System;
using MainLogic.Manager;

namespace Game
{
    public class UIHp : MonoBehaviour
    {
        public Transform Hp0;
        public Transform Hp1;
        public Transform Hp2;
        public Transform Hp3;
        public Transform Hp4;
        public Transform Hp5;
        public float CurrentHp;
        private List<Transform> HpList;
        private int MaxHp = 0;
        void Start()
        {
            if (MaxHp == 0)
            {
                EventSystem.Register<HpUIEvent>(InitializationHp);
            }
            else
            {
                EventSystem.Register<HpUIEvent>(HpChange);
            }
        }

        /// <summary>
        /// 绑定血条
        /// </summary>
        /// <param name="PrefabsRoute">路径</param>
        /// <param name="gameObject"> 对象名</param>
        /// <param name="Range"> 血条与对象的距离</param>
        /// <returns></returns>
        void MountHp(GameObject gameObject,string PrefabsRoute,int Range)
        {
            GameObject Prefabs = ResourcesManager.LoadPrefab(PrefabsRoute, "MonsterHp");
            Prefabs.transform.SetParent(gameObject.transform);
            Vector3 GameObjectV3 = this.transform.parent.transform.position;//获取父物体位置
            Vector3 PrefabsV3 = GameObjectV3;
            PrefabsV3.y += Range;//在敌人头上的距离
            Prefabs.transform.position = PrefabsV3;
        }

        //初始化HpUI
        void InitializationHp(HpUIEvent hpUIEvent)
        {
            HpList = new List<Transform>();
            HpList.Add(Hp0);
            HpList.Add(Hp1);
            HpList.Add(Hp2);
            HpList.Add(Hp3);
            HpList.Add(Hp4);
            HpList.Add(Hp5);
            for (int i = 5; i >= hpUIEvent.CurrentHp; i--)
            {
                HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
            }
            CurrentHp = hpUIEvent.CurrentHp;
        }

        //改变为该血量
        void HpChange(HpUIEvent hpUIEvent)
        {
            if (CurrentHp >= 0 && CurrentHp <= MaxHp)
            {
                int OperationHp = (int)(hpUIEvent.CurrentHp + hpUIEvent.CurrentHp);
                switch (OperationHp)
                {
                    case 0:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 1:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 2:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 3:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 4:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 5:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 6:
                        HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 7:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 8:
                        HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 9:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 10:
                        HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    case 11:
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        HpList[(int)hpUIEvent.CurrentHp].GetChild(0).GetComponent<Image>().enabled = true;
                        break;
                    case 12:
                        HpList[(int)hpUIEvent.CurrentHp - 1].GetChild(1).GetComponent<Image>().enabled = true;
                        for (int i = 5; i >= OperationHp / 2; i--)
                        {
                            HpList[i].GetChild(1).GetComponent<Image>().enabled = false;
                            HpList[i].GetChild(0).GetComponent<Image>().enabled = false;
                        }
                        break;
                    default:
                        Debug.Log("血量不符合规范");
                        break;
                }
            }
            
        }
    }
}