using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lockstep.Math;
using Lockstep.Util;
using UnityEngine;

namespace Lockstep.Collision2D {
    [Serializable]
    public partial class CTransform2D : IComponent {
        public CTransform2D parent;
        public bool WorldPosDirty => parent != null ? _worldPosDirty || parent._worldPosDirty : _worldPosDirty;
        private bool _worldPosDirty;
        private FVector2 _storedWorldPos;
        private FVector2 _localPosition;
        private FP _localRot;
        private FVector2 _localScale;

        public FVector2 localPosition {
            get => _localPosition;
            set {
                _localPosition = value;
                SetPosDirty();
            }
        }

        public FP localRot {
            get => _localRot;
            set {
                _localRot = value;
                SetRotDirty();
            }
        }

        public FVector2 localScale {
            get => _localScale;
            set {
                _localScale = value;
                SetScaleDirty();
            }
        }

        [NoBackup] public bool IsPosDirty { get; private set; }
        [NoBackup] public bool IsRotDirty { get; private set; }
        [NoBackup] public bool IsScaleDirty { get; private set; }

        [NoBackup]
        public FVector2 position {
            get {
                if (parent != null) {
                    if (WorldPosDirty) {
                        _storedWorldPos = localPosition;
                        _storedWorldPos.x *= parent.lossyScale.x;
                        _storedWorldPos.y *= parent.lossyScale.y;
                        _storedWorldPos = FVector2.Rotate(_storedWorldPos, parent.rot);
                        _storedWorldPos = parent.position + _storedWorldPos;
                        _worldPosDirty = false;
                    }
                    return _storedWorldPos;
                }
                else {
                    _worldPosDirty = false;
                    return localPosition;
                }
            }
            set {
                if (parent != null) {
                    var offset = value - parent.position;
                    offset.x /= parent.lossyScale.x;
                    offset.y /= parent.lossyScale.y;
                    localPosition = FVector2.Rotate(offset, -parent.rot);
                    _storedWorldPos = value;
                }
                else {
                    localPosition = value;
                }
                _worldPosDirty = false;
            }
        }

        [NoBackup]
        public FP rot {
            get {
                if (parent != null) {
                    return localRot + parent.rot;
                }
                else {
                    return localRot;
                }
            }
            set {
                if (parent != null) {
                    localRot = value - parent.rot;
                }
                else {
                    localRot = value;
                }
            }
        }

        [NoBackup]
        public FVector2 lossyScale {
            get {
                if (parent != null) {
                    var parentLossyScale = parent.lossyScale;
                    return new FVector2(localScale.x * parentLossyScale.x, localScale.y * parentLossyScale.y);
                }
                else {
                    return localScale;
                }
            }
        }

        [NoBackup]
        public FVector2 up {
            get => FVector2.Rotate(FVector2.up, rot);
            set => rot = ToRot(value);
        }

        [NoBackup]
        public FVector2 right {
            get => FVector2.Rotate(FVector2.right, rot);
            set => rot = FVector2.SignedAngle(FVector2.right, value);
        }

        void SetPosDirty() {
            IsPosDirty = true;
            _worldPosDirty = true;
        }

        void SetRotDirty() {
            IsRotDirty = true;
        }

        void SetScaleDirty() {
            IsScaleDirty = true;
        }

        public void Rotate(FP angle) {
            up = FVector2.Rotate(up, angle);
        }

        public void Translate(FVector2 translation) {
            position += translation;
        }

        public static FP ToRot(FVector2 value) {
            var ccwRot = FMath.Atan2(value.y, value.x) * FMath.Rad2Deg;
            var deg = 90 - ccwRot;
            return AbsRot(deg);
        }

        public static FP TurnToward(FVector2 targetPos, FVector2 currentPos, FP cursDeg, FP turnVal,
            out bool isLessDeg) {
            var toTarget = (targetPos - currentPos).normalized;
            var toDeg = CTransform2D.ToRot(toTarget);
            return TurnToward(toDeg, cursDeg, turnVal, out isLessDeg);
        }

        public static FP TurnToward(FP toRot, FP cursRot, FP turnVal,
            out bool isLessDeg) {
            var curRot = CTransform2D.AbsRot(cursRot);
            var diff = toRot - curRot;
            var absDiff = FMath.Abs(diff);
            isLessDeg = absDiff < turnVal;
            if (isLessDeg) {
                return toRot;
            }
            else {
                if (absDiff > 180) {
                    if (diff > 0) {
                        diff -= 360;
                    }
                    else {
                        diff += 360;
                    }
                }

                return curRot + turnVal * FMath.Sign(diff);
            }
        }

        public static FP AbsRot(FP deg) {
            var rawVal = deg._val % ((FP)360)._val;
            return new FP(true, rawVal);
        }

        public CTransform2D() {
        }

        public CTransform2D(FVector2 position) : this(position, FP.zero) {
        }

        public CTransform2D(FVector2 position, FP rot) {
            this.position = position;
            this.rot = rot;
        }

        public void Reset() {
            position = FVector2.zero;
            rot = FP.zero;
            localScale = FVector2.one;
        }

        public FVector2 TransformPoint(FVector2 point) {
            return position + TransformDirection(point);
        }

        public FVector2 TransformVector(FVector2 vec) {
            return TransformDirection(vec);
        }

        public FVector2 TransformDirection(FVector2 dir) {
            var y = up;
            var x = up.RightVec();
            return dir.x * x + dir.y * y;
        }

        public static Transform2D operator +(CTransform2D a, CTransform2D b) {
            return new Transform2D { position = a.position + b.position, rot = a.rot + b.rot };
        }

        [NoBackup]
        public FVector3 Position3 {
            get => new FVector3(position.x, position.y, FP.zero);
            set { position = new FVector2(value.x, value.y); }
        }

        [NoBackup]
        public FVector3 LocPosition3 {
            get => new FVector3(localPosition.x, localPosition.y, FP.zero);
            set { localPosition = new FVector2(value.x, value.y); }
        }


        [NoBackup]
        public FVector3 LocScale3 {
            get => new FVector3(localScale.x, localScale.y, FP.one);
            set { localScale = new FVector2(value.x, value.y); }
        }
        
        [NoBackup]
        public FVector3 LossyScale3 => new FVector3(lossyScale.x, lossyScale.y, FP.one);

        public void ResetParent() {
            if (parent != null) {
                //没有父物体时，localPosition 就是之前的世界position， localRot就是之前的世界rot
                this.localPosition = this.position;
                this.localRot = this.rot;
                parent = null;
            }
        }

        public void SetParent(CTransform2D setParent) {
            if (parent == setParent) return;
            ResetParent();
            if (setParent == null) {
                return;
            }

            parent = setParent;

            //因为设置坐标时一定会ResetParent，所以利用已经有的tsParent将世界坐标转化成本地坐标
            position = localPosition;
            rot = localRot;
        }

        public override string ToString() {
            return $"(rot:{rot}, pos:{position})";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = NativeHelper.STRUCT_PACK)]
    public unsafe struct Transform2D {
        public FVector2 position;
        public FP rot;
    }
}