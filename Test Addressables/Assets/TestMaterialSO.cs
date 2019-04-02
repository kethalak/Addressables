using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TestMaterial", menuName = "SO/TestMaterial", order = 1)]
public class TestMaterialSO : ScriptableObject {
    public Material material;
}
