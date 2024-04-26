using System.Collections;
using System.Collections.Generic;
using Buff;
using Game.Model;
using UnityEngine;

namespace Game.System
{
    public partial class StateSystem : BaseSystem
    {
        // 回合前执行
        private List<string> startExec;
        private List<string> endExec;
    }
}