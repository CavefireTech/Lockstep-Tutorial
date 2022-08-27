using System;
using System.Collections;
using System.Collections.Generic;
using Lockstep.Math;
using UnityEngine;


namespace Lockstep.Collision2D {
    [Serializable]
    public partial class ColliderData :IComponent{
        [Header("Offset")]
        public FP y;
        public FVector2 pos;
        [Header("Collider data")]
        public FP high;
        public FP radius;
        public FVector2 size;
        public FVector2 up;
        public FP deg;
        
    }
}