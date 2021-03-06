using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor;

namespace EasyLayout {
	[CustomEditor(typeof(EasyLayout), true)]
	[CanEditMultipleObjects]
	public class EasyLayoutEditor : Editor
	{
		Dictionary<string,SerializedProperty> sProperties = new Dictionary<string,SerializedProperty>();
		string[] properties = new string[]{
			"GroupPosition",
			"Stacking",
			"LayoutType",
			"RowAlign",
			"InnerAlign",
			"CellAlign",
			"Spacing",
			"Margin",
			"TopToBottom",
			"RightToLeft",
			"SkipInactive",
		};

		bool AutoUpdate = true;

		protected virtual void OnEnable()
		{
			sProperties.Clear();
			Array.ForEach(properties, x => sProperties.Add(x, serializedObject.FindProperty(x)));
		}
		
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(sProperties["GroupPosition"], true);
			EditorGUILayout.PropertyField(sProperties["Stacking"], true);
			EditorGUILayout.PropertyField(sProperties["LayoutType"], true);

			EditorGUI.indentLevel++;
			if (sProperties["LayoutType"].enumValueIndex==0)
			{
				EditorGUILayout.PropertyField(sProperties["RowAlign"], true);
				EditorGUILayout.PropertyField(sProperties["InnerAlign"], true);
			}
			if (sProperties["LayoutType"].enumValueIndex==1)
			{
				EditorGUILayout.PropertyField(sProperties["CellAlign"], true);
			}
			EditorGUI.indentLevel--;

			EditorGUILayout.PropertyField(sProperties["Spacing"], true);
			EditorGUILayout.PropertyField(sProperties["Margin"], true);
			EditorGUILayout.PropertyField(sProperties["SkipInactive"], true);
			EditorGUILayout.PropertyField(sProperties["RightToLeft"], true);
			EditorGUILayout.PropertyField(sProperties["TopToBottom"], true);

			if (targets.Length==1)
			{
				var script = (EasyLayout)target;

				EditorGUILayout.LabelField("Block size", string.Format("{0}x{1}", script.BlockSize.x, script.BlockSize.y));
				EditorGUILayout.LabelField("UI size", string.Format("{0}x{1}", script.UISize.x, script.UISize.y));
			}

			serializedObject.ApplyModifiedProperties();
			if (AutoUpdate)
			{
				UpdateLayout();
			}
			else
			{
				if (GUILayout.Button("Apply"))
				{
					UpdateLayout();
				}
			}
		}

		void UpdateLayout()
		{
			Array.ForEach(targets, x => ((EasyLayout)x).UpdateLayout());
		}


	}
}