using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCharacter : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private ParticleSystem fireworksParticle;
    private Transform player;
    private AudioSource _audioSource;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _audioSource = Camera.main.GetComponent<AudioSource>();
        player = Camera.main.transform;
        Move();
    }
    
    /// <summary>
    /// Method ChangePosition
    /// This method starts the position change process which is initialized by calling the corrutine to
    /// play particle system
    /// </summary>
    public void ChangePosition()
    {
        StartCoroutine(ManageParticleSystem());
    }
    
    /// <summary>
    /// Method ManageParticleSystem [Corrutine]
    /// This corrutine manages the coin sound, particle system and finally change a bat position
    /// </summary>
    /// <returns></returns>
    private IEnumerator ManageParticleSystem()
    {
        _audioSource.Play();
        fireworksParticle.Play();
        yield return new WaitForSeconds(fireworksParticle.main.duration);
        Move();
    }
    
    /// <summary>
    /// Method Move
    /// This method change the bat position
    /// </summary>
    private void Move()
    {
        transform.position = new Vector3(Random.insideUnitSphere.x * distance,
            transform.position.y,Random.insideUnitSphere.z * distance);
        transform.LookAt(player);
    }
}
