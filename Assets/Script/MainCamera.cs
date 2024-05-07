using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originalPos;
    async void  Start()
    {
        originalPos = this.transform.position;
        this.transform.parent = GameObject.Find("Enemys").transform.Find("Player(Clone)");
        Vector3 target = GameObject.Find("Transport").transform.position;
        this.transform.DOMove(new Vector3(target.x, target.y, this.transform.position.z), 3);
        await System.Threading.Tasks.Task.Delay(3000);
        this.transform.DOMove(originalPos, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
