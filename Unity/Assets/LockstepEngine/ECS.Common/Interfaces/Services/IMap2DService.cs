using Lockstep.Math;

namespace Lockstep.Game {
    public interface IMap2DService :IService {
        void LoadMap(int mapId);
        ushort Pos2TileId(FVector2Int pos, bool isCollider);
        void ReplaceTile(FVector2Int pos, ushort srcId, ushort dstId);
    }
}