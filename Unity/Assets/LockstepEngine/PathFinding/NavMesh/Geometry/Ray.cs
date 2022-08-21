using System;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    public class Ray {
        private static long serialVersionUID = -620692054835390878L;
        public FVector3 origin = new FVector3(); // 
        public FVector3 direction = new FVector3(); // 

        public Ray(){ }

        public Ray(FVector3 origin, FVector3 direction){
            this.origin.set(origin);
            this.direction.set(direction).nor();
        }

        /** @return a copy of this ray. */
        public Ray cpy(){
            return new Ray(this.origin, this.direction);
        }


        public FVector3 getEndPoint(FVector3 _out, FP distance){
            return _out.set(direction).scl(distance).Add(origin);
        }

        static FVector3 tmp = new FVector3();


        /** {@inheritDoc} */
        public String toString(){
            return "ray [" + origin + ":" + direction + "]";
        }


        public Ray set(FVector3 origin, FVector3 direction){
            this.origin.set(origin);
            this.direction.set(direction);
            return this;
        }

        public Ray set(FP x, FP y, FP z, FP dx, FP dy, FP dz){
            this.origin.set(x, y, z);
            this.direction.set(dx, dy, dz);
            return this;
        }


        public Ray set(Ray ray){
            this.origin.set(ray.origin);
            this.direction.set(ray.direction);
            return this;
        }
    }
}