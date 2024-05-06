using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.System;

namespace Assets.Script.GameBody
{
    public struct GameReadyEvent : IEvent
    {

    }

    public struct GameStartEvent : IEvent
    {

    }
    public struct GamePassEvent : IEvent
    {

    }

    public struct HpUIEvent : IEvent
    {
        public float CurrentHp;
    }

}
