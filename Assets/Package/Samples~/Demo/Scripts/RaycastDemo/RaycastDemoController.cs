using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastDemoController : MonoBehaviour
{
    private const float LevelIntervalThreeshold = 1;
    [SerializeField] private int _MaxObjectsCount = 15;
    [SerializeField] private float _LevelWidth;
    [SerializeField] private AnimationCurve _InstantiateTimeCurve;
    [SerializeField] private float _InstantiateTimerSpeed = 0.05f;
    [SerializeField] private int _WinScore = 50;
    [SerializeField] private Transform _Start;
    [SerializeField] private Transform _End;
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private RaycastDemoObstacle _RaycastDemoObstacle;
    [SerializeField] private Vector2 _ObstacleSpeedRange;
    private List<RaycastDemoObstacle> _ObstaclesPool;
    private int _CurrentScore;
    private bool _IsWin;
    private float _InstantiateTimer;
    private void Start ()
    {
        _IsWin = false;
        _CurrentScore = 0;
        _InstantiateTimer = 0;
        _ObstaclesPool = new List<RaycastDemoObstacle>();
        _ScoreText.text = $"Score:{_CurrentScore}/{_WinScore}";
        StartCoroutine(LevelGenerator());
        Get();
    }
    private void Update ()
    {
        foreach (var obstacle in _ObstaclesPool.Where(item => item.isActiveAndEnabled))
        {
            var pos = obstacle.transform.position;
            pos.x = 0;
            if (Vector3.Distance(_Start.position, pos) + 
                Vector3.Distance(_End.position, pos) 
                > Vector3.Distance(_Start.position, _End.position) + LevelIntervalThreeshold)
            {
                obstacle.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator LevelGenerator ()
    {
        while (!_IsWin)
        {
            yield return new WaitForSeconds(_InstantiateTimeCurve.Evaluate(_InstantiateTimer));
            if (_ObstaclesPool.Count(x => x.isActiveAndEnabled) < _MaxObjectsCount)
            {
                _InstantiateTimer += _InstantiateTimerSpeed;
                if (_InstantiateTimer > 1)
                {
                    _InstantiateTimer = 1;
                }
                Get();
            }
        }
    }
    private RaycastDemoObstacle Get ()
    {
        var result = _ObstaclesPool.FirstOrDefault(x => !x.isActiveAndEnabled);
        if (result == null)
        {
            result = Instantiate(_RaycastDemoObstacle, _Start);
     
            result.OnKilled += OnKilledObstacle;
            _ObstaclesPool.Add(result);
        }
        var startPosition = _Start.position;
        result.transform.position = startPosition;
        result.transform.position += Vector3.right * Random.Range(-_LevelWidth, _LevelWidth);
        result.Speed = (_End.position - startPosition) * Random.Range(_ObstacleSpeedRange.x, _ObstacleSpeedRange.y);
        result.gameObject.SetActive(true);
        return result;
    }
    
    private void OnKilledObstacle ()
    {
        _CurrentScore++;
        _ScoreText.text = $"Score:{_CurrentScore}/{_WinScore}";
    }
    private void OnDestroy ()
    {
        foreach (var raycastDemoObstacle in _ObstaclesPool)
        {
            raycastDemoObstacle.OnKilled -= OnKilledObstacle;
            Destroy(raycastDemoObstacle.gameObject);
        }
        _ObstaclesPool.Clear();
    }

    private void OnDrawGizmos ()
    {
        if(_Start == null || _End == null) return;
        Gizmos.color = Color.blue;
        var endPosition = _End.position;
        var startPosition = _Start.position;
        Gizmos.DrawWireCube((startPosition + endPosition) / 2, startPosition - endPosition);
    }
}
