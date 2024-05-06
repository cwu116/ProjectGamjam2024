using System.Collections.Generic;
using Game.System;
using System;

namespace Game.Model
{
    class PlayerActionModel : BaseModel
    {
        Stack<ICanUndo> _undoStack = new();
        public Item_Data CurrentPotion;
        public override void InitModel()
        {
            EventSystem.Register<AfterPlayerTurnEndEvent>(ClearOperations);
        }

        public void AddOperation(ICanUndo operation)
        {
                _undoStack.Push(operation);
        }

        public void UndoOperation()
        {
            if (CurrentPotion != null)
            {
                CurrentPotion = null;
                GameBody.GetSystem<MapSystem>().ClearHighlightCells();
                GameBody.GetSystem<MapSystem>().HighLightCells(Player.instance.MoveTimes > 0 ? Player.instance.StepLength > 0 ? 1 : 0 : 0);
                return;
            }
            ICanUndo operation;
            _undoStack.TryPop(out operation);
            if (operation != null)
            {
                operation.Undo();
            }
        }

        public void ClearOperations(AfterPlayerTurnEndEvent obj = default(AfterPlayerTurnEndEvent))
        {
            _undoStack.Clear();
        }
    }
}
