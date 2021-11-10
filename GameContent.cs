using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace rpg
{
    class GameContent
    {
        public Texture2D playerSprite { get; set; }

        public GameContent(ContentManager content)
        {
            //load images
            this.loadImages(content);
        }

        private void loadImages(ContentManager content)
        {
            playerSprite = content.Load<Texture2D>("player");
        }
    }
}
