using System.Collections.Generic;
using Game.System;


namespace Game.Model
{
    //class PlayerActionModel : BaseModel
    //{
    //    Stack<ICanUndo> _undoStack = new();
    //    public override void InitModel()
    //    {
    //        EventSystem.Register<AfterPlayerTurnEndEvent>(ClearOperations);
    //    }

    //    public void AddOperation(ICanUndo operation)
    //    {
    //        _undoStack.Push(operation);
    //    }

    //    public void UndoOperation()
    //    {
    //        ICanUndo operation;
    //        _undoStack.TryPop(out operation);
    //        if (operation != null)
    //            operation.Undo();
    //    }

    //    public void ClearOperations(AfterPlayerTurnEndEvent obj = default(AfterPlayerTurnEndEvent))
    //    {
    //        _undoStack.Clear();
    //    }
    //}
}
