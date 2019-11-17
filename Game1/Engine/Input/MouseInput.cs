﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Engine.Entity;

namespace Game1.Engine.Input
{
    public class MouseInput : IMouseInputObservable
    {
        private static List<IMouseInputObserver> m_subList = new List<IMouseInputObserver>();
        private MouseState mouse;

        public MouseInput()
        {
        }

        
        public static void Subscribe(IMouseInputObserver sub)
        {
            m_subList.Add(sub);
        }

        public void Update()
        {
            mouse = Mouse.GetState();
            notifyInput(new Vector2(mouse.X, mouse.Y), (mouse.LeftButton == ButtonState.Pressed));
        }

        public void notifyInput(Vector2 pos, bool pressed)
        {
            foreach(var sub in m_subList)
            {
                sub.mouseInput(pos, pressed);
            }
        }
    }
}