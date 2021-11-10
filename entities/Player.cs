using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    public class Player : Sprite
    {
        public new string componentID = "player";
        public List<String> inventory { get; set; }
        public Player(Dictionary<string, Animation> animations)
            : base(animations){}
        public override void Move(Input input)
        {
            if (Keyboard.GetState().IsKeyDown(input.Inventory))
            {
                //open inventory
            }
            base.Move(input);
        }
    }
}
