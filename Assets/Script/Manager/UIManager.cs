using UnityEngine;
using Game.System;
using Game.UI;
using System.Collections.Generic;
using System;

namespace Managers
{
    class UIElement
    {
        public GameObject instance;
        public string ResourcePath;
    }
    static class UIManager
    {
        const string UIPath = "Prefab/UI/";
        static Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

        //////////////////////////路径后面加的名字与prefab的名称保持一致/////////////////////
        static UIManager()
        {
            UIResources[typeof(DemoUI)] = new UIElement { ResourcePath = UIPath + "DemoUI" };
        }

        public static T Show<T>() where T : BasePanel
        {
            Type type = typeof(T);
            if (!UIResources.ContainsKey(type))
            {
                Debug.LogError("Show:"+type + "没有在UIManager中注册");
                return default;
            }
            UIElement element = UIResources[type];

            if (element.instance == null)//第一次加载
            {
                GameObject prefab = Resources.Load(element.ResourcePath) as GameObject;
                if (prefab == null)
                {
                    Debug.LogError(element.ResourcePath + " 没有找到对应预制体");
                    return default;
                }
                element.instance = GameObject.Instantiate(prefab);
                (element.instance.GetComponent<T>() as BasePanel).InitPanel();
                (element.instance.GetComponent<T>() as BasePanel).Refresh();
                return element.instance.GetComponent<T>();
            }
            else //OnEnable
            {
                element.instance.SetActive(true);
                (element.instance.GetComponent<T>() as BasePanel).Refresh();
                return element.instance.GetComponent<T>();
            }
        }

        public static void Close<T>() where T : BasePanel
        {
            if (!UIResources.ContainsKey(typeof(T)))
            {
                Debug.Log("Close:" + typeof(T) + " 没有在UIManager注册");
                return;
            }
            UIResources[typeof(T)].instance.SetActive(false);
        }
    }
}
