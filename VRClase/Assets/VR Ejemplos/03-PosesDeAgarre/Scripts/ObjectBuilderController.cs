using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectBuilderController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private List<GameObject> listOfobjects;
    
    /// <summary>
    /// Method HandleBuildPrefab
    /// </summary>
    /// <param name="index">Index prefab value</param>
    public void HandleBuildPrefab(int index)
    {
        var oPrefab = Instantiate(prefabs[index], transform.position, prefabs[index].transform.rotation);
        listOfobjects.Add(oPrefab);
    }
    
    /// <summary>
    /// Method HandleObjectRender
    /// </summary>
    /// <param name="value">Hide or Show value</param>
    public void HandleObjectRender(bool value)
    {
        foreach (var obj in listOfobjects)
            obj.gameObject.GetComponent<Renderer>().enabled = value;
    }
}
