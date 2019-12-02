﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Entity
{

    /// <summary>
    /// Abstraction for any game entity
    /// </summary>
    abstract class Entity : iEntity
    {
        public event EventHandler<EntityRequestArgs> EntityRequested;

        public virtual void OnEntityRequested(Vector2 pos, string texture, Type pType)
        {
            EntityRequested?.Invoke(this, new EntityRequestArgs() { Position = pos, Texture = texture, type = pType });
        }


        #region Properties

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Velocity
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;

        } = 0f;

        public Vector2 Origin
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get;
            set;
        }
        
        public string TextureString
        {
            get;
            set;
        }

        public Guid UID
        {
            get;
            set;
        }

        public String UName
        {
            get;
            set;
        }

        public float maxHealth
        {
            get;
            set;
        }

        public float currentHealth
        {
            get;

            set;
        }

        public Rectangle HitBox
        {
            
            get
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y,
                    Texture.Width, Texture.Height);
            }
        }

        public bool Visible
        {
            get;
            set;
        }

        public float DrawPriority
        {
            get;
            set;
        } = 0f;

        public float Transparency
        {
            get;
            set;
        } = 1f;


        #region TEMPORARY COLLISION VARS REMOVE THESE BASTARDS WHEN EVENTS ARE DONE
        public bool isColliding
        {
            get;
            set;
        }

        public iEntity CollidingEntity
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region Input structs

        public struct BasicInput
        {
            public BasicInput(Keys pUp = Keys.None, Keys pDown = Keys.None, Keys pLeft = Keys.None, Keys pRight = Keys.None, Keys pSprint = Keys.None, Keys pUse = Keys.None, Keys pRotate = Keys.None)
            {
                up = pUp;
                down = pDown;
                left = pLeft;
                right = pRight;
                sprint = pSprint;
                use = pUse;
                rotate = pRotate;

                allKeys = new List<Keys>(new Keys[] { up, down, left, right, sprint, use, rotate });
                allKeys.RemoveAll((Keys key) => key == Keys.None);
            }

            public Keys up;
            public Keys down;
            public Keys left;
            public Keys right;
            public Keys sprint;
            public Keys use;
            public Keys rotate;

            public List<Keys> allKeys;

        }

        public struct GamePadInput
        {
            public Buttons rotateCW;
            public Buttons rotateACW;
            public Buttons sprint;
            public Buttons use;

            public List<Buttons> allButtons;

            public GamePadInput(Buttons pRotateCW = 0, Buttons pRotateACW = 0, Buttons pUse = 0, Buttons pSprint = 0)
            {
                rotateCW = pRotateCW;
                rotateACW = pRotateACW;
                use = pUse;
                sprint = pSprint;

                allButtons = new List<Buttons>(new Buttons[] { rotateCW, rotateACW, use, sprint, });
                allButtons.RemoveAll((Buttons key) => key == 0);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Entity()
        {

        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Entity()
        {
            Console.WriteLine("HAHAHA - I have destoryed the entity");
        }

        /// <summary>
        /// Used to set the entities 
        /// UID and UName
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <param name="name">Unique Name</param>
        public void Setup(Guid id, string name)
        {
            UID = id;
            UName = name;
        }

        //Override
        public void Setup(Guid id, string name, string tex, Vector2 pos)
        {
            UID = id;
            UName = name;
            TextureString = tex;
            Position = pos;
        }

        /// <summary>
        /// This will distribute it to all other classes 
        /// In the hierarchy and any class which implements 
        /// An update with the same signature.
        /// </summary>
        public virtual void Update()
        {

        }

        #endregion
    }
}