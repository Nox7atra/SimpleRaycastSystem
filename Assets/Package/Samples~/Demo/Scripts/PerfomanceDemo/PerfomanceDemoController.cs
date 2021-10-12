using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerfomanceDemoController : MonoBehaviour
{
    [SerializeField] private GameObject _PhysicsPrefab;
    [SerializeField] private GameObject _SimplePhysicsPrefab;
    [SerializeField] private int _CountTest = 100000;
    [SerializeField] private Vector2 _Volume;
    private List<GameObject> _PhysicsObjects;
    private List<GameObject> _SimplePhysicsObject;
    [SerializeField] private bool _IsPhysicsShow;
    private void Start ()
    {
	    _PhysicsObjects = new List<GameObject>();
	    _SimplePhysicsObject = new List<GameObject>();
	    Generate();
    }
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleActive();
        }
	}
    private void ActivateObjects ()
    {
	    foreach (var physicsObject in _PhysicsObjects)
	    {
		    physicsObject.SetActive(_IsPhysicsShow);
	    }
	    foreach (var physicsObject in _SimplePhysicsObject)
	    {
		    physicsObject.SetActive(!_IsPhysicsShow);
	    }
    }
    private void ToggleActive ()
    {
	    _IsPhysicsShow = !_IsPhysicsShow;
	    ActivateObjects();
    }
    private void Clear ()
    {
	    foreach (var physicsObject in _PhysicsObjects)
	    {
		    Destroy(physicsObject.gameObject);
	    }
	    _PhysicsObjects.Clear();
	    foreach (var physicsObject in _SimplePhysicsObject)
	    {
		    Destroy(physicsObject.gameObject);
	    }
	    _SimplePhysicsObject.Clear();
    }
    private void Generate ()
    {
	    for (int i = 0; i < _CountTest; i++)
	    {
		    var obj = Instantiate(_PhysicsPrefab);
		    obj.transform.position = GetRandomPos();
		    obj.SetActive(_IsPhysicsShow);
		    _PhysicsObjects.Add(obj);
		    
		    var simpObj = Instantiate(_SimplePhysicsPrefab);
		    simpObj.transform.position = GetRandomPos();
		    simpObj.SetActive(!_IsPhysicsShow);
		    _SimplePhysicsObject.Add(simpObj);
	    }
    }
    private Vector3 GetRandomPos ()
    {
	    return new Vector3(
		    Random.Range(-_Volume.x, _Volume.x),
		    Random.Range(-_Volume.y, _Volume.y)
	    );
    }
}
