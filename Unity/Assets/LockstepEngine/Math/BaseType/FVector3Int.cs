// Decompiled with JetBrains decompiler
// Type: UnityEngine.Vector3Int
// Assembly: UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A5F7498-719A-42F7-B77A-5934DC79A5E9
// Assembly location: /Users/jiepengtan/Projects/LockstepDemo/Common/Libs/UnityEngine.dll

using System;
using Lockstep.Math;

namespace Lockstep.Math {
    /// <summary>
    ///   <para>Representation of 3D vectors and points using integers.</para>
    /// </summary>
    public struct FVector3Int : IEquatable<FVector3Int> {
        public class Mathf {
            /// <summary>
            ///   <para>Returns the smallest of two or more values.</para>
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static int Min(int a, int b){
                return a >= b ? b : a;
            }

            /// <summary>
            ///   <para>Returns the largest of two or more values.</para>
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <param name="values"></param>
            public static int Max(int a, int b){
                return a <= b ? b : a;
            }  
            public static FP Sqrt(FP val){
                return Lockstep.Math.FMath.Sqrt(val);
            }
        }

        private static readonly FVector3Int s_Zero = new FVector3Int(0, 0, 0);
        private static readonly FVector3Int s_One = new FVector3Int(1, 1, 1);
        private static readonly FVector3Int s_Up = new FVector3Int(0, 1, 0);
        private static readonly FVector3Int s_Down = new FVector3Int(0, -1, 0);
        private static readonly FVector3Int s_Left = new FVector3Int(-1, 0, 0);
        private static readonly FVector3Int s_Right = new FVector3Int(1, 0, 0);
        private int m_X;
        private int m_Y;
        private int m_Z;

        public FVector3Int(int x, int y, int z){
            this.m_X = x;
            this.m_Y = y;
            this.m_Z = z;
        }

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public int x {
            get { return this.m_X; }
            set { this.m_X = value; }
        }

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public int y {
            get { return this.m_Y; }
            set { this.m_Y = value; }
        }

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public int z {
            get { return this.m_Z; }
            set { this.m_Z = value; }
        }

        /// <summary>
        ///   <para>Set x, y and z components of an existing Vector3Int.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Set(int x, int y, int z){
            this.m_X = x;
            this.m_Y = y;
            this.m_Z = z;
        }

        public int this[int index] {
            get {
                switch (index) {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector3Int index addressed: {0}!",
                            (object) index));
                }
            }
            set {
                switch (index) {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException(
                            string.Format("Invalid Vector3Int index addressed: {0}!", (object) index));
                }
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public FP magnitude {
            get { return Mathf.Sqrt(new FP(this.x * this.x + this.y * this.y + this.z * this.z)); }
        }
     
        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public int sqrMagnitude {
            get { return this.x * this.x + this.y * this.y + this.z * this.z; }
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static FP Distance(FVector3Int a, FVector3Int b){
            return (a - b).magnitude;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static FVector3Int Min(FVector3Int lhs, FVector3Int rhs){
            return new FVector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static FVector3Int Max(FVector3Int lhs, FVector3Int rhs){
            return new FVector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static FVector3Int Scale(FVector3Int a, FVector3Int b){
            return new FVector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(FVector3Int scale){
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        /// <summary>
        ///   <para>Clamps the Vector3Int to the bounds given by min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Clamp(FVector3Int min, FVector3Int max){
            this.x = FMath.Max(min.x, this.x);
            this.x = FMath.Min(max.x, this.x);
            this.y = FMath.Max(min.y, this.y);
            this.y = FMath.Min(max.y, this.y);
            this.z = FMath.Max(min.z, this.z);
            this.z = FMath.Min(max.z, this.z);
        }

        public static explicit operator FVector2Int(FVector3Int v){
            return new FVector2Int(v.x, v.y);
        }


        public static FVector3Int operator +(FVector3Int a, FVector3Int b){
            return new FVector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static FVector3Int operator -(FVector3Int a, FVector3Int b){
            return new FVector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static FVector3Int operator *(FVector3Int a, FVector3Int b){
            return new FVector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static FVector3Int operator *(FVector3Int a, int b){
            return new FVector3Int(a.x * b, a.y * b, a.z * b);
        }

        public static bool operator ==(FVector3Int lhs, FVector3Int rhs){
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(FVector3Int lhs, FVector3Int rhs){
            return !(lhs == rhs);
        }

        /// <summary>
        ///   <para>Returns true if the objects are equal.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other){
            if (!(other is FVector3Int))
                return false;
            return this.Equals((FVector3Int) other);
        }

        public bool Equals(FVector3Int other){
            return this == other;
        }

        /// <summary>
        ///   <para>Gets the hash code for the Vector3Int.</para>
        /// </summary>
        /// <returns>
        ///   <para>The hash code of the Vector3Int.</para>
        /// </returns>
        public override int GetHashCode(){
            int hashCode1 = this.y.GetHashCode();
            int hashCode2 = this.z.GetHashCode();
            return this.x.GetHashCode() ^ hashCode1 << 4 ^ hashCode1 >> 28 ^ hashCode2 >> 4 ^ hashCode2 << 28;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString(){
            return string.Format("({0}, {1}, {2})", (object) this.x, (object) this.y, (object) this.z);
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public string ToString(string format){
            return string.Format("({0}, {1}, {2})", (object) this.x.ToString(format),
                (object) this.y.ToString(format), (object) this.z.ToString(format));
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (0, 0, 0).</para>
        /// </summary>
        public static FVector3Int zero {
            get { return FVector3Int.s_Zero; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (1, 1, 1).</para>
        /// </summary>
        public static FVector3Int one {
            get { return FVector3Int.s_One; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (0, 1, 0).</para>
        /// </summary>
        public static FVector3Int up {
            get { return FVector3Int.s_Up; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (0, -1, 0).</para>
        /// </summary>
        public static FVector3Int down {
            get { return FVector3Int.s_Down; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (-1, 0, 0).</para>
        /// </summary>
        public static FVector3Int left {
            get { return FVector3Int.s_Left; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector3Int (1, 0, 0).</para>
        /// </summary>
        public static FVector3Int right {
            get { return FVector3Int.s_Right; }
        }
    }
}