using System;
using Lockstep.Math;
#if UNITY_5_3_OR_NEWER
using Lockstep.Game;
using Lockstep.Util;
using UnityEngine;
#endif

namespace Lockstep.Collision2D {
#if UNITY_5_3_OR_NEWER
    public class ColliderDataMono : UnityEngine.MonoBehaviour {
        public ColliderData colliderData;
        public EShape2D shapeType;
        public bool isStatic;
        public EColliderLayer layer;
        public int showTreeId;
        ColliderPrefab CreatePrefab(CBaseShape collider){
            var prefab = new ColliderPrefab();
            prefab.parts.Add(new ColliderPart() {
                transform = new CTransform2D(FVector2.zero),
                collider = collider
            });
            return prefab;
        }

        private void Start() {
            CreateProxy();
        }

        [ContextMenu("CreateProxy")]
        private void CreateProxy() {
            var prefab = CreatePrefab(new CCircle(colliderData.radius));
            var proxy = PhysicSystem.Instance.CreateColliderProxy(prefab, transform.position.ToLVector2XZ(), isStatic, (int)layer);
            proxy.OnTriggerEvent += OnTriggerEvent;
        }

        void OnTriggerEvent(ColliderProxy other, ECollisionEvent type){
            if (type != ECollisionEvent.Stay) {
                Debug.Log(type);
            }
        }

        void Update() {
            PhysicSystem.Instance.showTreeId = showTreeId;
        }
    }
#endif
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