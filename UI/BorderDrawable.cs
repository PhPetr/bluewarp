using Nez;
using Nez.UI;
using Microsoft.Xna.Framework;

namespace bluewarp.UI
{
    public class BorderDrawable : Nez.UI.IDrawable
    {
        private Color _bgColor;
        private Color _borderColor;
        private int _borderWidth;

        public float LeftWidth { get; set; }
        public float RightWidth { get; set; }
        public float TopHeight { get; set; }
        public float BottomHeight { get; set; }
        public float MinWidth { get; set; }
        public float MinHeight { get; set; }

        public BorderDrawable(Color bgColor, Color borderColor, int borderWidth)
        {
            _bgColor = bgColor;
            _borderColor = borderColor;
            _borderWidth = borderWidth;

            LeftWidth = RightWidth = TopHeight = BottomHeight = borderWidth;
            MinHeight = MinWidth = borderWidth * 2;
        }

        public void Draw(Batcher batcher, float x, float y, float width, float height, Color color)
        {
            if (_bgColor.A > 0)
            {
                batcher.DrawRect(x, y, width, height, _bgColor *color);
            }

            if (_borderWidth > 0 && _bgColor.A > 0)
            {
                var borderColorWithAlpha = _borderColor * color;
                batcher.DrawRect(x, y, width, height, borderColorWithAlpha);
                batcher.DrawRect(x, y + height - _borderWidth, width, _borderWidth, borderColorWithAlpha);
                batcher.DrawRect(x, y, _borderWidth, height, borderColorWithAlpha);
                batcher.DrawRect(x + width - _borderWidth, y, _borderWidth, height, borderColorWithAlpha);
            }
        }
        public void SetPadding(float top, float bottom, float left, float right) { }
    }
}
