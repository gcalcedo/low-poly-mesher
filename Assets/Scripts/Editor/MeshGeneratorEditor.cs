using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
    protected MeshGenerator mt;
    protected SerializedObject serializedGenerator;

    private static List<string> templates;
    private static Dictionary<string, Type> templateMap;

    void OnEnable()
    {
        mt = (MeshGenerator)target;
        serializedGenerator = new SerializedObject(mt);

        templates = new List<string>();
        templateMap = new Dictionary<string, Type>();

        foreach(Type t in typeof(MeshTemplate).GetInheritedClasses())
        {
            Debug.Log(t.Name);
            TemplatePathAttribute templatePath = Attribute.GetCustomAttribute(t, typeof(TemplatePathAttribute)) as TemplatePathAttribute;

            if (templatePath is null) continue;

            templates.Add(templatePath.Path);
            templateMap.Add(t.Name, t);
        }
    }

    public override void OnInspectorGUI()
    {
        var rect = GUILayoutUtility.GetRect(new GUIContent(), EditorStyles.toolbarButton);

        string displayName = mt.templateAssemblyName == "" ? "Select a Template" : mt.templateAssemblyName.Split(',')[0];

        if (GUI.Button(rect, new GUIContent(displayName)))
        {
            StringListSearchProvider provider = CreateInstance<StringListSearchProvider>();
            provider.Init(templates, (t) =>
            {
                mt.templateAssemblyName = templateMap[t].AssemblyQualifiedName;
                mt.template = Type.GetType(mt.templateAssemblyName).ConstructDefault();
            });

            SearchWindow.Open(new SearchWindowContext(
                GUIUtility.GUIToScreenPoint(rect.center) - new Vector2(0, -rect.height - 4), rect.width - rect.width * 0.3f),
                provider);
        }

        serializedGenerator.Update();
        //using excluding, instead of marking primary spell HideInInspector because it will mess up the serialized property
        //DrawPropertiesExcluding(serializedGenerator, new string[] { ABILITY_FACTORY_NAME });
        DrawTemplate();
        serializedGenerator.ApplyModifiedProperties();
    }

    protected void DrawTemplate()
    {
        if (mt.templateAssemblyName == "") return;

        SerializedProperty specificTemplate = serializedGenerator.FindProperty("template");
        IEnumerable<ParameterInfo> constructorParams = Type.GetType(mt.templateAssemblyName).GetConstructorParameters();

        string parentPath = specificTemplate.propertyPath;
        while (specificTemplate.NextVisible(true) && specificTemplate.propertyPath.StartsWith(parentPath))
        {
            if (!constructorParams.Any(param => param.Name == specificTemplate.name)) continue;

            EditorGUILayout.PropertyField(specificTemplate);
        }
    }
}
