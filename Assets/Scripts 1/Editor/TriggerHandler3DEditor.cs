using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityUtils;

[CustomEditor(typeof(TriggerHandler3D))]
public class TriggerHandler3DEditor : Editor
{
    SerializedProperty targetScript;
    SerializedProperty enterMethodName;
    SerializedProperty stayMethodName;
    SerializedProperty exitMethodName;

    int selectedEnterMethod = 0;
    int selectedStayMethod = 0;
    int selectedExitMethod = 0;

    IEnumerable<MethodInfo> methods;
    List<string> methodOptions = new List<string>();

    private void OnEnable()
    {
        targetScript = serializedObject.FindProperty("target");
        enterMethodName = serializedObject.FindProperty("methodEnterName");
        stayMethodName = serializedObject.FindProperty("methodStayName");
        exitMethodName = serializedObject.FindProperty("methodExitName");

        if (targetScript.objectReferenceValue != null)
        {
            methodOptions.Clear();
            methods = GetMethods();
            methodOptions.Add("Disabled");
            methodOptions.AddRange(methods.Select(m => m.Name));
            if (!string.IsNullOrEmpty(enterMethodName.stringValue)) selectedEnterMethod = methodOptions.IndexOf(enterMethodName.stringValue);
            if (!string.IsNullOrEmpty(stayMethodName.stringValue)) selectedStayMethod = methodOptions.IndexOf(stayMethodName.stringValue);
            if (!string.IsNullOrEmpty(exitMethodName.stringValue)) selectedExitMethod = methodOptions.IndexOf(exitMethodName.stringValue);
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(targetScript, new GUIContent("Target"));

        if (targetScript.objectReferenceValue != null)
        {
            methodOptions.Clear();
            methods = GetMethods();
            methodOptions.Add("Disabled");
            methodOptions.AddRange(methods.Select(m => m.Name));

            EditorGUI.BeginChangeCheck();
            selectedEnterMethod = EditorGUILayout.Popup(new GUIContent("Enter Method"), selectedEnterMethod, methodOptions.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedEnterMethod == 0) enterMethodName.stringValue = null;
                else enterMethodName.stringValue = methodOptions[selectedEnterMethod];
            }

            EditorGUI.BeginChangeCheck();
            selectedStayMethod = EditorGUILayout.Popup(new GUIContent("Stay Method"), selectedStayMethod, methodOptions.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedStayMethod == 0) stayMethodName.stringValue = null;
                else stayMethodName.stringValue = methodOptions[selectedStayMethod];
            }

            EditorGUI.BeginChangeCheck();
            selectedExitMethod = EditorGUILayout.Popup(new GUIContent("Exit Method"), selectedExitMethod, methodOptions.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedExitMethod == 0) exitMethodName.stringValue = null;
                else exitMethodName.stringValue = methodOptions[selectedExitMethod];
            }

        }
        serializedObject.ApplyModifiedProperties();
    }

    private IEnumerable<MethodInfo> GetMethods()
    {
        return targetScript.objectReferenceValue.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).Where(m =>
        {
            if (m.DeclaringType != targetScript.objectReferenceValue.GetType()) return false;

            var p = m.GetParameters();
            if (p.Length > 1) return false;
            if (p.Length == 1 && p[0].ParameterType != typeof(Collider)) return false;
            return true;
        });
    }
}
