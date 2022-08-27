using Lockstep.Collision2D;
using Lockstep.Math;

namespace Lockstep.Math {
    public static partial class FMathExtension {
        public static FVector2 ToFVector2(this FVector2Int vec){
            return new FVector2(true, vec.x * FP.Precision, vec.y * FP.Precision);
        }

        public static FVector3 ToFVector3(this FVector3Int vec){
            return new FVector3(true, vec.x * FP.Precision, vec.y * FP.Precision, vec.z * FP.Precision);
        }

        public static FVector2Int ToLVector2Int(this FVector2 vec){
            return new FVector2Int(vec.x.ToInt(), vec.y.ToInt());
        }

        public static FVector3Int ToFVector3Int(this FVector3 vec){
            return new FVector3Int(vec.x.ToInt(), vec.y.ToInt(), vec.z.ToInt());
        }
    }

    public static partial class FMathExtension {
        public static FP ToFP(this float v){
            return FMath.ToLFloat(v);
        }

        public static FP ToFP(this int v){
            return FMath.ToLFloat(v);
        }

        public static FP ToFP(this long v){
            return FMath.ToLFloat(v);
        }
    }

    public static partial class FMathExtension {
        public static FVector2Int Floor(this FVector2 vec){
            return new FVector2Int(FMath.FloorToInt(vec.x), FMath.FloorToInt(vec.y));
        }

        public static FVector3Int Floor(this FVector3 vec){
            return new FVector3Int(
                FMath.FloorToInt(vec.x),
                FMath.FloorToInt(vec.y),
                FMath.FloorToInt(vec.z)
            );
        }
    }

    public static partial class FMathExtension {
        public static FVector2 RightVec(this FVector2 vec){
            return new FVector2(true, vec._y, -vec._x);
        }

        public static FVector2 LeftVec(this FVector2 vec){
            return new FVector2(true, -vec._y, vec._x);
        }

        public static FVector2 BackVec(this FVector2 vec){
            return new FVector2(true, -vec._x, -vec._y);
        }
        public static FP ToRot(this FVector2 vec){
            return CTransform2D.ToRot(vec);
        }
        
        public static FP Abs(this FP val){
            return FMath.Abs(val);
        }
    }
}