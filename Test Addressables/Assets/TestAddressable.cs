using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class TestAddressable : MonoBehaviour {
    public AssetReferenceTestSO testSORef;

    IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(1);
            StartCoroutine(LoadAsset());
        }
    }

    IEnumerator LoadAsset() {
        var op = Addressables.LoadAsset<TestSO>(testSORef);
        
        yield return op;
        var testSO = op.Result;
        
        var go = SpawnRandomly(testSO.prefabs[Random.Range(0, testSO.prefabs.Count)]);
        
        yield return new WaitForSeconds(2);
        
        var amazingScript = go.GetComponent<AmazingScript>();
        
        go.GetComponent<MeshRenderer>().material = amazingScript.materialHolder.material;
    }
    
    GameObject SpawnRandomly(GameObject prefab) {
        var go = Instantiate(prefab);
        go.transform.position = new Vector3(Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10));
        return go;
    }

}
[Serializable]
public class AssetReferenceTestSO : AssetReferenceT<TestSO> { }
