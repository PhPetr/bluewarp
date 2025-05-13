using Microsoft.Xna.Framework;
using Nez;
using Nez.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluewarp
{
    internal class RunGameScene : Scene
    {
        public override void Initialize()
        {
            ClearColor = Color.Blue;
            
            var tex = Content.LoadTiledMap(Nez.Content.Level1.levelone); // JEN PRIKLAD pouziti!!!!!!!!!!

        }
    }
}
