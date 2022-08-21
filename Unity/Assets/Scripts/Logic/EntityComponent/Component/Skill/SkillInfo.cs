using System;
using System.Collections.Generic;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Game {
    [Serializable]
    public class SkillColliderInfo {
        public FVector2 pos;
        public FVector2 size;
        public FP radius;
        public FP deg = new FP(180);
        public FP maxY;

        public bool IsCircle => radius > 0;
    }

    [Serializable]
    public class SkillPart {
        public bool _DebugShow;
        public FP moveSpd;
        public FP startFrame;
        public FP startTimer => startFrame * SkillPart.AnimFrameScale;
        public SkillColliderInfo collider;
        public FVector3 impulseForce;
        public bool needForce;
        public bool isResetForce;

        public FP interval;
        public int otherCount;
        public int damage;
        public static FP AnimFrameScale = new FP(true, 1667);
        [HideInInspector] public FP DeadTimer => startTimer + interval * (otherCount + FP.half);

        public FP NextTriggerTimer(int counter){
            return startTimer + interval * counter;
        }
    }

    [Serializable]
    public class SkillInfo {
        public string animName;
        public FP CD;
        public FP doneDelay;
        public int targetLayer;
        public FP maxPartTime;
        public List<SkillPart> parts = new List<SkillPart>();

        public void DoInit(){
            parts.Sort((a, b) => LMath.Sign(a.startFrame - b.startFrame));
            var time = FP.MinValue;
            foreach (var part in parts) {
                var partDeadTime = part.DeadTimer;
                if (partDeadTime > time) {
                    time = partDeadTime;
                }
            }

            maxPartTime = time + doneDelay;
        }
    }
}