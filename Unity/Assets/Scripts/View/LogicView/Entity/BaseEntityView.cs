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
            transform.position = updateEntity.transform.Position3.ToVector3();
            transform.rotation = Quaternion.Euler(0, 0, updateEntity.transform.rot.ToFloat());
            transform.localScale = updateEntity.transform.LossyScale3.ToVector3();
        }
        
        public virtual void OnTakeDamage(int amount, FVector3 hitPoint){
        }
        
        protected virtual void Update()
        {
            var timeRate = Time.deltaTime / FTime.deltaTime.ToLFloat();
            var pos = baseEntity.transform.Position3.ToVector3();
            transform.position = Vector3.Lerp(transform.position, pos, timeRate);
            var rot = baseEntity.transform.rot.ToFloat();
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rot), timeRate);
            var scale = baseEntity.transform.LossyScale3.ToVector3();
            transform.localScale = Vector3.Lerp(transform.localScale, scale, timeRate);
        }

        public virtual void OnDead(){
            GameObject.Destroy(gameObject);
        }

        public virtual void OnRollbackDestroy(){
            GameObject.Destroy(gameObject);
        }
    }
}