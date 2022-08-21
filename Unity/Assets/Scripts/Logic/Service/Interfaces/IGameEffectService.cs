using Lockstep.Math;

namespace Lockstep.Game {
    public interface IGameEffectService : IService {
        void ShowDiedEffect(FVector2 pos);
        void ShowBornEffect(FVector2 pos);
    }
}