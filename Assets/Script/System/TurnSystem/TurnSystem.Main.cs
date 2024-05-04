using Game.System;
using Game.Model;

namespace Game.System
{
    public partial class TurnSystem : BaseSystem
    {
        private TurnModel _turnModel;
        public override void InitSystem()
        {
            _turnModel = GameBody.GetModel<TurnModel>();
            RegisterEvents();
        }

        
    }
}
