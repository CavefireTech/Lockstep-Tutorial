using System;
using System.Collections.Generic;
using Lockstep.Math;

namespace Lockstep.PathFinding {
    [Serializable]
    public class NavMeshData {
        public FP agentRadius = FP.half;
        private static long serialVersionUID = 1L;

        /** 行走区顶点序号 */
        public int[] pathTriangles;

        /** 行走区坐标 */
        public FVector3[] pathVertices;

        /** 开始坐标 */
        public FP startX;

        public FP startZ;

        /** 结束坐标 */
        public FP endX;

        public FP endZ;

        /** navmesh地图id */
        public int mapID;

        public FP width; // 宽
        public FP height; // 高

        /**
         * 数据检测，客户端的顶点坐标和三角形数据有可能是重复的ç∂
         * TODO 小三角形合并成大三角形或多边形；判断顶点是否在寻路层中，寻路层中的顶点不能作为路径点；两点所连线段是否穿过阻挡区，不穿过，直接获取坐标点
         */
        public void check(int scale){
            amendmentSameVector(pathTriangles, pathVertices);
            scaleVector(pathVertices, scale);

            this.width = LMath.Abs(this.getEndX() - this.getStartX());
            this.height = LMath.Abs(this.getEndZ() - this.getStartZ());
        }

        /**
         * 缩放向量
         */
        protected void scaleVector(FVector3[] vertices, int scale){
            if (vertices == null || scale == 1) {
                return;
            }

            var lscale = scale.ToLFloat();
            for (int i = 0; i < vertices.Length; i++) {
                vertices[i].x += (-this.startX); // 缩放移动
                vertices[i].z += (-this.startZ);
                vertices[i] = vertices[i] * lscale;
            }
        }

        /**
         * 修正重复坐标，使坐标相同的下标修改为一致
         * <p>
         * unity的NavMeshData有一些共边的三角形，共边的三角形其实不是连通关系，共边的三角形只是他们共同构成一个凸多边形，并且这种共边的三角形，全部都是扇形排列。
         * </p>
         */
        public void amendmentSameVector(int[] indexs, FVector3[] vertices){
            if (indexs == null || vertices == null) {
                return;
            }

            Dictionary<FVector3, int> map = new Dictionary<FVector3, int>();
            // 检测路径重复点
            for (int i = 0; i < vertices.Length; i++) {
                // 重复出现的坐标
                if (map.ContainsKey(vertices[i])) {
                    for (int j = 0; j < indexs.Length; j++) {
                        if (indexs[j] == i) { // 修正重复的坐标
                            // System.out.println(String.format("坐标重复为%s",
                            // indexs[j],i,vertices[i].ToString()));
                            indexs[j] = map.get(vertices[i]);
                        }
                    }

                    // vertices[i] = null;
                }
                else {
                    map.Add(vertices[i], i);
                }
            }
        }


        public int[] GetPathTriangles(){
            return pathTriangles;
        }

        public void setPathTriangles(int[] pathTriangles){
            this.pathTriangles = pathTriangles;
        }

        public FVector3[] GetPathVertices(){
            return pathVertices;
        }

        public void setPathVertices(FVector3[] pathVertices){
            this.pathVertices = pathVertices;
        }

        public FP getStartX(){
            return startX;
        }

        public void setStartX(FP startX){
            this.startX = startX;
        }

        public FP getStartZ(){
            return startZ;
        }

        public void setStartZ(FP startZ){
            this.startZ = startZ;
        }

        public FP getEndX(){
            return endX;
        }

        public void setEndX(FP endX){
            this.endX = endX;
        }

        public FP getEndZ(){
            return endZ;
        }

        public void setEndZ(FP endZ){
            this.endZ = endZ;
        }

        public int getMapID(){
            return mapID;
        }

        public void setMapID(int mapID){
            this.mapID = mapID;
        }


        public FP getWidth(){
            return width;
        }

        public void setWidth(FP width){
            this.width = width;
        }

        public FP getHeight(){
            return height;
        }

        public void setHeight(FP height){
            this.height = height;
        }
    }
}