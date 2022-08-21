using System;
using Lockstep.Math;

namespace Lockstep.Math {
    [Serializable]
    public struct FP : IEquatable<FP>, IComparable<FP> {
        public const int Precision = 1000;
        public const int HalfPrecision = Precision / 2;
        public const float PrecisionFactor = 0.001f;

        public int _val;

        public static readonly FP zero = new FP(true,0);
        public static readonly FP one = new FP(true,FP.Precision);
        public static readonly FP negOne = new FP(true,-FP.Precision);
        public static readonly FP half = new FP(true,FP.Precision / 2);
        public static readonly FP FLT_MAX = new FP(true,int.MaxValue);
        public static readonly FP FLT_MIN = new FP(true,int.MinValue);
        public static readonly FP EPSILON = new FP(true,1);
        public static readonly FP INTERVAL_EPSI_LON = new FP(true,1);

        public static readonly FP MaxValue = new FP(true,int.MaxValue);
        public static readonly FP MinValue = new FP(true, int.MinValue);
        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        /// <param name="rawVal"></param>
        public FP(bool isUseRawVal,int rawVal){
            this._val = rawVal;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        /// <param name="rawVal"></param>
        public FP(bool isUseRawVal,long rawVal){
            this._val = (int) (rawVal);
        }

        public FP(int val){
            this._val = val * FP.Precision;
        }
        public FP(long val){
            this._val = (int)(val * FP.Precision);
        }

        #if UNITY_EDITOR
        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        public FP(bool shouldOnlyUseInEditor,float val)
        {
            this._val = (int)(val * FP.Precision);
        }
        #endif

        #region override operator 

        public static bool operator <(FP a, FP b){
            return a._val < b._val;
        }

        public static bool operator >(FP a, FP b){
            return a._val > b._val;
        }

        public static bool operator <=(FP a, FP b){
            return a._val <= b._val;
        }

        public static bool operator >=(FP a, FP b){
            return a._val >= b._val;
        }

        public static bool operator ==(FP a, FP b){
            return a._val == b._val;
        }

        public static bool operator !=(FP a, FP b){
            return a._val != b._val;
        }

        public static FP operator +(FP a, FP b){
            return new FP(true,a._val + b._val);
        }

        public static FP operator -(FP a, FP b){
            return new FP(true,a._val - b._val);
        }

        public static FP operator *(FP a, FP b){
            long val = (long) (a._val) * b._val;
            return new FP(true,(int) (val / 1000));
        }

        public static FP operator /(FP a, FP b){
            long val = (long) (a._val * 1000) / b._val;
            return new FP(true,(int) (val));
        }

        public static FP operator -(FP a){
            return new FP(true,-a._val);
        }


        #region adapt for int

        public static FP operator +(FP a, int b){
            return new FP(true,a._val + b * Precision);
        }

        public static FP operator -(FP a, int b){
            return new FP(true,a._val - b * Precision);
        }

        public static FP operator *(FP a, int b){
            return new FP(true,(a._val * b));
        }

        public static FP operator /(FP a, int b){
            return new FP(true,(a._val) / b);
        }


        public static FP operator +(int a, FP b){
            return new FP(true,b._val + a * Precision);
        }

        public static FP operator -(int a, FP b){
            return new FP(true,a * Precision - b._val);
        }

        public static FP operator *(int a, FP b){
            return new FP(true,(b._val * a));
        }

        public static FP operator /(int a, FP b){
            return new FP(true,(int) ((long) (a * Precision * Precision) / b._val));
        }


        public static bool operator <(FP a, int b){
            return a._val < (b * Precision);
        }

        public static bool operator >(FP a, int b){
            return a._val > (b * Precision);
        }

        public static bool operator <=(FP a, int b){
            return a._val <= (b * Precision);
        }

        public static bool operator >=(FP a, int b){
            return a._val >= (b * Precision);
        }

        public static bool operator ==(FP a, int b){
            return a._val == (b * Precision);
        }

        public static bool operator !=(FP a, int b){
            return a._val != (b * Precision);
        }


        public static bool operator <(int a, FP b){
            return (a * Precision) < (b._val);
        }

        public static bool operator >(int a, FP b){
            return (a * Precision) > (b._val);
        }

        public static bool operator <=(int a, FP b){
            return (a * Precision) <= (b._val);
        }

        public static bool operator >=(int a, FP b){
            return (a * Precision) >= (b._val);
        }

        public static bool operator ==(int a, FP b){
            return (a * Precision) == (b._val);
        }

        public static bool operator !=(int a, FP b){
            return (a * Precision) != (b._val);
        }

        #endregion

        #endregion

        #region override object func 

        public override bool Equals(object obj){
            return obj is FP && ((FP) obj)._val == _val;
        }

        public bool Equals(FP other){
            return _val == other._val;
        }

        public int CompareTo(FP other){
            return _val.CompareTo(other._val);
        }

        public override int GetHashCode(){
            return _val;
        }

        public override string ToString(){
            return (_val * FP.PrecisionFactor).ToString();
        }

        #endregion

        #region override type convert 
        public static implicit operator FP(short value){
            return new FP(true,value * Precision);
        }

        public static explicit operator short(FP value){
            return (short)(value._val / Precision);
        }
        
        public static implicit operator FP(int value){
            return new FP(true,value * Precision);
        }

        public static implicit operator int(FP value){
            return value._val / Precision;
        }

        public static explicit operator FP(long value){
            return new FP(true,value * Precision);
        }

        public static implicit operator long(FP value){
            return value._val / Precision;
        }


        public static explicit operator FP(float value){
            return new FP(true,(long) (value * Precision));
        }

        public static explicit operator float(FP value){
            return (float) value._val * 0.001f;
        }

        public static explicit operator FP(double value){
            return new FP(true,(long) (value * Precision));
        }

        public static explicit operator double(FP value){
            return (double) value._val * 0.001;
        }

        #endregion


        public int ToInt(){
            return _val / 1000;
        }

        public long ToLong(){
            return _val / 1000;
        }

        public float ToFloat(){
            return _val * 0.001f;
        }

        public double ToDouble(){
            return _val * 0.001;
        }

        public int Floor(){
            int x = this._val;
            if (x > 0) {
                x /= FP.Precision;
            }
            else {
                if (x % FP.Precision == 0) {
                    x /= FP.Precision;
                }
                else {
                    x = x / FP.Precision - 1;
                }
            }

            return x;
        }

        public int Ceil(){
            int x = this._val;
            if (x < 0) {
                x /= FP.Precision;
            }
            else {
                if (x % FP.Precision == 0) {
                    x /= FP.Precision;
                }
                else {
                    x = x / FP.Precision + 1;
                }
            }

            return x;
        }
    }
}