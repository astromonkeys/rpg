using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    public class Sprite: Component
    {
        #region Fields

        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight); }
        }

        public float Speed = 3f;

        public Vector2 Velocity;

        private Boolean animatedSprite = false;

        #endregion

        #region Methods

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        public virtual void Move(Input input)
        {
            if (Keyboard.GetState().IsKeyDown(input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(input.Down))
                Velocity.Y = Speed;
            else if (Keyboard.GetState().IsKeyDown(input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(input.Right))
                Velocity.X = Speed;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["WalkLeft"]);
            else if (Velocity.Y > 0)
                _animationManager.Play(_animations["WalkDown"]);
            else if (Velocity.Y < 0)
                _animationManager.Play(_animations["WalkUp"]);
            else _animationManager.Stop();
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
            animatedSprite = true;
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Update(GameTime gameTime) //not used... yet
        {
            throw new NotImplementedException();
        }
        public override void Update(GameTime gameTime, Input input)
        {
            if (animatedSprite) { 
                Move(input);

                SetAnimations();

                _animationManager.Update(gameTime);

                Position += Velocity;
                Velocity = Vector2.Zero;
            }
        }

        #endregion
    }
}
