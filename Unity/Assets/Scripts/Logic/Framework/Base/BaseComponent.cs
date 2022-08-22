using System;
using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using Lockstep.Serialization;

namespace Lockstep.Game {
    [Serializable]
    [NoBackup]
    public partial class BaseComponent : IComponent {
        public BaseEntity baseEntity { get; private set; }
        public CTransform2D transform { get; private set; }
        public bool Enable = true;
        private bool _enable = true;

        public virtual void BindEntity(BaseEntity entity) {
            this.baseEntity = entity;
            transform = entity.transform;
        }

        public virtual void DoAwake() { }
        public virtual void DoStart() { }
        public virtual void DoUpdate(FP deltaTime) { }
        public virtual void DoDestroy() { }

        public virtual void SetEnable(bool isEnable) {
            if (_enable == isEnable) return;
            _enable = isEnable;
            if (_enable) {
                OnEnable();
            }
            else {
                OnDisable();
            }
        }

        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}