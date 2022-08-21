using System;
using Lockstep.Math;
using static Lockstep.Math.FVector3;

namespace Lockstep.Math
{
    public static partial class LMath
    {
        public static FVector3 Transform(ref FVector3 point, ref FVector3 axis_x, ref FVector3 axis_y, ref FVector3 axis_z,
            ref FVector3 trans)
        {
            return new FVector3(true,
                ((axis_x._x * point._x + axis_y._x * point._y + axis_z._x * point._z) / FP.Precision) + trans._x,
                ((axis_x._y * point._x + axis_y._y * point._y + axis_z._y * point._z) / FP.Precision) + trans._y,
                ((axis_x._z * point._x + axis_y._z * point._y + axis_z._z * point._z) / FP.Precision) + trans._z);
        }

        public static FVector3 Transform(FVector3 point, ref FVector3 axis_x, ref FVector3 axis_y, ref FVector3 axis_z,
            ref FVector3 trans)
        {
            return new FVector3(true,
                ((axis_x._x * point._x + axis_y._x * point._y + axis_z._x * point._z) / FP.Precision) + trans._x,
                ((axis_x._y * point._x + axis_y._y * point._y + axis_z._y * point._z) / FP.Precision) + trans._y,
                ((axis_x._z * point._x + axis_y._z * point._y + axis_z._z * point._z) / FP.Precision) + trans._z);
        }

        public static FVector3 Transform(ref FVector3 point, ref FVector3 axis_x, ref FVector3 axis_y, ref FVector3 axis_z,
            ref FVector3 trans, ref FVector3 scale)
        {
            long num = (long) point._x * (long) scale._x;
            long num2 = (long) point._y * (long) scale._x;
            long num3 = (long) point._z * (long) scale._x;
            return new FVector3(true,
                (int) (((long) axis_x._x * num + (long) axis_y._x * num2 + (long) axis_z._x * num3) / 1000000L) +
                trans._x,
                (int) (((long) axis_x._y * num + (long) axis_y._y * num2 + (long) axis_z._y * num3) / 1000000L) +
                trans._y,
                (int) (((long) axis_x._z * num + (long) axis_y._z * num2 + (long) axis_z._z * num3) / 1000000L) +
                trans._z);
        }

        public static FVector3 Transform(ref FVector3 point, ref FVector3 forward, ref FVector3 trans)
        {
            FVector3 up = FVector3.up;
            FVector3 vInt = Cross(FVector3.up, forward);
            return LMath.Transform(ref point, ref vInt, ref up, ref forward, ref trans);
        }

        public static FVector3 Transform(FVector3 point, FVector3 forward, FVector3 trans)
        {
            FVector3 up = FVector3.up;
            FVector3 vInt = Cross(FVector3.up, forward);
            return LMath.Transform(ref point, ref vInt, ref up, ref forward, ref trans);
        }

        public static FVector3 Transform(FVector3 point, FVector3 forward, FVector3 trans, FVector3 scale)
        {
            FVector3 up = FVector3.up;
            FVector3 vInt = Cross(FVector3.up, forward);
            return LMath.Transform(ref point, ref vInt, ref up, ref forward, ref trans, ref scale);
        }

        public static FVector3 MoveTowards(FVector3 from, FVector3 to, FP dt)
        {
            if ((to - from).sqrMagnitude <= (dt * dt))
            {
                return to;
            }

            return from + (to - from).Normalize(dt);
        }


        public static FP AngleInt(FVector3 lhs, FVector3 rhs)
        {
            return LMath.Acos(Dot(lhs, rhs));
        }

        
        
    }
}