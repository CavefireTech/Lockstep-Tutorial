using Lockstep.Math;

namespace Lockstep.PathFinding {
    public class Funnel {

        public Plane leftPlane = new Plane(); // 左平面，高度为y轴
        public Plane rightPlane = new Plane();
        public FVector3 leftPortal = new FVector3(); // 路径左顶点，
        public FVector3 rightPortal = new FVector3(); // 路径右顶点
        public FVector3 pivot = new FVector3(); // 漏斗点，路径的起点或拐点

        public void setLeftPlane(FVector3 pivot, FVector3 leftEdgeVertex){
            leftPlane.set(pivot, pivot.Add(FVector3.up), leftEdgeVertex);
            leftPortal = leftEdgeVertex;
        }

        public void setRightPlane(FVector3 pivot, FVector3 rightEdgeVertex){
            rightPlane.set(pivot, pivot.Add(FVector3.up), rightEdgeVertex); // 高度
            rightPlane.normal = -rightPlane.normal; // 平面方向取反
            rightPlane.d = -rightPlane.d;
            rightPortal = (rightEdgeVertex);
        }

        public void setPlanes(FVector3 pivot, TriangleEdge edge){
            setLeftPlane(pivot, edge.leftVertex);
            setRightPlane(pivot, edge.rightVertex);
        }

        public PlaneSide sideLeftPlane(FVector3 point){
            return leftPlane.testPoint(point);
        }

        public PlaneSide sideRightPlane(FVector3 point){
            return rightPlane.testPoint(point);
        }
    }
}