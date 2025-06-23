using Nez;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace bluewarp
{
    public class FighterShip : BaseProjectileShooter
    {
        enum ShipState
        {
            Base,
            PoweredUp
        }
        ShipState _shipState = ShipState.Base;

        string animation = "Base";
        SpriteAnimator _animator;
        Mover _mover;
        SubpixelVector2 _subpixelV2 = new SubpixelVector2();
        float _moveSpeed;
        float _upwardsSpeed;

        VirtualButton _fireInput;
        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;

        float _elapsedTimeAfterCreation = 0f;
        bool _shouldMove = false;

        public FighterShip(float moveSpeed, float upwardsSpeed)
        {
            _moveSpeed = moveSpeed;
            _upwardsSpeed = upwardsSpeed;

            ProjectileSpeed = new Vector2(GameConstants.Player.ProjectileSpeed);
            ProjectileSpawnOffset = GameConstants.Player.ProjectileSpawnOffset;
            ProjectileDirection = new Vector2(0, -1);
            ProjectileDelay = GameConstants.Player.ProjectileDelay;
        }

        public override void OnAddedToEntity()
        {
            _mover = Entity.AddComponent(new Mover());
            _animator = Entity.AddComponent<SpriteAnimator>();

            base.OnAddedToEntity();

            setupInput();
            randomSkinChooser();
        }

        protected override void SetupVisuals()
        {
            var shipTexture = Entity.Scene.Content.LoadTexture(Nez.Content.PlayerShip.playership);
            var sprites = Sprite.SpritesFromAtlas(shipTexture, GameConstants.TileSize, GameConstants.TileSize);

            var explosionTexture = Entity.Scene.Content.LoadTexture(Nez.Content.BasicEnemy.explosion);
            var explosion = Sprite.SpritesFromAtlas(explosionTexture, GameConstants.TileSize, GameConstants.TileSize);
            _explosionAnimator = _animator;
            _animator.AddAnimation("Explosion", explosion.ToArray());

            _animator.AddAnimation("PoweredUp", new[]
            {
                sprites[0], sprites[1], sprites[2]
            });
            _animator.AddAnimation("Base", new[]
            {
                sprites[3], sprites[4], sprites[5]
            });
            _animator.RenderLayer = RenderLayer.PlayerAnimator;
        }

        protected override void HandleFiring()
        {
            var battleScene = Entity.Scene as RunGameScene;
            battleScene.CreateProjectiles(
                new Vector2(Transform.Position.X, Transform.Position.Y + ProjectileSpawnOffset),
                ProjectileSpeed * ProjectileDirection,
                CollideWithLayer.PlayerProjectile,
                PhysicsLayer.PlayerProjectile,
                Nez.Content.PlayerShip.player_main_projectile);
            GameSFXManager.PlaySFX(GameConstants.SFX.Blaster, GameConstants.SFX.BlasterVolume);
        }

        public override void OnRemovedFromEntity()
        {
            // deregister virtual input
            _fireInput.Deregister();
            _xAxisInput.Deregister();
            _yAxisInput.Deregister();
        }

        void randomSkinChooser()
        {
            var randomInt = Nez.Random.Range(1, GameConstants.Player.SkinChance);
            if (randomInt == 1)
            {
                _shipState = ShipState.PoweredUp;
            }
            if (_shipState == ShipState.PoweredUp)
            {
                animation = "PoweredUp";
            }
        }

        void setupInput()
        {
            // setup input for shooting a projectile. we will allow z on the keyboard or a on the gamepad
            _fireInput = new VirtualButton();
            _fireInput.Nodes.Add(new VirtualButton.KeyboardKey(Keys.Space));
            _fireInput.Nodes.Add(new VirtualButton.GamePadButton(0, Buttons.A));

            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            _xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            _yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Up, Keys.Down));
            _yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.W, Keys.S));
        }

        public override void Update()
        {
            if (_isDying) return;
            
            if (!_animator.IsAnimationActive(animation))
                _animator.Play(animation);

            if (!_shouldMove)
            {
                _elapsedTimeAfterCreation += Time.DeltaTime;
                if (_elapsedTimeAfterCreation >= GameConstants.MoveStartDelay)
                {
                    _shouldMove = true;
                }
                return;
            }
            
            var moveDir = new Vector2(_xAxisInput.Value, _yAxisInput.Value);
            var upwardMovement = new Vector2(0, -1 * _upwardsSpeed * Time.DeltaTime);
            if (Transform.Position.Y <= GameConstants.Player.StopHeightY) 
                upwardMovement = new Vector2(0, 0);

            var inputMovement = moveDir * _moveSpeed * Time.DeltaTime;
            var totalMovement = inputMovement + upwardMovement;

            _mover.CalculateMovement(ref totalMovement, out var res);
            _subpixelV2.Update(ref totalMovement);
            _mover.ApplyMovement(totalMovement);
            
            if (_fireInput.IsDown && CanFireProjectile())
            {
                _lastProjectileTime = Time.TotalTime;
                HandleFiring();
            }
        }

        public override void PlayExplosionAndDestroy()
        {
            _shouldMove = false;
            base.PlayExplosionAndDestroy();
        }

        public override void OnTriggerExit(Collider other, Collider self)
        {
            //nothing
        }
    }
}
