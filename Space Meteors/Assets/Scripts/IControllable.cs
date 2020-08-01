using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    /*I think a cool feature would be a 2 Player game so this is a thing for the "future"*/
    interface IControllable
    {
        /**/
        bool IsPlayerFiring();
        bool IsPlayerMoving();
    }
}
