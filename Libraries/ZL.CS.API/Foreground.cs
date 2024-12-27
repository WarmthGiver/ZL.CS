using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ZL.CS.API
{
    public class Foreground : Graphic
    {
        public const byte defaultColor = 007;

        public readonly List<CharCell>[] charCellMap;

        public Foreground(params string[] strings) : this(strings.ToCharCellMap()) { }

        public Foreground(byte[,] colorMap, params string[] strings) : base(colorMap)
        {
            charCellMap = strings.ToCharCellMap();
        }

        private Foreground(List<CharCell>[] charCellMap) : base(charCellMap.GetMaxSize())
        {
            this.charCellMap = charCellMap;
        }

        protected Foreground(Size size, Size pivot, byte[,]? colorMap, List<CharCell>[] charCellMap) : base(size, pivot, colorMap)
        {
            this.charCellMap = charCellMap;
        }
    }
}