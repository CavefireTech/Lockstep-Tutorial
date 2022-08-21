using System;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    public class Plane {
        private static long serialVersionUID = -1240652082930747866L;

        public FVector3 normal = new FVector3(); // 单位长度
        public FP d = FP.zero; // 距离

        public Plane(){ }

        public Plane(FVector3 normal, FP d){
            this.normal.set(normal).nor();
            this.d = d;
        }

        public Plane(FVector3 normal, FVector3 point){
            this.normal.set(normal).nor();
            this.d = -this.normal.dot(point);
        }

        public Plane(FVector3 point1, FVector3 point2, FVector3 point3){
            set(point1, point2, point3);
        }

        public void set(FVector3 point1, FVector3 point2, FVector3 point3){
            normal = (point1).sub(point2).cross(point2.x - point3.x, point2.y - point3.y, point2.z - point3.z).nor();
            d = -point1.dot(normal);
        }



        public FP distance(FVector3 point){
            return normal.dot(point) + d;
        }

        public PlaneSide testPoint(FVector3 point){
            FP dist = normal.dot(point) + d;

            if (dist == 0)
                return PlaneSide.OnPlane;
            else if (dist < 0)
                return PlaneSide.Back;
            else
                return PlaneSide.Front;
        }


        public PlaneSide testPoint(FP x, FP y, FP z){
            FP dist = normal.dot(x, y, z) + d;

            if (dist == 0)
                return PlaneSide.OnPlane;
            else if (dist < 0)
                return PlaneSide.Back;
            else
                return PlaneSide.Front;
        }


        public bool isFrontFacing(FVector3 direction){
            FP dot = normal.dot(direction);
            return dot <= 0;
        }

        /** @return The normal */
        public FVector3 getNormal(){
            return normal;
        }

        /** @return The distance to the origin */
        public FP getD(){
            return d;
        }


        public void set(FVector3 point, FVector3 normal){
            this.normal.set(normal);
            d = -point.dot(normal);
        }

        public void set(FP pointX, FP pointY, FP pointZ, FP norX, FP norY, FP norZ){
            this.normal.set(norX, norY, norZ);
            d = -(pointX * norX + pointY * norY + pointZ * norZ);
        }


        public void set(Plane plane){
            this.normal.set(plane.normal);
            this.d = plane.d;
        }

        public override String ToString(){
            return normal.ToString() + ", " + d;
        }
    }
}