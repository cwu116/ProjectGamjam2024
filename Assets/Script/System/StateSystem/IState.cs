using System.Collections.Generic;
using Buff;
using UnityEngine;

namespace Game.System
{
    public interface IState
    {
        public void PlayerStatesStart() {}
        public void PlayerStatesEnd() {}
        public void EnemyStatesStart() {}
        public void EnemyStatesEnd() {}
        public void CheckForDelay(){}
    }
}

