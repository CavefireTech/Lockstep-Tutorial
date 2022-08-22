using System;
using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Game {
    
    [Serializable]
    public partial class CMover : Component {
        public Player player => (Player) entity;
        public PlayerInput input => player.input;

        
        static FP _sqrStopDist = new FP(true, 40);
        public FP speed => player.moveSpd;
        public bool hasReachTarget = false;
        public bool needMove = true;

        public override void DoUpdate(FP deltaTime){
            var needChase = input.inputUV.sqrMagnitude > new FP(true, 10);
            if (needChase) {
                
            }

            var dir = input.inputUV.normalized;
            transform.position = transform.position + dir * speed * deltaTime;
            var targetDeg = dir.ToRot();
            transform.rot = CTransform2D.TurnToward(targetDeg, transform.rot, player.turnSpd * deltaTime, out var hasReachDeg);
            hasReachTarget = !needChase;
        }
    }
}