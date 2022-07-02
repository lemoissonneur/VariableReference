using UnityEngine;
using UnityEditor;
using System.IO;

namespace VariableReference
{
    /// <summary>
    /// ScriptableObject to create new implementations of VariableReference
    /// </summary>
    public class CustomVariableReferenceCreator : ScriptableObject
    {
        [Header("Settings")]
        [Tooltip("Ignore 'Path' parameter and create the asset at the current ScriptableObject's path.")]
        public bool CreateAtCurrentPath;

        [Tooltip("Target path to create asset (this must be an existing folder).")]
        public string TargetFolder = "";

        [Tooltip("The type name you want to create a VariableReference for.")]
        public string TypeName;

        public void Create()
        {
            if (TargetParentFolderExist())
            {
                string newFolderGuid = AssetDatabase.CreateFolder(GetParentFolderPath().Remove(GetParentFolderPath().Length - 1), GetNewFolderName());

                string newFolderPath = AssetDatabase.GUIDToAssetPath(newFolderGuid);

                File.WriteAllText(newFolderPath + "/" + SOFileName.Replace("#CUSTOMTYPE#", TypeName), SOFileContent.Replace("#CUSTOMTYPE#", TypeName));
                File.WriteAllText(newFolderPath + "/" + MonoFileName.Replace("#CUSTOMTYPE#", TypeName), MonoFileContent.Replace("#CUSTOMTYPE#", TypeName));
                File.WriteAllText(newFolderPath + "/" + RefFileName.Replace("#CUSTOMTYPE#", TypeName), RefFileContent.Replace("#CUSTOMTYPE#", TypeName));

                AssetDatabase.Refresh();
            }
            else Debug.Log("target folder does not exist !");
        }

        public bool TargetParentFolderExist() => Directory.Exists(Application.dataPath.Remove(Application.dataPath.LastIndexOf("/")) + "/" + GetParentFolderPath());

        public string GetNewFolderPath() => GetParentFolderPath() + GetNewFolderName();

        public string GetParentFolderPath()
        {
            string parentFolderPath;

            if (CreateAtCurrentPath)
            {
                string thisPath = AssetDatabase.GetAssetPath(this);
                parentFolderPath = thisPath.Remove(thisPath.LastIndexOf('/')) + "/";
            }
            else
            {
                parentFolderPath = "Assets/";

                if (TargetFolder != "")
                {
                    parentFolderPath += TargetFolder;

                    if (TargetFolder[TargetFolder.Length - 1] != '/')
                        parentFolderPath += "/";
                }
            }

            return parentFolderPath;
        }

        public string GetNewFolderName()
        {
            return FolderName.Replace("#CUSTOMTYPE#", TypeName);
        }

        private const string FolderName = "#CUSTOMTYPE#VariableReference";

        public const string SOFileName = "#CUSTOMTYPE#ScriptableObjectVariable.cs";
        private const string SOFileContent = @"
using System;
using UnityEngine;


namespace VariableReference
{
	[Serializable, CreateAssetMenu(menuName = ""lemoissonneur/Variables/#CUSTOMTYPE# Variable"", order = 1)]
    public class #CUSTOMTYPE#ScriptableObjectVariable : ScriptableObjectVariable<#CUSTOMTYPE#> { }
}
";

        public const string MonoFileName = "#CUSTOMTYPE#MonoBehaviourVariable.cs";
        private const string MonoFileContent = @"
using System;
using UnityEngine;


namespace VariableReference
{
	[Serializable, AddComponentMenu(""lemoissonneur/Variables/#CUSTOMTYPE# Variable"", 1)]
    public class #CUSTOMTYPE#MonoBehaviourVariable : MonoBehaviourVariable<#CUSTOMTYPE#> { }
}
";

        public const string RefFileName = "#CUSTOMTYPE#Reference.cs";
        private const string RefFileContent = @"
using System;
using UnityEngine;


namespace VariableReference
{
    [Serializable]
    public class #CUSTOMTYPE#Reference : VariableReference<#CUSTOMTYPE#, #CUSTOMTYPE#ScriptableObjectVariable, #CUSTOMTYPE#MonoBehaviourVariable>
    {
        public #CUSTOMTYPE#Reference() : base() { }
        public #CUSTOMTYPE#Reference(#CUSTOMTYPE# value) : base(value) { }
        public #CUSTOMTYPE#Reference(#CUSTOMTYPE#ScriptableObjectVariable newVariable) : base(newVariable) { }
        public #CUSTOMTYPE#Reference(#CUSTOMTYPE#MonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
";

    }
}
