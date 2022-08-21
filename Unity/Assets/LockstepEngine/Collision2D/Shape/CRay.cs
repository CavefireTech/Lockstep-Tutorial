using System.Runtime.InteropServices;
using Lockstep.Math;
using Lockstep.UnsafeCollision2D;
using Lockstep.Util;

namespace Lockstep.Collision2D {
    public class CRay : CBaseShape {
        public override int TypeId => (int) EShape2D.Ray;
        public FVector2 pos;
        public FVector2 dir;
    }
        
    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public unsafe struct Ray2D  {
        public int TypeId => (int) EShape2D.Ray;
        public FVector2 origin;
        public FVector2 direction;
    }

    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public struct LRaycastHit2D {
        public FVector2 point;
        public FP distance;
        public int colliderId;
        
    }
}