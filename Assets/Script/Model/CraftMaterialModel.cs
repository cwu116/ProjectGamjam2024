using Assets.Script.Config;
using Game.System;
using Game.Util;
using MainLogic.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Data
{
    public string id;
    public string Name;
    public string Material_1;
    public string Material_2;
    public string MaterialSpecial;
    public string Description;
    public List<string> Type;
}
public class Item_s
{
    private string id;
    private string name;
    private string description;
    public int quantity;

    public string Id
    {

        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public Item_s(string id, string name, string description, int quantity)
    {
        this.name = name;
        this.description = description;
        this.id = id;
        this.quantity = quantity;
    }
}
namespace Game.Model
{
 
    public class CompoundModel : BaseModel
    {
        public List<Item_Data> Item_Data;
        public override void InitModel()
        {
             Item_Data= JsonUtil.ToObject<List<Item_Data>>(
               ResourcesManager.LoadText(JsonPath.ItemPath, JsonFileName.CommpoundData));
        }
    }
   

}
