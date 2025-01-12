// Decompiled with JetBrains decompiler
// Type: UnityEngine.Vector2Int
// Assembly: UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A5F7498-719A-42F7-B77A-5934DC79A5E9

using Lockstep.Math;
using System;

namespace Lockstep.Math  {
    public struct FVector2Int : IEquatable<FVector2Int> {
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

        private static readonly FVector2Int s_Zero = new FVector2Int(0, 0);
        private static readonly FVector2Int s_One = new FVector2Int(1, 1);
        private static readonly FVector2Int s_Up = new FVector2Int(0, 1);
        private static readonly FVector2Int s_Down = new FVector2Int(0, -1);
        private static readonly FVector2Int s_Left = new FVector2Int(-1, 0);
        private static readonly FVector2Int s_Right = new FVector2Int(1, 0);
        private int m_X;
        private int m_Y;

        public FVector2Int(int x, int y){
            this.m_X = x;
            this.m_Y = y;
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
        ///   <para>Set x and y components of an existing Vector2Int.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(int x, int y){
            this.m_X = x;
            this.m_Y = y;
        }

        public int this[int index] {
            get {
                if (index == 0)
                    return this.x;
                if (index == 1)
                    return this.y;
                throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!",
                    (object) index));
            }
            set {
                if (index != 0) {
                    if (index != 1)
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!",
                            (object) index));
                    this.y = value;
                }
                else
                    this.x = value;
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (Read Only).</para>
        /// </summary>
        public FP magnitude {
            get { return Mathf.Sqrt(new FP(this.x * this.x + this.y * this.y)); }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (Read Only).</para>
        /// </summary>
        public int sqrMagnitude {
            get { return this.x * this.x + this.y * this.y; }
        }

        /// <summary>
        ///   <para>Returns the distance between a and b.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static FP Distance(FVector2Int a, FVector2Int b){
            var num1 = (a.x - b.x);
            var num2 = (a.y - b.y);
            return Mathf.Sqrt(new FP(num1 * num1 + num2 * num2));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static FVector2Int Min(FVector2Int lhs, FVector2Int rhs){
            return new FVector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static FVector2Int Max(FVector2Int lhs, FVector2Int rhs){
            return new FVector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static FVector2Int Scale(FVector2Int a, FVector2Int b){
            return new FVector2Int(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of scale.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(FVector2Int scale){
            this.x *= scale.x;
            this.y *= scale.y;
        }

        /// <summary>
        ///   <para>Clamps the Vector2Int to the bounds given by min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Clamp(FVector2Int min, FVector2Int max){
            this.x = FMath.Max(min.x, this.x);
            this.x = FMath.Min(max.x, this.x);
            this.y = FMath.Max(min.y, this.y);
            this.y = FMath.Min(max.y, this.y);
        }


        public static explicit operator FVector3Int(FVector2Int v){
            return new FVector3Int(v.x, v.y, 0);
        }


        public static FVector2Int operator +(FVector2Int a, FVector2Int b){
            return new FVector2Int(a.x + b.x, a.y + b.y);
        }

        public static FVector2Int operator -(FVector2Int a, FVector2Int b){
            return new FVector2Int(a.x - b.x, a.y - b.y);
        }

        public static FVector2Int operator *(FVector2Int a, FVector2Int b){
            return new FVector2Int(a.x * b.x, a.y * b.y);
        }

        public static FVector2Int operator *(FVector2Int a, int b){
            return new FVector2Int(a.x * b, a.y * b);
        }

        public static bool operator ==(FVector2Int lhs, FVector2Int rhs){
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(FVector2Int lhs, FVector2Int rhs){
            return !(lhs == rhs);
        }

        /// <summary>
        ///   <para>Returns true if the objects are equal.</para>
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other){
            if (!(other is FVector2Int))
                return false;
            return this.Equals((FVector2Int) other);
        }

        public bool Equals(FVector2Int other){
            return this.x.Equals(other.x) && this.y.Equals(other.y);
        }

        /// <summary>
        ///   <para>Gets the hash code for the Vector2Int.</para>
        /// </summary>
        /// <returns>
        ///   <para>The hash code of the Vector2Int.</para>
        /// </returns>
        public override int GetHashCode(){
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        public override string ToString(){
            return string.Format("({0}, {1})", (object) this.x, (object) this.y);
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, 0).</para>
        /// </summary>
        public static FVector2Int zero {
            get { return FVector2Int.s_Zero; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (1, 1).</para>
        /// </summary>
        public static FVector2Int one {
            get { return FVector2Int.s_One; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, 1).</para>
        /// </summary>
        public static FVector2Int up {
            get { return FVector2Int.s_Up; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (0, -1).</para>
        /// </summary>
        public static FVector2Int down {
            get { return FVector2Int.s_Down; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (-1, 0).</para>
        /// </summary>
        public static FVector2Int left {
            get { return FVector2Int.s_Left; }
        }

        /// <summary>
        ///   <para>Shorthand for writing Vector2Int (1, 0).</para>
        /// </summary>
        public static FVector2Int right {
            get { return FVector2Int.s_Right; }
        }
    }
}