using Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public IControllable controllingTank;
        private Camera mainCam;

        void Start()
        {
            mainCam = Camera.main;
        }

        void Update()
        {
            if (controllingTank != null)
            {
                var h = Input.GetAxisRaw("Horizontal");
                var v = Input.GetAxisRaw("Vertical");
                var moveDir = new Vector2(h, v).normalized;
                controllingTank.SetMove(moveDir);

                var worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                controllingTank.SetAimingPosition(worldPos);
            }
        }

        public void SetControllingTank(IControllable controllingTank)
        {
            this.controllingTank = controllingTank;
        }
    }

}

