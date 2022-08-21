using System;
using Lockstep.Math;

namespace Lockstep.Game {
    [Serializable]
    public partial class SpawnerInfo : INeedBackup {
        public FP spawnTime;
        public FVector3 spawnPoint;
        public int prefabId;
    }
}