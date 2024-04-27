using System.Collections.Generic;
using Buff;
using UnityEngine;

namespace Game.System
{
    public interface IState
    {
        public void StateWithStart() {}
        public void StateWithEnd() {}
    }
}

