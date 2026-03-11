using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpGradeData))]
public class UpGradeEditor : Editor
{
        public override void OnInspectorGUI()
    {
        UpGradeData data = (UpGradeData)target;

        EditorGUILayout.LabelField("Upgrade Settings", EditorStyles.boldLabel);

        data.upGradeName =
            EditorGUILayout.TextField("Upgrade Name", data.upGradeName);

        data.type =
            (UpGradeType)EditorGUILayout.EnumPopup("Type", data.type);

        EditorGUILayout.Space();

        switch (data.type)
        {
            case UpGradeType.Stat:

                data.statType =
                    (StatType)EditorGUILayout.EnumPopup("Stat Type", data.statType);

                data.value =
                    EditorGUILayout.FloatField("Value", data.value);

                break;

            case UpGradeType.WeaponUnlock:

                data.weaponData =
                    (WeaponData)EditorGUILayout.ObjectField(
                        "Weapon Data",
                        data.weaponData,
                        typeof(WeaponData),
                        false);

                break;

            case UpGradeType.WeaponUpgrade:

                data.weaponData =
                    (WeaponData)EditorGUILayout.ObjectField(
                        "Weapon Data",
                        data.weaponData,
                        typeof(WeaponData),
                        false);

                data.value =
                    EditorGUILayout.FloatField("Upgrade Value", data.value);

                break;
        }

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }
}
