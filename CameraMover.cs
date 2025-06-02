using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using Nez.Tweens;
using Nez.Systems;
using System.Collections;

namespace bluewarp
{
    public class CameraMover : Component, IUpdatable
    {
        int _startHeightY = 200 * 32;
        int _startWidthX = 4 * 32;
        int _stopHeightY = 64;
        
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        //SpriteRenderer _renderer;
        Mover _mover;
        float _moveSpeed = 1000f;

        float _elapsedTimeAfterCreation = 0f;
        float _delayMoveStart = 2f;
        bool _shouldMove = false;
        bool _stopped = false;

        public CameraMover(int startHeightY, int startWidthX, float moveSpeed)
        {
            _startHeightY = startHeightY;
            _startWidthX = startWidthX;
            _moveSpeed = moveSpeed;
        }

        public override void OnAddedToEntity()
        {
            //_renderer = Entity.AddComponent(new PrototypeSpriteRenderer(32, 32));
            _mover = Entity.AddComponent(new Mover());
            Transform.Position = new Vector2(_startWidthX, _startHeightY);
        }

        public void StopCamera()
        {
            _stopped = true;
        }

        void IUpdatable.Update()
        {
            if (_stopped) return;
            
            if (!_shouldMove)
            {
                _elapsedTimeAfterCreation += Time.DeltaTime;
                if (_elapsedTimeAfterCreation >= _delayMoveStart)
                {
                    _shouldMove = true;
                }
                return;
            }
            if (Transform.Position.Y < _stopHeightY) return;
            
            //var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            
            var moveDir = new Vector2(0, -1);

            var movement = moveDir * _moveSpeed * Time.DeltaTime;

            _mover.CalculateMovement(ref movement, out var res);
            _subpixelV2.Update(ref movement);
            _mover.ApplyMovement(movement);
            
        }
    }
}
