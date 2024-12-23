﻿using System.Collections.Generic;

using ZL.CS.ConsoleEngine;

namespace ZL.CS.Graphics
{
    public sealed class Foreground : Graphic
    {
        public const byte defaultColor = 007;

        public readonly List<FixedChar>[] textMap;

        public Foreground(byte[,] colorMap, params string[] textMap) : base(colorMap)
        {
            this.textMap = textMap.ToFixedChar();
        }

        public Foreground(params string[] textMap) : this(textMap.ToFixedChar()) { }

        private Foreground(List<FixedChar>[] textMap) : base(textMap.GetMaxSize())
        {
            this.textMap = textMap;
        }
    }
}