using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = GameObject.Find("Enemys").transform.Find("Player(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
