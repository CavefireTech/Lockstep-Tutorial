
using System;
using System.Collections.Generic;
using Lockstep.Math;

namespace Lockstep.PathFinding {
	public abstract class NavMesh {

		/** 地图宽x轴 */
		protected FP width;

		/** 地图高y轴 */
		protected FP height;

		/** 配置id */
		protected int mapId;

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

		public int getMapId(){
			return mapId;
		}

		public void setMapId(int mapId){
			this.mapId = mapId;
		}

	}
}