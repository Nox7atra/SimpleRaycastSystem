using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastDemoObstacle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MeshRenderer _MeshRenderer;
    [SerializeField] private ParticleSystem _Particle;

    public Vector3 Speed;
    public event Action OnKilled;
    private bool _IsKilled;
    private void OnEnable ()
    {
        _MeshRenderer.enabled = true;
        _Particle.gameObject.SetActive(false);
        _IsKilled = false;
    }
    public void OnPointerDown (PointerEventData eventData)
    {
        if(_IsKilled) return;
        
        _IsKilled = true;
        _MeshRenderer.enabled = false;
        Speed = new Vector3();
        _Particle.gameObject.SetActive(true);
        OnKilled?.Invoke();
        StartCoroutine(Deactivate(_Particle.main.startLifetime.constant));
    }

    private IEnumerator Deactivate (float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void Update ()
    {
        transform.position += Speed * Time.deltaTime;
    }
}
