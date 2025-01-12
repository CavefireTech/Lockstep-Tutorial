using System;
using System.Collections.Generic;
using System.Linq;
using Lockstep;
using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.Game {
    [Serializable]
    [NoBackup]
    public partial class BaseEntity : BaseLifeCycle, IEntity, ILPTriggerEventHandler {
        public int EntityId;
        public int PrefabId;
        public CTransform2D transform = new CTransform2D();
        [NoBackup] public BaseEntity Parent { get; private set; }
        [NoBackup] public object engineTransform;
        protected List<BaseComponent> allComponents;

        [ReRefBackup] public IGameStateService GameStateService { get; set; }
        [ReRefBackup] public IServiceContainer ServiceContainer { get; set; }
        [ReRefBackup] public IDebugService DebugService { get; set; }

        [ReRefBackup] public IEntityView EntityView;

        [NoBackup] public bool IsActive => Parent != null ? _isActive && Parent.IsActive : _isActive;
        
        private bool _isActive = true;
        
        public virtual void SetActive(bool isActive) {
            _isActive = isActive;
        }
        
        public void SetParent(BaseEntity setParent) {
            if (Parent == setParent) return;
            if (setParent == null) {
                return;
            }

            Parent = setParent;
            transform.SetParent(setParent.transform);
        }

        public T GetService<T>() where T : IService {
            return ServiceContainer.GetService<T>();
        }

        public void DoBindRef() {
            BindRef();
        }

        public virtual void OnRollbackDestroy() {
            EntityView?.OnRollbackDestroy();
            EntityView = null;
            engineTransform = null;
        }

        protected virtual void BindRef() {
            allComponents?.Clear();
        }

        protected void RegisterComponent(BaseComponent comp) {
            if (allComponents == null) {
                allComponents = new List<BaseComponent>();
            }

            allComponents.Add(comp);
            comp.BindEntity(this);
        }

        public override void DoAwake() {
            if (allComponents == null) return;
            foreach (var comp in allComponents) {
                if (comp.Enable) {
                    comp.DoAwake();
                }
            }
        }

        public override void DoStart() {
            if (allComponents == null) return;
            foreach (var comp in allComponents) {
                if (comp.Enable) {
                    comp.DoStart();
                }
            }
        }

        public override void DoUpdate(FP deltaTime) {
            if (allComponents == null) return;
            foreach (var comp in allComponents) {
                if (comp.Enable) {
                    comp.DoUpdate(deltaTime);
                }
            }
        }

        public override void DoDestroy() {
            if (allComponents == null) return;
            foreach (var comp in allComponents) {
                comp.DoDestroy();
            }
        }

        public virtual void OnLPTriggerEnter(ColliderProxy other) {
        }

        public virtual void OnLPTriggerStay(ColliderProxy other) {
        }

        public virtual void OnLPTriggerExit(ColliderProxy other) {
        }
    }
}