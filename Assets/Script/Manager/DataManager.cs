using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using System.IO;
using Assets.Script.Config;
using Game.Util;
using MainLogic.Manager;

public class DataManager : MonoSingleton<DataManager>
{
    public List<MedicalmentModel> medicals = new List<MedicalmentModel>();
    public List<AttackUnitModel> attackUnits = new List<AttackUnitModel>();
    public DataManager()
    {
        Debug.LogFormat("DataManager > DataManager()");
    }

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        //Fixme:¸üÕýÒ©¼ÁjsonÂ·¾¶
        //string json = File.ReadAllText(JsonPath.MedicalmentPath + "Medicalment.txt");
        //this.medicals = JsonUtil.ToObject<List<MedicalmentModel>>(json);

    }

}
