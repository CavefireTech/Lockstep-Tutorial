using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Game{    
    public enum EColliderLayer {
        Ground,
        Wall,
        Enemy,
        Hero,
        Bullet,
        EnumCount
    }
    public class HeroSystem : BaseSystem {
        public override void DoUpdate(FP deltaTime){
            foreach (var player in _gameStateService.GetPlayers()) {
                if (player.IsActive) {
                    player.DoUpdate(deltaTime);
                }
            }
        }
    }
}