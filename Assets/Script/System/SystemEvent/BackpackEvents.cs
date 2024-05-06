using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System
{
    struct AddItemEvent:IEvent
    {
        public Item_s item;
        public int count;
    }

    struct UnlockRecipe:IEvent
    {
        public Item_Data potion;
    }
}
