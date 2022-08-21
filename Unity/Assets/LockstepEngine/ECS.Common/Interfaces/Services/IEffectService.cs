using Lockstep.Math;

namespace Lockstep.Game {
    public interface IEffectService : IService {
        void CreateEffect(int assetId, FVector2 pos);
        void DestroyEffect(object node);
    }
}