﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Game1.Engine.Input
{
    interface IKeyboardInputObserverable
    {
        void notifyInput(Keys keys);
    }
}