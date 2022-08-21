using Lockstep.Game;
using Lockstep.Math;
using Lockstep.Util;
using UnityEngine;

namespace Lockstep.Game {
    public class EntityView : BaseEntityView, IEntityView {
        public UIFloatBar uiFloatBar;
        public Entity entity;
        protected bool isDead => entity?.isDead ?? true;

        public override void BindEntity(BaseEntity e, BaseEntity oldEntity = null){
            base.BindEntity(e,oldEntity);
            e.EntityView = this;
            this.entity = e as Entity;
            if (oldEntity != null) {
                uiFloatBar = (oldEntity.EntityView as EntityView).uiFloatBar;
            }else{
                uiFloatBar = FloatBarManager.CreateFloatBar(transform, this.entity.curHealth, this.entity.maxHealth);
            }
        }

        public override void OnTakeDamage(int amount, FVector3 hitPoint){
            uiFloatBar.UpdateHp(entity.curHealth, entity.maxHealth);
            FloatTextManager.CreateFloatText(hitPoint.ToVector3(), -amount);
        }

        public override void OnDead(){
            if (uiFloatBar != null) FloatBarManager.DestroyText(uiFloatBar);
            GameObject.Destroy(gameObject);
        }

        public override void OnRollbackDestroy(){
            if (uiFloatBar != null) FloatBarManager.DestroyText(uiFloatBar);
            GameObject.Destroy(gameObject);
        }

        private void Update()
        {
            var timeRate = Time.deltaTime / LTime.deltaTime.ToLFloat();
            var pos = entity.transform.Pos3.ToVector3();
            transform.position = Vector3.Lerp(transform.position, pos, timeRate);
            var deg = entity.transform.rot.ToFloat();
            //deg = Mathf.Lerp(transform.rotation.eulerAngles.y, deg, 0.3f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, deg, 0), timeRate);
        }    
        
        private void OnDrawGizmos(){
            if (entity.skillBox.isFiring) {
                var skill = entity.skillBox.curSkill;
                skill?.OnDrawGizmos();
            }
        }
    }
}