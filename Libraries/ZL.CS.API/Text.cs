using System.Collections.Generic;

using System.Drawing;

namespace ZL.CS.API
{
    public sealed class Text : Foreground
    {
        public Text(params string[] strings) : this(strings.ToCharCellMap()) { }

        public Text(byte[] colorMap, params string[] strings) : this(colorMap, strings.ToCharCellMap()) { }

        private Text(List<CharCell>[] charCellMap) : this(null, charCellMap) { }

        private Text(byte[]? colorMap, List<CharCell>[] charCellMap) : base(charCellMap.GetTotalLength(), new Size(), colorMap?.ToMap(), charCellMap) { }
    }
}