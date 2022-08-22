using System;
using Lockstep.Collision2D;
using Lockstep.Logging;
using Lockstep.Math;

namespace Lockstep.Game {
    public delegate void OnFloorResultCallback(bool isOnFloor);

    [Serializable]
    public partial class CRigidbody : IComponent {
        public CTransform2D transform { get; private set; }
        public static FP G = new FP(10);
        public static FP MinSleepSpeed = new FP(true, 100);
        public static FP FloorFriction = new FP(20);
        public static FP MinYSpd = new FP(-10);
        public static FP FloorY = FP.zero;

        [ReRefBackup] public OnFloorResultCallback OnFloorEvent;

        public FVector3 Speed;
        public FP Mass = FP.one;
        public bool isEnable = true;
        public bool isSleep = false;
        public bool isOnFloor;

        public void BindRef(CTransform2D transform2D){
            this.transform = transform2D;
        }

        //private int __id;
        //private static int __idCount;
        public void DoStart(){
            //__id = __idCount++;
            FP y = FP.zero;
            isOnFloor = TestOnFloor(transform.Position3, ref y);
            Speed = FVector3.zero;
            isSleep = isOnFloor;
        }

        public void DoUpdate(FP deltaTime){
            if (!isEnable) return;
            if (!TestOnFloor(transform.Position3)) {
                isSleep = false;
            }

            if (!isSleep) {
                if (!isOnFloor) {
                    Speed.y -= G * deltaTime;
                    Speed.y = FMath.Max(MinYSpd, Speed.y);
                }

                var pos = transform.Position3;
                pos += Speed * deltaTime;
                FP y = pos.y;
                //Test floor
                isOnFloor = TestOnFloor(transform.Position3, ref y);
                if (isOnFloor && Speed.y <= 0) {
                    Speed.y = FP.zero;
                }

                if (Speed.y <= 0) {
                    pos.y = y;
                }

                //Test walls
                if (TestOnWall(ref pos)) {
                    Speed.x = FP.zero;
                    Speed.z = FP.zero;
                }

                if (isOnFloor) {
                    var speedVal = Speed.magnitude - FloorFriction * deltaTime;
                    speedVal = FMath.Max(speedVal, FP.zero);
                    Speed = Speed.normalized * speedVal;
                    if (speedVal < MinSleepSpeed) {
                        isSleep = true;
                    }
                }

                transform.Position3 = pos;
            }
        }


        public void AddImpulse(FVector3 force){
            isSleep = false;
            Speed += force / Mass;
            //Debug.Log(__id+ " AddImpulse " + force  +" after " + Speed);
        }

        public void ResetSpeed(FP ySpeed){
            Speed = FVector3.zero;
            Speed.y = ySpeed;
        }

        public void ResetSpeed(){
            Speed = FVector3.zero;
        }

        private bool TestOnFloor(FVector3 pos, ref FP y){
            var onFloor = pos.y <= 0; //TODO check with scene
            if (onFloor) {
                y = FP.zero;
            }

            return onFloor;
        }

        private bool TestOnFloor(FVector3 pos){
            var onFloor = pos.y <= 0; //TODO check with scene
            return onFloor;
        }

        private bool TestOnWall(ref FVector3 pos){
            return false; //TODO check with scene
        }
    }
}