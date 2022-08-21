using Lockstep.Collision2D;
using Lockstep.Math;

namespace Lockstep.Math {
    public static partial class LMathExtension {
        public static FVector2 ToLVector2(this LVector2Int vec){
            return new FVector2(true, vec.x * FP.Precision, vec.y * FP.Precision);
        }

        public static FVector3 ToLVector3(this LVector3Int vec){
            return new FVector3(true, vec.x * FP.Precision, vec.y * FP.Precision, vec.z * FP.Precision);
        }

        public static LVector2Int ToLVector2Int(this FVector2 vec){
            return new LVector2Int(vec.x.ToInt(), vec.y.ToInt());
        }

        public static LVector3Int ToLVector3Int(this FVector3 vec){
            return new LVector3Int(vec.x.ToInt(), vec.y.ToInt(), vec.z.ToInt());
        }
    }

    public static partial class LMathExtension {
        public static FP ToLFloat(this float v){
            return LMath.ToLFloat(v);
        }

        public static FP ToLFloat(this int v){
            return LMath.ToLFloat(v);
        }

        public static FP ToLFloat(this long v){
            return LMath.ToLFloat(v);
        }
    }

    public static partial class LMathExtension {
        public static LVector2Int Floor(this FVector2 vec){
            return new LVector2Int(LMath.FloorToInt(vec.x), LMath.FloorToInt(vec.y));
        }

        public static LVector3Int Floor(this FVector3 vec){
            return new LVector3Int(
                LMath.FloorToInt(vec.x),
                LMath.FloorToInt(vec.y),
                LMath.FloorToInt(vec.z)
            );
        }
    }

    public static partial class LMathExtension {
        public static FVector2 RightVec(this FVector2 vec){
            return new FVector2(true, vec._y, -vec._x);
        }

        public static FVector2 LeftVec(this FVector2 vec){
            return new FVector2(true, -vec._y, vec._x);
        }

        public static FVector2 BackVec(this FVector2 vec){
            return new FVector2(true, -vec._x, -vec._y);
        }
        public static FP ToDeg(this FVector2 vec){
            return CTransform2D.ToDeg(vec);
        }
        
        public static FP Abs(this FP val){
            return LMath.Abs(val);
        }
    }
}