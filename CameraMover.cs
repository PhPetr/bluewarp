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
        const int startHeightY = 200 * 32;
        const int startWidthX = 4 * 32;
        const int stopHeightY = 64;
        
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        SpriteRenderer _renderer;
        Mover _mover;
        float _moveSpeed = 100f;

        float _elapsedTimeAfterCreation = 0f;
        float _delayMoveStart = 2f;
        bool _shouldMove = false;
        bool _stopped = false;
        ITween<Vector2> _moveTween;

        public override void OnAddedToEntity()
        {
            _renderer = Entity.AddComponent(new PrototypeSpriteRenderer(32, 32));
            _mover = Entity.AddComponent(new Mover());
            Transform.Position = new Vector2(startWidthX, startHeightY);
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
            if (Transform.Position.Y < stopHeightY) return;
            
            //var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            
            var moveDir = new Vector2(0, -1);

            var movement = moveDir * _moveSpeed * Time.DeltaTime;

            _mover.CalculateMovement(ref movement, out var res);
            _subpixelV2.Update(ref movement);
            _mover.ApplyMovement(movement);
            
        }
    }
}
