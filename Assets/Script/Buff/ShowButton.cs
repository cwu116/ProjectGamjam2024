using System;
using System.Collections.Generic;
using Buff.Config;
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

        public ValueKey testIndex;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SoundLog);
        }

        private void Start()
        {
            targetPlayer = GameObject.FindWithTag("Player");
        }

        void SoundLog()
        {
            // Debug.Log(targetPlayer.GetComponent<Player>().Hp);
            GameBody.GetSystem<MoveSystem>().EnemyMoveTo(FindObjectsOfType<Enemy>()[0].gameObject);
        }
        
    }
}
