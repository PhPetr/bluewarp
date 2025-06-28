using Microsoft.Xna.Framework;
using Nez;

namespace bluewarp.UI
{
    /// <summary>
    /// Creates Drawable bordered background.
    /// </summary>
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

        /// <summary>
        /// Create bordered background around UI element.
        /// </summary>
        /// <param name="bgColor">Background color</param>
        /// <param name="borderColor">Border color</param>
        /// <param name="borderWidth">Border width in px</param>
        public BorderDrawable(Color bgColor, Color borderColor, int borderWidth)
        {
            _bgColor = bgColor;
            _borderColor = borderColor;
            _borderWidth = borderWidth;

            LeftWidth = RightWidth = TopHeight = BottomHeight = borderWidth;
            MinHeight = MinWidth = borderWidth * 2;
        }

        /// <summary>
        /// Draws bordered background.
        /// </summary>
        /// <param name="batcher">Batcher which will draw</param>
        /// <param name="x">X coord of UI element</param>
        /// <param name="y">Y coord of UI element</param>
        /// <param name="width">Width of UI element</param>
        /// <param name="height">Height of UI element</param>
        /// <param name="color">Color of element to draw</param>
        public void Draw(Batcher batcher, float x, float y, float width, float height, Color color)
        {

            if (_borderWidth > 0 && _bgColor.A > 0)
            {
                var borderColorWithAlpha = _borderColor * color;

                batcher.DrawRect(x - 2 * _borderWidth, y - 2 * _borderWidth, width + 3 * _borderWidth, height + 3 * _borderWidth, borderColorWithAlpha);

                batcher.DrawRect(x - _borderWidth, y-_borderWidth, width + _borderWidth, height + _borderWidth, _bgColor *color);
            }
        }
        public void SetPadding(float top, float bottom, float left, float right) { }
    }
}
