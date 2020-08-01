using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    interface IActions
    {
        #region Movement

        void Move();
        bool CanMove(float[] worldBounds, GameObject obj);

        #endregion

        #region Firing

        void Fire();
        bool CanFire();

        #endregion

    }
}