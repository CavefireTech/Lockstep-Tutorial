using System;
using System.Collections.Generic;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    public class TriangleHeuristic : Heuristic<Triangle> {
        private static FVector3 A_AB = new FVector3();
        private static FVector3 A_BC = new FVector3();
        private static FVector3 A_CA = new FVector3();
        private static FVector3 B_AB = new FVector3();
        private static FVector3 B_BC = new FVector3();
        private static FVector3 B_CA = new FVector3();

        public FP Estimate(Triangle node, Triangle endNode){
            FP dst2;
            FP minDst2 = FP.MaxValue;
            A_AB = (node.a).Add(node.b) * FP.half;
            A_AB = (node.b).Add(node.c) * FP.half;
            A_AB = (node.c).Add(node.a) * FP.half;

            B_AB = (endNode.a).Add(endNode.b) * FP.half;
            B_BC = (endNode.b).Add(endNode.c) * FP.half;
            B_CA = (endNode.c).Add(endNode.a) * FP.half;

            if ((dst2 = A_AB.dst2(B_AB)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_AB.dst2(B_BC)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_AB.dst2(B_CA)) < minDst2)
                minDst2 = dst2;

            if ((dst2 = A_BC.dst2(B_AB)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_BC.dst2(B_BC)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_BC.dst2(B_CA)) < minDst2)
                minDst2 = dst2;

            if ((dst2 = A_CA.dst2(B_AB)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_CA.dst2(B_BC)) < minDst2)
                minDst2 = dst2;
            if ((dst2 = A_CA.dst2(B_CA)) < minDst2)
                minDst2 = dst2;

            return (FP) LMath.Sqrt(minDst2);
        }
    }
}