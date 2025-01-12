using Lockstep.Math;
using static Lockstep.Math.FMath;

namespace Lockstep.Math
{
   
    public struct FAxis2D
    {
        public FVector3 x;
        public FVector3 y;
        
        public static readonly FAxis2D identity = new FAxis2D(FVector3.right, FVector3.up);
        public FAxis2D(FVector3 x, FVector3 y)
        {
            this.x = x;
            this.y = y;
        }
        public FVector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new System.IndexOutOfRangeException("vector idx invalid" + index);
                }
            }

            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    default: throw new System.IndexOutOfRangeException("vector idx invalid" + index);
                }
            }
        }
    }


  
}