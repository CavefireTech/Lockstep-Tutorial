using System;
using System.Collections.Generic;
using Lockstep.Math;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    public class Triangle {
        /** 三角形序号 */
        public int index;
        public FVector3 a;
        public FVector3 b;
        public FVector3 c;

        public FP y; //三角形高度，三个顶点的平均高度

        /** 中点 */
        public FVector3 center;

        /** 三角形和其他三角形的共享边 */
        public List<Connection<Triangle>> connections;

        /**三角形顶点序号*/
        public int[] vectorIndex;

        public Triangle(FVector3 a, FVector3 b, FVector3 c, int index, params int[] vectorIndex){
            this.a = a;
            this.b = b;
            this.c = c;
            this.y = (a.y + b.y + c.y) / 3;
            this.index = index;
            this.center = a.Add(b).Add(c).scl(1/3.ToLFloat());
            this.connections = new List<Connection<Triangle>>();
            this.vectorIndex = vectorIndex;
        }

        public override String ToString(){
            return "Triangle [index=" + index + ", a=" + a + ", b=" + b + ", c=" + c + ", center=" + center + "]";
        }

        public int getIndex(){
            return this.index;
        }

        public List<Connection<Triangle>> getConnections(){
            return connections;
        }

    

        public bool IsInnerPoint(FVector3 point){
            bool res = pointInLineLeft(a, b, point);
            if (res != pointInLineLeft(b, c, point)) {
                return false;
            }

            if (res != pointInLineLeft(c, a, point)) {
                return false;
            }

            if (cross2D(a, b, c) == 0) { //三点共线
                return false;
            }

            return true;
        }

        public static FP cross2D(FVector3 fromPoint, FVector3 toPoint, FVector3 p){
            return (toPoint.x - fromPoint.x) * (p.z - fromPoint.z) - (toPoint.z - fromPoint.z) * (p.x - fromPoint.x);
        }

        public static bool pointInLineLeft(FVector3 fromPoint, FVector3 toPoint, FVector3 p){
            return cross2D(fromPoint, toPoint, p) > 0;
        }


        public override int GetHashCode(){
            int prime = 31;
            int result = 1;
            result = prime * result + index;
            return result;
        }

        public override bool Equals(object obj){
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            Triangle other = (Triangle) obj;
            if (index != other.index)
                return false;
            return true;
        }
    }
}