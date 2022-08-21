using Lockstep.Math;

namespace Lockstep.Game {
    public interface IRandomService : IService {
        FP value { get; }
        uint Next();
        uint Next(uint max);
        int Next(int max);
        uint Range(uint min, uint max);
        int Range(int min, int max);
        FP Range(FP min, FP max);
    }
}