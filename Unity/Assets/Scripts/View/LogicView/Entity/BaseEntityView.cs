using Lockstep.Math;
using Lockstep.Util;
using UnityEngine;

namespace Lockstep.Game {
    public class BaseEntityView : MonoBehaviour, IEntityView {
        public const float LerpPercent = 0.3f;
        public BaseEntity baseEntity;
        
        public virtual void BindEntity(BaseEntity e, BaseEntity oldEntity = null){
            e.EntityView = this;
            this.baseEntity = e;
            var updateEntity = oldEntity ?? e;
            transform.localPosition = updateEntity.transform.LocPosition3.ToVector3();
            transform.localRotation = Quaternion.Euler(0, 0, updateEntity.transform.localRot.ToFloat());
            transform.localScale = updateEntity.transform.LocScale3.ToVector3();
        }
        
        public virtual void OnTakeDamage(int amount, FVector3 hitPoint){
        }
        
        protected virtual void Update()
        {
            var timeRate = Time.deltaTime / LTime.deltaTime.ToLFloat();
            var pos = baseEntity.transform.LocPosition3.ToVector3();
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, timeRate);
            var rot = baseEntity.transform.localRot.ToFloat();
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, rot), timeRate);
            var scale = baseEntity.transform.LocScale3.ToVector3();
            transform.localScale = Vector3.Lerp(transform.localScale, scale, timeRate);
        }

        public void SetParent(IEntityView parentView) {
            if (parentView is BaseEntityView parentEntityView) {
                transform.SetParent(parentEntityView.transform);
            }
        }

        public virtual void OnDead(){
            GameObject.Destroy(gameObject);
        }

        public virtual void OnRollbackDestroy(){
            GameObject.Destroy(gameObject);
        }
    }
}