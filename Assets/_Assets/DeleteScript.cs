using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RemoveMissingComponents : MonoBehaviour
{
    [MenuItem("Tools/Remove Missing Components in Prefab")]
    public static void RemoveMissingComponentsInPrefab()
    {
        // Lấy prefab được chọn trong Project view
        var selectedPrefab = Selection.activeObject;

        if (selectedPrefab == null)
        {
            Debug.LogError("No prefab selected. Please select a prefab in the Project view.");
            return;
        }

        string path = AssetDatabase.GetAssetPath(selectedPrefab);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

        if (prefab == null)
        {
            Debug.LogError("Selected object is not a prefab.");
            return;
        }

        // Đếm số component script bị missing đã xóa
        int count = RemoveMissingComponentsRecursively(prefab);
        Debug.Log($"Removed {count} missing components from prefab: {path}");

        // Lưu các thay đổi
        PrefabUtility.SavePrefabAsset(prefab);
    }

    private static int RemoveMissingComponentsRecursively(GameObject gameObject)
    {
        int count = 0;
        // Duyệt qua tất cả các component của gameObject
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            // Tạo một danh sách để lưu các component bị missing
            var componentsToRemove = new List<Component>();

            // Duyệt qua từng component của đối tượng con
            foreach (var component in child.GetComponents<Component>())
            {
                if (component == null)
                {
                    componentsToRemove.Add(component);
                }
            }

            // Xóa các component bị missing
            foreach (var component in componentsToRemove)
            {
                DestroyImmediate(component);
                count++;
            }
        }
        return count;
    }
}
