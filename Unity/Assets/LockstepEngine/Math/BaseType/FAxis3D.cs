using Lockstep.Math;
using static Lockstep.Math.FVector3;

namespace Lockstep.Math
{
    public struct FAxis3D
    {
        public FVector3 x;
        public FVector3 y;
        public FVector3 z;
        public static readonly FAxis3D identity = new FAxis3D(FVector3.right, FVector3.up, FVector3.forward);

        public FAxis3D(FVector3 right, FVector3 up, FVector3 forward)
        {
            this.x = right;
            this.y = up;
            this.z = forward;
        }

        public FVector3 WorldToLocal(FVector3 vec)
        {
            var _x = Dot(x, vec);
            var _y = Dot(y, vec);
            var _z = Dot(z, vec);
            return new FVector3(_x, _y, _z);
        }
        public FVector3 LocalToWorld(FVector3 vec)
        {
            return x * vec.x + y * vec.y + z * vec.z;
        }

        public FVector3 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
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
                    case 2:
                        z = value;
                        break;
                    default: throw new System.IndexOutOfRangeException("vector idx invalid" + index);
                }
            }
        }
    }
}