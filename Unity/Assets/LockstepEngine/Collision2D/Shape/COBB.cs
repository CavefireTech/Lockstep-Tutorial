using Lockstep.Math;
using Lockstep.UnsafeCollision2D;

namespace Lockstep.Collision2D {
    public class COBB : CAABB {
        public override int TypeId => (int) EShape2D.OBB;
        public FP deg;
        public FVector2 up;

        public COBB(FVector2 size, FP deg) : base(size){
            this.deg = deg;
            SetDeg(deg);
        }

        public COBB(FVector2 size, FVector2 up) : base(size){
            SetUp(up);
        }

        //CCW 旋转角度
        public void Rotate(FP rdeg){
            deg += rdeg;
            if (deg > 360 || deg < -360) {
                deg = deg - (deg / 360 * 360);
            }

            SetDeg(deg);
        }

        public void SetUp(FVector2 up){
            this.up = up;
            this.deg = FMath.Atan2(-up.x, up.y);
        }

        public void SetDeg(FP rdeg){
            deg = rdeg;
            var rad = FMath.Deg2Rad * deg;
            var c = FMath.Cos(rad);
            var s = FMath.Sin(rad);
            up = new FVector2(-s, c);
        }
        public override string ToString(){
            return $"(radius:{radius} up:{size} deg:{radius} up:{up} )";
        }
    }
}