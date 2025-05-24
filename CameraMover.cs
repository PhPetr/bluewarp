using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace bluewarp
{
    public class CameraMover : Component, IUpdatable
    {
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        SpriteRenderer _renderer;
        Mover _mover;
        float _moveSpeed = 200f;
        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;
        const int startHeightY = 200 * 32;

        public override void OnAddedToEntity()
        {
            _renderer = Entity.AddComponent(new PrototypeSpriteRenderer(32, 32));
            _mover = Entity.AddComponent(new Mover());
            _mover.ApplyMovement(new Vector2(0, startHeightY));
            SetupInput();
        }

        void SetupInput()
        { 
            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
        }

        void IUpdatable.Update()
        {
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            var movement = moveDir * _moveSpeed * Time.DeltaTime;

            _mover.CalculateMovement(ref movement, out var res);
            _subpixelV2.Update(ref movement);
            _mover.ApplyMovement(movement);
        }
    }
}
