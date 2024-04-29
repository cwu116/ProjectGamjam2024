using System.Collections.Generic;
using Buff.Tool;
using Game;
using Game.System;
using UnityEngine;
using UnityEngine.UI;

namespace Buff.Test
{
    public class ShowButton : MonoBehaviour
    {
        private Button _button;

        public GameObject targetPlayer;

        public int testIndex;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(delegate
            {
                Debug.Log(targetPlayer.GetComponent<PlayTemplate>().ShowParam(testIndex));
            });
        }
    }
}
