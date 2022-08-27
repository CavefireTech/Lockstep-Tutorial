#if UNITY_EDITOR
using UnityEngine;
using Lockstep.Math;

namespace UnityEditor {
    public static class EditorGUILayoutExt {
        public static FP FloatField( string label,FP value,params GUILayoutOption[] options){
            return EditorGUILayout.FloatField(label, value.ToFloat(),options).ToFP();
        }
        public static FVector2 Vector2Field( string label,FVector2 value,params GUILayoutOption[] options){
            return EditorGUILayout.Vector2Field(label, value.ToVector2(),options).ToFVector2();
        }
        public static FVector3 Vector3Field( string label,FVector3 value,params GUILayoutOption[] options){
            return EditorGUILayout.Vector3Field(label, value.ToVector3(),options).ToFVector3();
        }  
    }
}
#endif    