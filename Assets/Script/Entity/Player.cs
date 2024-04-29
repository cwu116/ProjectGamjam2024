using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Medicalment
{
    public MedicalmentModel medModel;
    public int num;
}
public class Player : BaseEntity
{
    private Medicalment myMedicalment;

    public Player(string name = "Player")
    {
        this.SetModel(name);
    }

    public void SynthesisMedicalment(SyntheticMedical sm, MaterialType mt)
    {
        this.RestMoveTimes--;
    }


    public override void UseSkill(BaseEntity target)
    {
        base.UseSkill(target);

    }

    public void ReleaseMedicalment(BaseEntity target)
    {
        this.RestMoveTimes--;
    }

    public void MoveToTarget(HexCell target)
    {

    }
}
