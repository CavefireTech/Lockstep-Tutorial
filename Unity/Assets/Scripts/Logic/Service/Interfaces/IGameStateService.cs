using System.Collections.Generic;
using Lockstep.Math;
using Lockstep.Game;

namespace Lockstep.Game {
    public interface IGameStateService : IService {
        //changed in the game
        FP RemainTime { get; set; }
        FP DeltaTime { get; set; }
        int MaxEnemyCount { get; set; }
        int CurEnemyCount { get; set; }
        int CurEnemyId { get; set; }
        
        
        
        Enemy[] GetEnemies();
        Player[] GetPlayers();
        Spawner[] GetSpawners();
        object GetEntity(int id);
        T CreateEntity<T>(int prefabId, FVector3 position) where T:BaseEntity, new ();
        void DestroyEntity(BaseEntity entity);
    }
}