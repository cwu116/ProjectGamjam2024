using UnityEngine;
using Game.System;

namespace Controller
{
    public class PlayerInputController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(1))
                EventSystem.Send<OnMouseRightClick>();
        }
    }
}

