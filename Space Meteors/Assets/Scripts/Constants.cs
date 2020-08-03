using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public static class Constants
    {
        public static class InputAxis
        {
            public const string horizontalAxis = "Horizontal";
            public const string fire1 = "Fire1";
            public const string submit = "Submit";
            public const string cancel = "Cancel";
        }

        public static class GameSceneObjects
        {
            public const string shootingLocation = "ShootingLocation";
        }

        public static class CollidableNames
        {
            public const string missileInstances = "Missile(Clone)";
            public const string meteorMissileInstances = "MeteorMissile(Clone)";
            public const string player = "Player";
        }

        public static class Tags
        {
            public const string player = "Player";
        }
        public static class Scenes
        {
            public const string game = "Game";
            public const string mainMenu = "Home";
        }
    }
}
