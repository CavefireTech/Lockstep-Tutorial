
using Lockstep.UnsafeCollision2D;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif
using Lockstep.Math;

namespace Lockstep.Math {
#if UNITY_5_3_OR_NEWER
    public static partial class FMathExtension {
        public static FVector2 ToLVector2(this Vector2Int vec){
            return new FVector2(true,vec.x * FP.Precision, vec.y * FP.Precision);
        }       
     
        public static FVector3 ToLVector3(this Vector3Int vec){
            return new FVector3(true,vec.x * FP.Precision, vec.y * FP.Precision, vec.z * FP.Precision);
        }
     
        public static FVector2Int ToLVector2Int(this Vector2Int vec){
            return new FVector2Int(vec.x, vec.y);
        }

        public static FVector3Int ToLVector3Int(this Vector3Int vec){
            return new FVector3Int(vec.x, vec.y, vec.z);
        }
        public static Vector2Int ToVector2Int(this FVector2Int vec){
            return new Vector2Int(vec.x, vec.y);
        }

        public static Vector3Int ToVector3Int(this FVector3Int vec){
            return new Vector3Int(vec.x, vec.y, vec.z);
        }
        public static FVector2 ToLVector2(this Vector2 vec){
            return new FVector2(
                FMath.ToLFloat(vec.x),
                FMath.ToLFloat(vec.y));
        }

        public static FVector3 ToLVector3(this Vector3 vec){
            return new FVector3(
                FMath.ToLFloat(vec.x),
                FMath.ToLFloat(vec.y),
                FMath.ToLFloat(vec.z));
        }
        public static FVector2 ToLVector2XZ(this Vector3 vec){
            return new FVector2(
                FMath.ToLFloat(vec.x),
                FMath.ToLFloat(vec.y));
        }
        public static Vector2 ToVector2(this FVector2 vec){
            return new Vector2(vec.x.ToFloat(), vec.y.ToFloat());
        }
        public static Vector3 ToVector3(this FVector2 vec){
            return new Vector3(vec.x.ToFloat(), vec.y.ToFloat(),0);
        }
        public static Vector3 ToVector3XZ(this FVector2 vec,FP z){
            return new Vector3(vec.x.ToFloat(), vec.y.ToFloat(),z);
        }
        public static Vector3 ToVector3XZ(this FVector2 vec){
            return new Vector3(vec.x.ToFloat(), vec.y.ToFloat(),0);
        }
        public static Vector3 ToVector3(this FVector3 vec){
            return new Vector3(vec.x.ToFloat(), vec.y.ToFloat(), vec.z.ToFloat());
        }
        public static Rect ToRect(this FRect vec){
            return new Rect(vec.position.ToVector2(),vec.size.ToVector2());
        }
    }
#endif
}