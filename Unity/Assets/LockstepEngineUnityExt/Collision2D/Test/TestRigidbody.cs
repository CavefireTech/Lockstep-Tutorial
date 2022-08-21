using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Collision2D {
    public class TestRigidbody : MonoBehaviour {
        public CRigidbody CRigidbody;
        public CTransform2D CTransform2D;

        public FP G;
        public FVector3 force;
        
        public FP MinSleepSpeed = new FP(true, 100);
        public FP FloorFriction = new FP(3);
        public FP Mass = FP.one;
        public FP resetYSpd = new FP(true,100);
        private void Start(){
            CRigidbody = new CRigidbody();
            CTransform2D = new CTransform2D();
            CTransform2D.Pos3 = transform.position.ToLVector3();
            CRigidbody.BindRef(CTransform2D); 
            CRigidbody.DoStart();
        }

        private void Update(){
            CRigidbody.G = G;
            CRigidbody.MinSleepSpeed = MinSleepSpeed;
            CRigidbody.FloorFriction = FloorFriction;
            
            CRigidbody.Mass = Mass;
            CRigidbody.DoUpdate(Time.deltaTime.ToLFloat());
            transform.position = CTransform2D.Pos3.ToVector3();
        }

        public void AddImpulse(){
            CRigidbody.AddImpulse(force);
        }  
        public void ResetSpeed(FP ySpeed){
            CRigidbody.ResetSpeed(ySpeed);
        }
    }
}