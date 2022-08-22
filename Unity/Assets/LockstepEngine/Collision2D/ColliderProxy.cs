using Lockstep.Game;
using Lockstep.UnsafeCollision2D;
using Lockstep.Math;
#if UNITY_EDITOR
using UnityEngine;

#endif
namespace Lockstep.Collision2D {
    public delegate void FuncOnTriggerEvent(ColliderProxy other, ECollisionEvent type);

    public partial class ColliderProxy : ILPCollisionEventHandler, ILPTriggerEventHandler {
        public object EntityObject;
#if UNITY_EDITOR
        public Transform UnityTransform => (EntityObject as BaseEntity)?.engineTransform as Transform;
#endif
        public int Id;
        public int LayerType { get; set; }
        public ColliderPrefab Prefab;
        public CTransform2D Transform2D;
        public FP Height;
        public bool IsTrigger = true;
        public bool IsStatic = false;


        private FVector2 _prePos;
        private FP _preDeg;
        public static FP DegGap = new FP(true, 100);

        private FRect _bound;

        public FuncOnTriggerEvent OnTriggerEvent;

        private BoundsQuadTree _quadTree;

        private static int autoIncId = 0;
        
        public void Init(ColliderPrefab prefab, FVector2 pos){
            Init(prefab, pos, FP.zero);
        }

        public void Init(ColliderPrefab prefab, FVector2 pos, FP rot){
            Init(prefab, new CTransform2D(pos, rot));
        }

        public void Init(ColliderPrefab prefab, CTransform2D trans){
            this.Prefab = prefab;
            _bound = prefab.GetBounds();
            Transform2D = trans;
            _prePos = Transform2D.position;
            _preDeg = Transform2D.rot;
            unchecked {
                Id = autoIncId++;
            }
        }

        public void DoUpdate(FP deltaTime){
            var curPos = Transform2D.position;
            if (_prePos != curPos) {
                _prePos = curPos;
                IsMoved = true;
            }

            var curDeg = Transform2D.rot;
            if (FMath.Abs(curDeg - _preDeg) > DegGap) {
                _preDeg = curDeg;
                IsMoved = true;
            }
        }


        public bool IsMoved = true;

        public FVector2 pos {
            get => Transform2D.position;
            set {
                IsMoved = true;
                Transform2D.position = value;
            }
        }

        public FP rot {
            get => Transform2D.rot;
            set {
                IsMoved = true;
                Transform2D.rot = value;
            }
        }


        public FRect GetBounds(){
            return new FRect(_bound.position + pos, _bound.size);
        }

        public virtual void OnLPTriggerEnter(ColliderProxy other){ }
        public virtual void OnLPTriggerStay(ColliderProxy other){ }
        public virtual void OnLPTriggerExit(ColliderProxy other){ }
        public virtual void OnLPCollisionEnter(ColliderProxy other){ }
        public virtual void OnLPCollisionStay(ColliderProxy other){ }
        public virtual void OnLPCollisionExit(ColliderProxy other){ }
    }

    public interface ILPCollisionEventHandler {
        void OnLPTriggerEnter(ColliderProxy other);
        void OnLPTriggerStay(ColliderProxy other);
        void OnLPTriggerExit(ColliderProxy other);
    }

    public interface ILPTriggerEventHandler {
        void OnLPTriggerEnter(ColliderProxy other);
        void OnLPTriggerStay(ColliderProxy other);
        void OnLPTriggerExit(ColliderProxy other);
    }
}