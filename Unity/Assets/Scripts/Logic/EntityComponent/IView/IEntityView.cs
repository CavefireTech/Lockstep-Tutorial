using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Game {

    public interface IEntityView : IView {
        void OnTakeDamage(int amount, FVector3 hitPoint);
        void OnDead();
        void OnRollbackDestroy();
    }
}