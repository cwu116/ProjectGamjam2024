using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game.System;

public class UIBtnRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Send<GameReStartTrigger>();
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("StartScene"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
