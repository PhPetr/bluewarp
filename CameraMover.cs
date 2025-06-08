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
        float _moveSpeed;

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

            var movement = new Vector2(0, -1 * _moveSpeed * Time.DeltaTime);

            _subpixelV2.Update(ref movement);
            Transform.Position += movement;
            
            // pixel perfect movement
            Transform.Position = new Vector2(
                Mathf.Round(Transform.Position.X),
                Mathf.Round(Transform.Position.Y));
        }
    }
}
