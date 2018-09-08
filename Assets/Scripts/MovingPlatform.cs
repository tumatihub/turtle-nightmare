using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    Transform _wayPoint;
    Transform _startPoint;
    Transform _platform;
    Transform _body;
    bool _isActive = true;
    bool _isWaiting;
    Transform _nextPoint;
    float _timer;

    public float waypointWaitTime;
    public float platformSpeed;

	// Use this for initialization
	void Start () {
        _body = transform;
        _platform = _body.transform.parent;
        _wayPoint = _platform.Find("waypoint");
        if(_wayPoint == null)
        {
            Debug.LogError("Missing waypoint!");
        }
        _startPoint = _platform.Find("startpoint");
        if (_wayPoint == null)
        {
            Debug.LogError("Missing startpoint!");
        }
        _nextPoint = _wayPoint;
    }
	
	// Update is called once per frame
	void Update () {
		if (_isActive)
        {
            if (!_isWaiting)
            {
                // Go to nextpoit
                _body.Translate(
                    (_nextPoint.position - _body.transform.position).normalized * platformSpeed * Time.deltaTime
                );
                // se chegou
                if (Vector2.Distance(_nextPoint.position, _body.transform.position) <= 0.1)
                {
                    if (_nextPoint == _wayPoint)
                    {
                        _isWaiting = true;
                        _timer = waypointWaitTime;
                        _nextPoint = _startPoint;
                    }
                    else
                    {
                        _isWaiting = true;
                        _timer = waypointWaitTime;
                        _nextPoint = _wayPoint;
                    }
                }
            }
            else
            {
                if (_timer <= 0)
                {
                    _isWaiting = false;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }
	}
    
}
