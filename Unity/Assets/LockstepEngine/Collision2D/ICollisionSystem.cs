using System;
using Lockstep.Math;
using Lockstep.UnsafeCollision2D;

namespace Lockstep.Collision2D {
    public delegate void FuncCollision(ColliderProxy obj);

    public interface ICollisionSystem {
        void DoStart(bool[] interestingMasks, int[] allTypes);
        void DoUpdate(FP deltaTime);
        ColliderProxy GetCollider(int id);
        void AddCollider(ColliderProxy collider);
        void RemoveCollider(ColliderProxy collider);
        bool Raycast(int layerType, Ray2D checkRay, out FP t, out int id, FP maxDistance);
        bool Raycast(int layerType, Ray2D checkRay, out FP t, out int id);
        void QueryRegion(int layerType, FVector2 pos, FVector2 size, FVector2 forward, FuncCollision callback);
        void QueryRegion(int layerType, FVector2 pos, FP radius, FuncCollision callback);

        //for debug
        void DrawGizmos();
        int ShowTreeId { get; set; }
    }
}