using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    public static class VectorExtension {
        public static FVector3 set(this FVector3 vec, FP x, FP y, FP z){
            vec.x = x;
            vec.y = y;
            vec.z = z;
            return vec;
        }

        public static FVector3 set(this FVector3 vec, FVector3 val){
            vec = val;
            return vec;
        }

        public static FVector3 mulAdd(this FVector3 _this, FVector3 vec, FP scalar){
            _this.x += vec.x * scalar;
            _this.y += vec.y * scalar;
            _this.z += vec.z * scalar;
            return _this;
        }

        public static FVector3 Add(this FVector3 vec, FVector3 val){
            return vec + val;
        }

        public static FVector3 sub(this FVector3 vec, FVector3 val){
            return vec - val;
        }

        public static FVector3 scl(this FVector3 vec, FP val){
            return vec * val;
        }
      
        public static FP dot(this FVector3 vec, FVector3 val){
            return FVector3.Dot(vec, val);
        }
        public static FP dot(this FVector3 vec, FP x, FP y, FP z){
            return FVector3.Dot(vec, new FVector3(x, y, z));
        }


        public static FVector3 cross(this FVector3 vec, FVector3 vector){
            return new FVector3(vec.y * vector.z - vec.z * vector.y, vec.z * vector.x - vec.x * vector.z,
                vec.x * vector.y - vec.y * vector.x);
        }

        public static FVector3 cross(this FVector3 vec, FP x, FP y, FP z){
            return new FVector3(vec.y * z - vec.z * y, vec.z * x - vec.x * z, vec.x * y - vec.y * x);
        }

        public static FVector3 nor(this FVector3 vec){
            return vec.normalized;
        }

        public static FP len(this FVector3 vec){
            return vec.magnitude;
        }

        public static FP dst2(this FVector3 vec, FVector3 p){
            return dst2(vec.x, vec.z, p.x, p.z);
        }

        public static FP dst2(FP x1, FP z1, FP x2, FP z2){
            x1 -= x2;
            z1 -= z2;
            return (x1 * x1 + z1 * z1);
        }

        public static T get<T>(this List<T> lst, int idx){
            return lst[idx];
        }

        public static Tval get<Tkey, Tval>(this Dictionary<Tkey, Tval> lst, Tkey idx){
            return lst[idx];
        }
    }
}