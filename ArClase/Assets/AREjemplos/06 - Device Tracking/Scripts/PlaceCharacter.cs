using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCharacter : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private ParticleSystem fireworksParticle;
    private Transform player;
    private bool _isInit;
    
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
        StartCoroutine(ManageParticleSystem());

        /*transform.position = new Vector3(Random.insideUnitSphere.x * distance,
            transform.position.y,Random.insideUnitSphere.z * distance);
        transform.LookAt(player);*/
        
    }

    private IEnumerator ManageParticleSystem()
    {
        fireworksParticle.Play();
        yield return new WaitForSeconds(fireworksParticle.main.duration);
        transform.position = new Vector3(Random.insideUnitSphere.x * distance,
            transform.position.y,Random.insideUnitSphere.z * distance);
        transform.LookAt(player);
    }
    
}
