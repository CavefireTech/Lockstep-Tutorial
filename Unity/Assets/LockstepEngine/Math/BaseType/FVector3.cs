using System;
#if UNITY_5_3_OR_NEWER 
using UnityEngine;
#endif

namespace Lockstep.Math
{
    [Serializable]
    public struct FVector3 : IEquatable<FVector3>
    {
        public FP x
        {
            get { return new FP(true,_x); }
            set { _x = value._val ; }
        }

        public FP y
        {
            get { return new FP(true,_y); }
            set { _y = value._val ; }
        }

        public FP z
        {
            get { return new FP(true,_z); }
            set { _z = value._val ; }
        }

        public int _x;
        public int _y;
        public int _z;


        public static readonly FVector3 zero = new FVector3(true,0, 0, 0);
        public static readonly FVector3 one = new FVector3(true,FP.Precision, FP.Precision, FP.Precision);
        public static readonly FVector3 half = new FVector3(true,FP.Precision / 2, FP.Precision / 2,FP.Precision / 2);
        
        public static readonly FVector3 forward = new FVector3(true,0, 0, FP.Precision);
        public static readonly FVector3 up = new FVector3(true,0, FP.Precision, 0);
        public static readonly FVector3 right = new FVector3(true,FP.Precision, 0, 0);
        public static readonly FVector3 back = new FVector3(true,0, 0, -FP.Precision);
        public static readonly FVector3 down = new FVector3(true,0, -FP.Precision, 0);
        public static readonly FVector3 left = new FVector3(true,-FP.Precision, 0, 0);
        
        /// <summary>
        /// 将这些值作为内部值 直接构造(高效) （仅用于内部实现，外部不建议使用）
        /// </summary>
        public FVector3(bool isUseRawVal,int _x, int _y, int _z)
        {
            this._x = _x;
            this._y = _y;
            this._z = _z;
        }
        /// <summary>
        /// 将这些值作为内部值 直接构造(高效) （仅用于内部实现，外部不建议使用）
        /// </summary>
        public FVector3(bool isUseRawVal,long _x, long _y, long _z)
        {
            this._x = (int) _x;
            this._y = (int) _y;
            this._z = (int) _z;
        }

        public FVector3(int _x, int _y, int _z)
        {
            this._x = _x * FP.Precision;
            this._y = _y * FP.Precision;
            this._z = _z * FP.Precision;
        }
        public FVector3(FP x, FP y, FP z)
        {
            this._x = x._val;
            this._y = y._val;
            this._z = z._val;
        }
        #if UNITY_EDITOR
        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        public FVector3(bool shouldOnlyUseInEditor,float x, float y, float z)
        {
            this._x = (int)(x * FP.Precision);
            this._y = (int)(y * FP.Precision);
            this._z = (int)(z * FP.Precision);
        }
        #endif

        public FP magnitude
        {
            get
            {
                long x = (long) this._x;
                long y = (long) this._y;
                long z = (long) this._z;
                return new FP(true,LMath.Sqrt(x * x + y * y + z * z));
            }
        }


        public FP sqrMagnitude
        {
            get
            {
                long x = (long) this._x;
                long y = (long) this._y;
                long z = (long) this._z;
                return new FP(true,(x * x + y * y + z * z) / FP.Precision);
            }
        }

        public FVector3 abs
        {
            get { return new FVector3(true,LMath.Abs(this._x), LMath.Abs(this._y), LMath.Abs(this._z)); }
        }

        public FVector3 Normalize()
        {
            return Normalize((FP) 1);
        }

        public FVector3 Normalize(FP newMagn)
        {
            long num = (long) (this._x * 100);
            long num2 = (long) (this._y * 100);
            long num3 = (long) (this._z * 100);
            long num4 = num * num + num2 * num2 + num3 * num3;
            if (num4 == 0L)
            {
                return this;
            }

            long b = (long) LMath.Sqrt(num4);
            long num5 = newMagn._val;
            this._x = (int) (num * num5 / b);
            this._y = (int) (num2 * num5 / b);
            this._z = (int) (num3 * num5 / b);
            return this;
        }

        public FVector3 normalized
        {
            get
            {
                long num = (long) ((long) this._x << 7);
                long num2 = (long) ((long) this._y << 7);
                long num3 = (long) ((long) this._z << 7);
                long num4 = num * num + num2 * num2 + num3 * num3;
                if (num4 == 0L)
                {
                    return FVector3.zero;
                }

                var ret = new FVector3();
                long b = (long) LMath.Sqrt(num4);
                long num5 = FP.Precision;
                ret._x = (int) (num * num5 / b);
                ret._y = (int) (num2 * num5 / b);
                ret._z = (int) (num3 * num5 / b);
                return ret;
            }
        }

        public FVector3 RotateY(FP degree)
        {
            FP s;
            FP c;
            LMath.SinCos(out s, out c, new FP(true,degree._val * 31416L / 1800000L));
            FVector3 vInt;
            vInt._x = (int) (((long) this._x * s._val + (long) this._z * c._val) / FP.Precision);
            vInt._z = (int) (((long) this._x * -c._val + (long) this._z * s._val) / FP.Precision);
            vInt._y = 0;
            return vInt.normalized;
        }


        public static bool operator ==(FVector3 lhs, FVector3 rhs)
        {
            return lhs._x == rhs._x && lhs._y == rhs._y && lhs._z == rhs._z;
        }

        public static bool operator !=(FVector3 lhs, FVector3 rhs)
        {
            return lhs._x != rhs._x || lhs._y != rhs._y || lhs._z != rhs._z;
        }

        public static FVector3 operator -(FVector3 lhs, FVector3 rhs)
        {
            lhs._x -= rhs._x;
            lhs._y -= rhs._y;
            lhs._z -= rhs._z;
            return lhs;
        }

        public static FVector3 operator -(FVector3 lhs)
        {
            lhs._x = -lhs._x;
            lhs._y = -lhs._y;
            lhs._z = -lhs._z;
            return lhs;
        }

        public static FVector3 operator +(FVector3 lhs, FVector3 rhs)
        {
            lhs._x += rhs._x;
            lhs._y += rhs._y;
            lhs._z += rhs._z;
            return lhs;
        }

        public static FVector3 operator *(FVector3 lhs, FVector3 rhs)
        {
            lhs._x = (int) (((long) (lhs._x * rhs._x)) / FP.Precision);
            lhs._y = (int) (((long) (lhs._y * rhs._y)) / FP.Precision);
            lhs._z = (int) (((long) (lhs._z * rhs._z)) / FP.Precision);
            return lhs;
        }

        public static FVector3 operator *(FVector3 lhs, FP rhs)
        {
            lhs._x = (int) (((long) (lhs._x * rhs._val)) / FP.Precision);
            lhs._y = (int) (((long) (lhs._y * rhs._val)) / FP.Precision);
            lhs._z = (int) (((long) (lhs._z * rhs._val)) / FP.Precision);
            return lhs;
        }

        public static FVector3 operator /(FVector3 lhs, FP rhs)
        {
            lhs._x = (int) (((long) lhs._x * FP.Precision) / rhs._val);
            lhs._y = (int) (((long) lhs._y * FP.Precision) / rhs._val);
            lhs._z = (int) (((long) lhs._z * FP.Precision) / rhs._val);
            return lhs;
        }
        
        public static FVector3 operator *(FP rhs,FVector3 lhs)
        {
            lhs._x = (int) (((long) (lhs._x * rhs._val)) / FP.Precision);
            lhs._y = (int) (((long) (lhs._y * rhs._val)) / FP.Precision);
            lhs._z = (int) (((long) (lhs._z * rhs._val)) / FP.Precision);
            return lhs;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", _x * FP.PrecisionFactor, _y * FP.PrecisionFactor,
                _z * FP.PrecisionFactor);
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }

            FVector3 other = (FVector3) o;
            return this._x == other._x && this._y == other._y && this._z == other._z;
        }


        public bool Equals(FVector3 other)
        {
            return this._x == other._x && this._y == other._y && this._z == other._z;
        }


        public override int GetHashCode()
        {
            return this._x * 73856093 ^ this._y * 19349663 ^ this._z * 83492791;
        }

        
        public FP this[int index]

        {

            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new IndexOutOfRangeException("vector idx invalid" + index);
                }
            }

            set
            {
                switch (index)
                {
                    case 0: _x = value._val; break;
                    case 1: _y = value._val;break;
                    case 2: _z = value._val;break;
                    default: throw new IndexOutOfRangeException("vector idx invalid" + index);
                }
            }

        }

        public static FP Dot(ref FVector3 lhs, ref FVector3 rhs)
        {
            var val = ((long) lhs._x) * rhs._x + ((long) lhs._y) * rhs._y + ((long) lhs._z) * rhs._z;
            return new FP(true,val / FP.Precision);
        }

        public static FP Dot(FVector3 lhs, FVector3 rhs)
        {
            var val = ((long) lhs._x) * rhs._x + ((long) lhs._y) * rhs._y + ((long) lhs._z) * rhs._z;
            return new FP(true,val / FP.Precision);
            ;
        }
        
        public static FVector3 Cross(ref FVector3 lhs, ref FVector3 rhs)
        {
            return new FVector3(true,
                ((long) lhs._y * rhs._z - (long) lhs._z * rhs._y) / FP.Precision,
                ((long) lhs._z * rhs._x - (long) lhs._x * rhs._z) / FP.Precision,
                ((long) lhs._x * rhs._y - (long) lhs._y * rhs._x) / FP.Precision
            );
        }

        public static FVector3 Cross(FVector3 lhs, FVector3 rhs)
        {
            return new FVector3(true,
                ((long) lhs._y * rhs._z - (long) lhs._z * rhs._y) / FP.Precision,
                ((long) lhs._z * rhs._x - (long) lhs._x * rhs._z) / FP.Precision,
                ((long) lhs._x * rhs._y - (long) lhs._y * rhs._x) / FP.Precision
            );
        }
        
        
        public static FVector3 Lerp(FVector3 a, FVector3 b, FP f)
        {
            return new FVector3(true,
                (int) (((long) (b._x - a._x) * f._val) / FP.Precision) + a._x,
                (int) (((long) (b._y - a._y) * f._val) / FP.Precision) + a._y,
                (int) (((long) (b._z - a._z) * f._val) / FP.Precision) + a._z);
        }
    }
}