using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCharacter : MonoBehaviour
{
    [SerializeField] private float distance;

    private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
        ChangePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePosition()
    {
        transform.position = new Vector3(Random.insideUnitSphere.x * distance,
            transform.position.y,Random.insideUnitSphere.z * distance);
        transform.LookAt(player);
    }
}
