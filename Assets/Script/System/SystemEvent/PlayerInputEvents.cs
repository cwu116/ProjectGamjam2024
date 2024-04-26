
namespace Game.System
{
   struct OnMouseRightClick:IEvent
    { }

    struct OnClickPotion:IEvent
    {

    }

    struct UndoEvent:IEvent
    {
        ICanUndo undoOperation;
    }
}
