using System;
using System.Runtime.InteropServices;
using Lockstep.Math;
using Lockstep.Util;

namespace Lockstep.Collision2D {
    [Serializable]
    public partial class CTransform2D : IComponent {
        public FVector2 pos;
        public FP rot; //same as Unity CW deg(up) =0

        [NoBackup]
        public FVector2 up {
            get {
                FP s, c;
                var ccwDeg = (-rot + 90);
                FMath.SinCos(out s, out c, FMath.Deg2Rad * ccwDeg);
                return new FVector2(c, s);
            }
            set => rot = ToRot(value);
        }

        public static FP ToRot(FVector2 value){
            var ccwRot = FMath.Atan2(value.y, value.x) * FMath.Rad2Deg;
            var deg = 90 - ccwRot;
            return AbsDeg(deg);
        }

        public static FP TurnToward(FVector2 targetPos, FVector2 currentPos, FP cursDeg, FP turnVal,
            out bool isLessDeg){
            var toTarget = (targetPos - currentPos).normalized;
            var toDeg = CTransform2D.ToRot(toTarget);
            return TurnToward(toDeg, cursDeg, turnVal, out isLessDeg);
        }

        public static FP TurnToward(FP toRot, FP cursRot, FP turnVal,
            out bool isLessDeg){
            var curRot = CTransform2D.AbsDeg(cursRot);
            var diff = toRot - curRot;
            var absDiff = FMath.Abs(diff);
            isLessDeg = absDiff < turnVal;
            if (isLessDeg) {
                return toRot;
            }
            else {
                if (absDiff > 180) {
                    if (diff > 0) {
                        diff -= 360;
                    }
                    else {
                        diff += 360;
                    }
                }

                return curRot + turnVal * FMath.Sign(diff);
            }
        }

        public static FP AbsDeg(FP deg){
            var rawVal = deg._val % ((FP) 360)._val;
            return new FP(true, rawVal);
        }

        public CTransform2D(){ }
        public CTransform2D(FVector2 pos) : this(pos, FP.zero){ }

        public CTransform2D(FVector2 pos, FP rot){
            this.pos = pos;
            this.rot = rot;
        }


        public void Reset(){
            pos = FVector2.zero;
            rot = FP.zero;
        }

        public FVector2 TransformPoint(FVector2 point){
            return pos + TransformDirection(point);
        }

        public FVector2 TransformVector(FVector2 vec){
            return TransformDirection(vec);
        }

        public FVector2 TransformDirection(FVector2 dir){
            var y = up;
            var x = up.RightVec();
            return dir.x * x + dir.y * y;
        }

        public static Transform2D operator +(CTransform2D a, CTransform2D b){
            return new Transform2D {pos = a.pos + b.pos, rot = a.rot + b.rot};
        }
        [NoBackup]
        public FVector3 Pos3 {
            get => new FVector3(pos.x, pos.y, FP.zero);
            set {
                pos = new FVector2(value.x, value.y);
            }
        }

        public override string ToString(){
            return $"(rot:{rot}, pos:{pos})";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public unsafe struct Transform2D {
        public FVector2 pos;
        public FP rot;
    }
}