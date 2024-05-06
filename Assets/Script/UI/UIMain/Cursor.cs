using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Model;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public static Cursor instance;
    PlayerActionModel model;
    private void Start()
    {
        instance = this;
        model = GameBody.GetModel<PlayerActionModel>();
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
        Debug.Log(model.CurrentPotion);
        gameObject.SetActive(model.CurrentPotion != null);
        if (model.CurrentPotion != null)
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>(UIImagePath.ImagePath +"绿");
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
