using System;
using Lockstep.Math;
#if UNITY_5_3_OR_NEWER
using Lockstep.Game;
using Lockstep.Util;
using UnityEngine;
#endif

namespace Lockstep.Collision2D {
    [RequireComponent(typeof(BoxCollider2D))]
    public class StaticColliderMono : MonoBehaviour {
        private BoxCollider2D _boxCollider;
        public EColliderLayer layer;

        private void Start() {
            if (_boxCollider == null) {
                _boxCollider = GetComponent<BoxCollider2D>();
            }
            
            FVector2 size = new FVector2(
                _boxCollider.size.ToFVector2().x * transform.localScale.ToFVector2().x / 2,
                _boxCollider.size.ToFVector2().y * transform.localScale.ToFVector2().y / 2
            );
            FVector2 pos = transform.position.ToFVector2();
            CreateProxy(pos, size);
        }

        [ContextMenu("CreateProxy")]
        private void CreateProxy(FVector2 pos, FVector2 size) {
            var prefab = CreatePrefab(pos,new CAABB(size));
            var proxy = PhysicSystem.Instance.CreateColliderProxy(prefab, transform.position.ToFVector2(), true,
                (int)layer);
            // proxy.OnTriggerEvent += OnTriggerEvent;
        }

        private ColliderPrefab CreatePrefab(FVector2 pos,CBaseShape collider) {
            var prefab = new ColliderPrefab();
            prefab.parts.Add(new ColliderPart() {
                transform = new CTransform2D(pos),
                collider = collider
            });
            return prefab;
        }
        // void OnTriggerEvent(ColliderProxy other, ECollisionEvent type) {
        //     if (type != ECollisionEvent.Stay) {
        //         Debug.Log(type);
        //     }
        // }
    }
}