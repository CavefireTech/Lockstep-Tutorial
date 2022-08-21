
namespace Lockstep.Math
{
    public static partial class LMath
    {
        public static readonly FP PIHalf = new FP(true,1571);
        public static readonly FP PI = new FP(true,3142);
        public static readonly FP PI2 = new FP(true,6283);
        public static readonly FP Rad2Deg = 180 / PI;
        public static readonly FP Deg2Rad = PI / 180;

        public static FP Pi => PI;

        public static FP Atan2(FP y, FP x)
        {
            return Atan2(y._val, x._val);
        }

        public static FP Atan2(int y, int x)
        {
            if (x == 0) {
                if (y > 0) return PIHalf;
                else if (y < 0) {return -PIHalf;}
                else return FP.zero; 
            }     
            if (y == 0) {
                if (x > 0) return FP.zero;
                else if (x < 0) {return PI;}
                else return FP.zero; 
            }

            int num;
            int num2;
            if (x < 0)
            {
                if (y < 0)
                {
                    x = -x;
                    y = -y;
                    num = 1;
                }
                else
                {
                    x = -x;
                    num = -1;
                }

                num2 = -31416;
            }
            else
            {
                if (y < 0)
                {
                    y = -y;
                    num = -1;
                }
                else
                {
                    num = 1;
                }

                num2 = 0;
            }

            int dIM = LUTAtan2.DIM;
            long num3 = (long) (dIM - 1);
            long b = (long) ((x >= y) ? x : y);
            int num4 = (int) ((long) x * num3 / b);
            int num5 = (int) ((long) y * num3 / b);
            int num6 = LUTAtan2.table[num5 * dIM + num4];
            return new FP(true,(long) ((num6 + num2) * num) / 10);
        }

        public static FP Acos(FP val)
        {
            int num = (int) (val._val * (long) LUTAcos.HALF_COUNT / FP.Precision) +
                      LUTAcos.HALF_COUNT;
            num = Clamp(num, 0, LUTAcos.COUNT);
            return new FP(true,(long) LUTAcos.table[num] / 10);
        }
        
        public static FP Asin(FP val)
        {
            int num = (int) (val._val * (long) LUTAsin.HALF_COUNT / FP.Precision) +
                      LUTAsin.HALF_COUNT;
            num = Clamp(num, 0, LUTAsin.COUNT);
            return new FP(true,(long) LUTAsin.table[num] / 10);
        }

        //ccw
        public static FP Sin(FP radians)
        {
            int index = LUTSinCos.getIndex(radians);
            return new FP(true,(long) LUTSinCos.sin_table[index] / 10);
        }

        //ccw
        public static FP Cos(FP radians)
        {
            int index = LUTSinCos.getIndex(radians);
            return new FP(true,(long) LUTSinCos.cos_table[index] / 10);
        }
        //ccw
        public static void SinCos(out FP s, out FP c, FP radians)
        {
            int index = LUTSinCos.getIndex(radians);
            s = new FP(true,(long) LUTSinCos.sin_table[index] / 10);
            c = new FP(true,(long) LUTSinCos.cos_table[index] / 10);
        }

        public static uint Sqrt32(uint a)
        {
            uint num = 0u;
            uint num2 = 0u;
            for (int i = 0; i < 16; i++)
            {
                num2 <<= 1;
                num <<= 2;
                num += a >> 30;
                a <<= 2;
                if (num2 < num)
                {
                    num2 += 1u;
                    num -= num2;
                    num2 += 1u;
                }
            }

            return num2 >> 1 & 65535u;
        }

        public static ulong Sqrt64(ulong a)
        {
            ulong num = 0uL;
            ulong num2 = 0uL;
            for (int i = 0; i < 32; i++)
            {
                num2 <<= 1;
                num <<= 2;
                num += a >> 62;
                a <<= 2;
                if (num2 < num)
                {
                    num2 += 1uL;
                    num -= num2;
                    num2 += 1uL;
                }
            }

            return num2 >> 1 & 0xffffffffu;
        }

        public static int Sqrt(int a)
        {
            if (a <= 0)
            {
                return 0;
            }

            return (int) LMath.Sqrt32((uint) a);
        }

        public static int Sqrt(long a)
        {
            if (a <= 0L)
            {
                return 0;
            }

            if (a <= (long) (0xffffffffu))
            {
                return (int) LMath.Sqrt32((uint) a);
            }

            return (int) LMath.Sqrt64((ulong) a);
        }

        public static FP Sqrt(FP a)
        {
            if (a._val <= 0)
            {
                return FP.zero;
            }

            return new FP(true,Sqrt((long) a._val * FP.Precision));
        }

        public static FP Sqr(FP a){
            return a * a;
        }

        

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static long Clamp(long a, long min, long max)
        {
            if (a < min)
            {
                return min;
            }

            if (a > max)
            {
                return max;
            }

            return a;
        }

        public static FP Clamp(FP a, FP min, FP max)
        {
            if (a < min)
            {
                return min;
            }

            if (a > max)
            {
                return max;
            }

            return a;
        }        
        public static FP Clamp01(FP a)
        {
            if (a < FP.zero)
            {
                return FP.zero;
            }

            if (a > FP.one)
            {
                return FP.one;
            }

            return a;
        }


        public static bool SameSign(FP a, FP b)
        {
            return (long) a._val * b._val > 0L;
        }

        public static int Abs(int val)
        {
            if (val < 0)
            {
                return -val;
            }

            return val;
        }

        public static long Abs(long val)
        {
            if (val < 0L)
            {
                return -val;
            }

            return val;
        }

        public static FP Abs(FP val)
        {
            if (val._val < 0)
            {
                return new FP(true,-val._val);
            }

            return val;
        }

        public static int Sign(FP val){
            return System.Math.Sign(val._val);
        }

        public static FP Round(FP val){
            if (val <= 0) {
                var remainder = (-val._val) % FP.Precision;
                if (remainder > FP.HalfPrecision) {
                    return new FP(true,val._val + remainder - FP.Precision);
                }
                else {
                    return new FP(true,val._val + remainder);
                }
            }
            else {
                var remainder = (val._val) % FP.Precision;
                if (remainder > FP.HalfPrecision) {
                    return new FP(true,val._val - remainder + FP.Precision);
                }
                else {
                    return new FP(true,val._val - remainder);
                }
            }
        }

        public static long Max(long a, long b)
        {
            return (a <= b) ? b : a;
        }

        public static int Max(int a, int b)
        {
            return (a <= b) ? b : a;
        }

        public static long Min(long a, long b)
        {
            return (a > b) ? b : a;
        }

        public static int Min(int a, int b)
        {
            return (a > b) ? b : a;
        }
        public static int Min(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
                return 0;
            int num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }
        public static FP Min(params FP[] values)
        {
            int length = values.Length;
            if (length == 0)
                return FP.zero;
            FP num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }
        public static int Max(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
                return 0;
            int num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }
        
        public static FP Max(params FP[] values)
        {
            int length = values.Length;
            if (length == 0)
                return FP.zero;
            var num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }
        
        public static int FloorToInt(FP a){
            var val = a._val;
            if (val < 0) {
                val = val - FP.Precision + 1;
            }
            return val / FP.Precision ;
        }

        public static FP ToLFloat(float a)
        {
            return  new FP(true,(int)(a * FP.Precision));
        }
        public static FP ToLFloat(int a)
        {
            return  new FP(true,(int)(a * FP.Precision));
        }
        public static FP ToLFloat(long a)
        {
            return  new FP(true,(int)(a * FP.Precision));
        }

        public static FP Min(FP a, FP b)
        {
            return new FP(true,Min(a._val, b._val));
        }

        public static FP Max(FP a, FP b)
        {
            return new FP(true,Max(a._val, b._val));
        }

        public static FP Lerp(FP a, FP b, FP f)
        {
            return new FP(true,(int) (((long) (b._val - a._val) * f._val) / FP.Precision) + a._val);
        }

        public static FP InverseLerp(FP a, FP b, FP value)
        {
            if ( a !=  b)
                return Clamp01( (( value -  a) / ( b -  a)));
            return FP.zero;
        }
        public static FVector2 Lerp(FVector2 a, FVector2 b, FP f)
        {
            return new FVector2(true,
                (int) (((long) (b._x - a._x) * f._val) / FP.Precision) + a._x,
                (int) (((long) (b._y - a._y) * f._val) / FP.Precision) + a._y);
        }

        public static FVector3 Lerp(FVector3 a, FVector3 b, FP f)
        {
            return new FVector3(true,
                (int) (((long) (b._x - a._x) * f._val) / FP.Precision) + a._x,
                (int) (((long) (b._y - a._y) * f._val) / FP.Precision) + a._y,
                (int) (((long) (b._z - a._z) * f._val) / FP.Precision) + a._z);
        }

        public static bool IsPowerOfTwo(int x)
        {
            return (x & x - 1) == 0;
        }

        public static int CeilPowerOfTwo(int x)
        {
            x--;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x++;
            return x;
        }

        public static FP Dot(FVector2 u, FVector2 v){
            return new FP(true,((long) u._x * v._x + (long) u._y * v._y) / FP.Precision);
        }

        public static FP Dot(FVector3 lhs, FVector3 rhs)
        {
            var val = ((long) lhs._x) * rhs._x + ((long) lhs._y) * rhs._y + ((long) lhs._z) * rhs._z;
            return new FP(true,val / FP.Precision);
            ;
        }
        public static FVector3 Cross(FVector3 lhs, FVector3 rhs)
        {
            return new FVector3(true,
                ((long) lhs._y * rhs._z - (long) lhs._z * rhs._y) / FP.Precision,
                ((long) lhs._z * rhs._x - (long) lhs._x * rhs._z) / FP.Precision,
                ((long) lhs._x * rhs._y - (long) lhs._y * rhs._x) / FP.Precision
            );
        }

        public static FP Cross2D(FVector2 u, FVector2 v)
        {
            return new FP(true,((long)u._x * v._y - (long)u._y * v._x) / FP.Precision);
        }
        public static FP Dot2D(FVector2 u, FVector2 v)
        {
            return new FP(true,((long) u._x * v._x + (long) u._y * v._y) / FP.Precision);
        }

    }
}