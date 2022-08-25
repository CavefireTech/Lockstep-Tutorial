using System;
using System.Collections.Generic;
using Lockstep;
using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lockstep.Game {
    [Serializable]
    public partial class Player : Entity {
        public int localId;
        public PlayerInput input = new PlayerInput();
        public CMover mover = new CMover();
 
        protected override void BindRef(){
            base.BindRef();
            RegisterComponent(mover);
        }
        public override void DoUpdate(FP deltaTime){
            base.DoUpdate(deltaTime);
            if (input.skillId != 0) {
                Fire(input.skillId);
            }
        }

        public override void OnLPTriggerEnter(ColliderProxy other) {
            base.OnLPTriggerEnter(other);
            Debug.Log($"trigger enter: {other}");
        }
    }
}