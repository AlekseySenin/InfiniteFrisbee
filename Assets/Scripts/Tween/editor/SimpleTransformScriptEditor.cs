using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleTransformTweenSequence))]
public class SimpleTransformScriptEditor : Editor
{
    Vector2 scrollPos;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SimpleTransformTweenSequence _target = (SimpleTransformTweenSequence)target;
        if (GUILayout.Button("↓+", GUILayout.MinHeight(25)))
        {
            _target.simpleTransformTweenDatas.Insert(0, new SimpleTransformTweenData());
        }
        if (_target.simpleTransformTweenDatas.Count > 0)
        {
            var ScrollHight = Mathf.Clamp(_target.simpleTransformTweenDatas.Count * 80, 0, 300);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(ScrollHight));
            for (int i = 0; i < _target.simpleTransformTweenDatas.Count; i++)
            {
                EditorGUILayout.Space();
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(20));
                GUILayout.BeginVertical();
                if (GUILayout.Button("↑+", GUILayout.MinHeight(25)))
                {
                    _target.simpleTransformTweenDatas.Insert(i, new SimpleTransformTweenData());
                }
                if (GUILayout.Button("↓+", GUILayout.MinHeight(25)))
                {
                    _target.simpleTransformTweenDatas.Insert(i + 1, new SimpleTransformTweenData());
                }

                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                if (i > 0)
                {
                    if (GUILayout.Button("↑", GUILayout.MinHeight(25)))
                    {
                        var temp = _target.simpleTransformTweenDatas[i];
                        _target.simpleTransformTweenDatas[i] = _target.simpleTransformTweenDatas[i - 1];
                        _target.simpleTransformTweenDatas[i - 1] = temp;
                    }
                }
                if (i < _target.simpleTransformTweenDatas.Count - 1)
                    if (GUILayout.Button("↓", GUILayout.MinHeight(25)))
                    {
                        var temp = _target.simpleTransformTweenDatas[i];
                        _target.simpleTransformTweenDatas[i] = _target.simpleTransformTweenDatas[i + 1];
                        _target.simpleTransformTweenDatas[i + 1] = temp;
                    }

                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                _target.simpleTransformTweenDatas[i].type = (TweenType)EditorGUILayout.EnumPopup("TweenType", _target.simpleTransformTweenDatas[i].type);
                _target.simpleTransformTweenDatas[i].finalValue = EditorGUILayout.Vector3Field("finalValue", _target.simpleTransformTweenDatas[i].finalValue);
                _target.simpleTransformTweenDatas[i].time = EditorGUILayout.FloatField("time", _target.simpleTransformTweenDatas[i].time);
                GUILayout.EndVertical();
                if (GUILayout.Button("X", GUILayout.MinHeight(50)))
                {
                    _target.simpleTransformTweenDatas.RemoveAt(i);
                }

                GUILayout.EndHorizontal();
                EditorGUILayout.Space();

            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("↑+", GUILayout.MinHeight(25)))
            {
                _target.simpleTransformTweenDatas.Add(new SimpleTransformTweenData());
            }
        }
    }
}
