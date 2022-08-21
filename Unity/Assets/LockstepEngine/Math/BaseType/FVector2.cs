using System;
using Lockstep.Math;

namespace Lockstep.Math {
    [Serializable]
    public struct FVector2 {
        public FP x {
            get { return new FP(true, _x); }
            set { _x = value._val; }
        }

        public FP y {
            get { return new FP(true, _y); }
            set { _y = value._val; }
        }

        public int _x;
        public int _y;
        public static readonly FVector2 zero = new FVector2(true, 0, 0);
        public static readonly FVector2 one = new FVector2(true, FP.Precision, FP.Precision);
        public static readonly FVector2 half = new FVector2(true, FP.Precision / 2, FP.Precision / 2);
        public static readonly FVector2 up = new FVector2(true, 0, FP.Precision);
        public static readonly FVector2 down = new FVector2(true, 0, -FP.Precision);
        public static readonly FVector2 right = new FVector2(true, FP.Precision, 0);
        public static readonly FVector2 left = new FVector2(true, -FP.Precision, 0);

        private static readonly int[] Rotations = new int[] {
            1,
            0,
            0,
            1,
            0,
            1,
            -1,
            0,
            -1,
            0,
            0,
            -1,
            0,
            -1,
            1,
            0
        };

        /// <summary>
        /// 顺时针旋转90Deg 参数
        /// </summary>
        public const int ROTATE_CW_90 = 1;

        public const int ROTATE_CW_180 = 2;
        public const int ROTATE_CW_270 = 3;
        public const int ROTATE_CW_360 = 4;

        public FVector2(bool isUseRawVal, int x, int y){
            this._x = x;
            this._y = y;
        }

        public FVector2(FP x, FP y){
            this._x = x._val;
            this._y = y._val;
        }

        public FVector2(int x, int y){
            this._x = x * FP.Precision;
            this._y = y * FP.Precision;
        }


        /// <summary>
        /// clockwise 顺时针旋转  
        /// 1表示顺时针旋转 90 degree
        /// 2表示顺时针旋转 180 degree
        /// </summary>
        public static FVector2 Rotate(FVector2 v, int r){
            r %= 4;
            return new FVector2(true,
                v._x * FVector2.Rotations[r * 4] + v._y * FVector2.Rotations[r * 4 + 1],
                v._x * FVector2.Rotations[r * 4 + 2] + v._y * FVector2.Rotations[r * 4 + 3]);
        }

        public FVector2 Rotate( FP deg){
            var rad = LMath.Deg2Rad * deg;
            FP cos, sin;
            LMath.SinCos(out sin, out cos, rad);
            return new FVector2(x * cos - y * sin, x * sin + y * cos);
        }

        public static FVector2 Min(FVector2 a, FVector2 b){
            return new FVector2(true, LMath.Min(a._x, b._x), LMath.Min(a._y, b._y));
        }

        public static FVector2 Max(FVector2 a, FVector2 b){
            return new FVector2(true, LMath.Max(a._x, b._x), LMath.Max(a._y, b._y));
        }

        public void Min(ref FVector2 r){
            this._x = LMath.Min(this._x, r._x);
            this._y = LMath.Min(this._y, r._y);
        }

        public void Max(ref FVector2 r){
            this._x = LMath.Max(this._x, r._x);
            this._y = LMath.Max(this._y, r._y);
        }


        public void Normalize(){
            long num = (long) (this._x * 100);
            long num2 = (long) (this._y * 100);
            long num3 = num * num + num2 * num2;
            if (num3 == 0L) {
                return;
            }

            long b = (long) LMath.Sqrt(num3);
            this._x = (int) (num * 1000L / b);
            this._y = (int) (num2 * 1000L / b);
        }

        public FP sqrMagnitude {
            get {
                long num = (long) this._x;
                long num2 = (long) this._y;
                return new FP(true, (num * num + num2 * num2) / FP.Precision);
            }
        }

        public long rawSqrMagnitude {
            get {
                long num = (long) this._x;
                long num2 = (long) this._y;
                return num * num + num2 * num2;
            }
        }

        public FP magnitude {
            get {
                long num = (long) this._x;
                long num2 = (long) this._y;
                return new FP(true, LMath.Sqrt(num * num + num2 * num2));
            }
        }

        public FVector2 normalized {
            get {
                FVector2 result = new FVector2(true, this._x, this._y);
                result.Normalize();
                return result;
            }
        }

        public static FVector2 operator +(FVector2 a, FVector2 b){
            return new FVector2(true, a._x + b._x, a._y + b._y);
        }

        public static FVector2 operator -(FVector2 a, FVector2 b){
            return new FVector2(true, a._x - b._x, a._y - b._y);
        }

        public static FVector2 operator -(FVector2 lhs){
            lhs._x = -lhs._x;
            lhs._y = -lhs._y;
            return lhs;
        }

        public static FVector2 operator *(FP rhs, FVector2 lhs){
            lhs._x = (int) (((long) (lhs._x * rhs._val)) / FP.Precision);
            lhs._y = (int) (((long) (lhs._y * rhs._val)) / FP.Precision);
            return lhs;
        }

        public static FVector2 operator *(FVector2 lhs, FP rhs){
            lhs._x = (int) (((long) (lhs._x * rhs._val)) / FP.Precision);
            lhs._y = (int) (((long) (lhs._y * rhs._val)) / FP.Precision);
            return lhs;
        }
        public static FVector2 operator *(int rhs, FVector2 lhs){
            lhs._x = lhs._x * rhs;
            lhs._y = lhs._y * rhs;
            return lhs;
        }

        public static FVector2 operator *(FVector2 lhs, int rhs){
            lhs._x = lhs._x * rhs;
            lhs._y = lhs._y * rhs;
            return lhs;
        }
        public static FVector2 operator /(FVector2 lhs, FP rhs){
            lhs._x = (int) (((long) lhs._x * FP.Precision) / rhs._val);
            lhs._y = (int) (((long) lhs._y * FP.Precision) / rhs._val);
            return lhs;
        }
        public static FVector2 operator /(FVector2 lhs, int rhs){
            lhs._x = lhs._x / rhs;
            lhs._y = lhs._y / rhs;
            return lhs;
        }
        public static bool operator ==(FVector2 a, FVector2 b){
            return a._x == b._x && a._y == b._y;
        }

        public static bool operator !=(FVector2 a, FVector2 b){
            return a._x != b._x || a._y != b._y;
        }

        public static implicit operator FVector2(FVector3 v){
            return new FVector2(true, v._x, v._y);
        }

        public static implicit operator FVector3(FVector2 v){
            return new FVector3(true, v._x, v._y, 0);
        }

        public override bool Equals(object o){
            if (o == null) {
                return false;
            }

            FVector2 vInt = (FVector2) o;
            return this._x == vInt._x && this._y == vInt._y;
        }

        public override int GetHashCode(){
            return this._x * 49157 + this._y * 98317;
        }

        public override string ToString(){
            return string.Format("({0},{1})", _x * FP.PrecisionFactor, _y * FP.PrecisionFactor);
        }

        public FVector3 ToInt3 {
            get { return new FVector3(true, _x, 0, _y); }
        }

        public FP this[int index] {
            get {
                switch (index) {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException("vector idx invalid" + index);
                }
            }

            set {
                switch (index) {
                    case 0:
                        _x = value._val;
                        break;
                    case 1:
                        _y = value._val;
                        break;
                    default: throw new IndexOutOfRangeException("vector idx invalid" + index);
                }
            }
        }


        public static FP Dot(FVector2 u, FVector2 v){
            return new FP(true, ((long) u._x * v._x + (long) u._y * v._y) / FP.Precision);
        }

        public static FP Cross(FVector2 a, FVector2 b){
            return new FP(true, ((long) a._x * (long) b._y - (long) a._y * (long) b._x) / FP.Precision);
        }

        public static FVector2 Lerp(FVector2 a, FVector2 b, FP f){
            return new FVector2(true,
                (int) (((long) (b._x - a._x) * f._val) / FP.Precision) + a._x,
                (int) (((long) (b._y - a._y) * f._val) / FP.Precision) + a._y);
        }
    }
}