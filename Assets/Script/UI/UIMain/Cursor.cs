using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using Game.Model;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    PlayerActionModel model;
    private void Start()
    {
        model = GameBody.GetModel<PlayerActionModel>();
        
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
        gameObject.SetActive(model.currentPotion != null);
        if (model.currentPotion != null)
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>(UIImagePath.ImagePath + model.currentPotion.id);
    }
}
