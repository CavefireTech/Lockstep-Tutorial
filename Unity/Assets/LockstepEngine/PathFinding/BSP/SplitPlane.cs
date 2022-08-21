using Lockstep.Math;

namespace Lockstep.PathFinding {
    public struct SplitPlane {
        public SplitPlane(FVector2 a, FVector2 b){
            this.a = a;
            this.b = b;
        }

        public FVector2 a;
        public FVector2 b;
        public FVector2 dir => b - a;

        private static FP val;
        public static ESplitType GetSplitResult(SplitPlane plane, TriRef tri){
            var planeDir = plane.dir;
            var valA = FVector2.Cross(planeDir, tri.a - plane.a);
            var valB = FVector2.Cross(planeDir, tri.b - plane.a);
            var valC = FVector2.Cross(planeDir, tri.c - plane.a);

            var isRight = false;
            if (valA != 0) isRight = valA < 0;
            if (valB != 0) isRight = valB < 0;
            if (valC != 0) isRight = valC < 0;
            
            var isA = valA <= 0;
            var isB = valB <= 0;
            var isC = valC <= 0;
            if (isA == isB && isB == isC) {
                return isRight ? ESplitType.Right : ESplitType.Left;
            }

            isA = valA >= 0;
            isB = valB >= 0;
            isC = valC >= 0;
            if (isA == isB && isB == isC) {
                return isRight ? ESplitType.Right : ESplitType.Left;
            }

            return ESplitType.OnPlane;
        }

        public static ESplitType ClassifyPointToPlane(SplitPlane plane, FVector2 vertex){
            var val = FVector2.Cross(plane.dir, vertex - plane.a);
            if (val == 0)
                return ESplitType.OnPlane;
            else {
                return val < 0 ? ESplitType.Right : ESplitType.Left;
            }
        }

        public static FVector2 GetIntersectPoint(FVector2 p0, FVector2 p1, FVector2 p2, FVector2 p3){
            var diff = p2 - p0;
            var d1 = p1 - p0;
            var d2 = p3 - p2;
            var demo = FMath.Cross2D(d1, d2); //det
            if (FMath.Abs(demo) < FP.EPSILON) //parallel
                return p0;

            var t1 = FMath.Cross2D(diff, d2) / demo; // Cross2D(diff,-d2)
            return p0 + (p1 - p0) * t1;
        }
    }
}