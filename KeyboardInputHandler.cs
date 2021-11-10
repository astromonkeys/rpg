using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

/*
 * This class is the master input handler for the game. Its job is to process 
 * the user's input(just keyboard for now, might make a different handler for mouse)
 * based on the current game's state. For example, if the game state is normal gameplay,
 * WASD(or whatever the controls are set to, implement this in user options later) would
 * move the player. If the game's state is a typing screen, WASD would type letters instead.
 */
namespace rpg
{
    class KeyboardInputHandler
    {
        //set of keybinds/controls
        public Input input;

        public KeyboardInputHandler()
        {
            input = new Input()
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Inventory = Keys.E,
            };
        }
        public KeyboardInputHandler(Input inputs) : this() { } //use when player customizable controls are implemented
        
        public void Update(GameTime gameTime, GameState currentGameState, List<Component> gameComponents) 
        {
            /*
             * Current state is gameplay, meaning we process input as a user control to control the player
             * Such as E opening the inventory, WASD moving
             */
            if (currentGameState.name.Equals("gameplay"))
            {
                //update all game components(currently just house and player)
                foreach (var component in gameComponents)
                    component.Update(gameTime, input);
            }
            else { //nothing yet
                 }
            // take keyboard input and perform different actions based on teh current game state
        }
    }
}
