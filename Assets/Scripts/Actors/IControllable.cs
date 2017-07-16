using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors
{

    public interface IControllable
    {
        void SetAimingPosition(Vector3 worldPos);
        void SetMove(Vector2 moveDir);
        void ActivateWeapon(int weaponIndex);
    }
}

