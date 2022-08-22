using System;
using Lockstep.Collision2D;
using Lockstep.Math;

namespace Lockstep.Game {
    [Serializable]
    public partial class CBrain : Component {
        public Entity target { get; private set; }
        public int targetId;
        public FP stopDistSqr = 1 * 1;
        public FP atkInterval = 1;
        [Backup] private FP _atkTimer;

        public override void BindEntity(BaseEntity e){
            base.BindEntity(e);
            target = GameStateService.GetEntity(targetId) as Entity;
        }

        public override void DoUpdate(FP deltaTime){
            // if (!entity.rigidbody.isOnFloor) {
            //     return;
            // }

            //find target
            var allPlayer = GameStateService.GetPlayers();
            var minDist = FP.MaxValue;
            Entity minTarget = null;
            foreach (var player in allPlayer) {
                if (player.isDead) continue;
                var dist = (player.transform.position - transform.position).sqrMagnitude;
                if (dist < minDist) {
                    minTarget = player;
                    minDist = dist;
                }
            }

            target = minTarget;
            targetId = target?.EntityId ?? -1;

            if (minTarget == null)
                return;
            if (minDist > stopDistSqr) {
                // turn to target
                var targetPos = minTarget.transform.position;
                var currentPos = transform.position;
                var turnVal = entity.turnSpd * deltaTime;
                var targetDeg = CTransform2D.TurnToward(targetPos, currentPos, transform.rot, turnVal,
                    out var isFinishedTurn);
                transform.rot = targetDeg;
                //move to target
                var distToTarget = (targetPos - currentPos).magnitude;
                var movingStep = entity.moveSpd * deltaTime;
                if (movingStep > distToTarget) {
                    movingStep = distToTarget;
                }

                var toTarget = (targetPos - currentPos).normalized;
                transform.position = transform.position + toTarget * movingStep;
            }
            else {
                //atk target
                _atkTimer -= deltaTime;
                if (_atkTimer <= 0) {
                    _atkTimer = atkInterval;
                    //Atk
                    target.TakeDamage(entity, entity.damage, target.transform.Position3);
                }
            }
        }
    }
}