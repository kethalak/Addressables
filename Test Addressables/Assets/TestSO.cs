using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSO", menuName = "SO/TestSO", order = 1)]
public class TestSO : ScriptableObject {
    public List<GameObject> prefabs = new List<GameObject>();
}
