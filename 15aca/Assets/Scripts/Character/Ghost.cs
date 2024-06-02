using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float _ghostDelay;
    private float _ghostDelaySeconds;
    [SerializeField]
    private GameObject _ghost;
    public bool _isOnGhost=false;

    // Start is called before the first frame update
    void Start()
    {
        _ghostDelaySeconds = _ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isOnGhost)
        {
            if (_ghostDelaySeconds > 0)
            {
                _ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {// ghost »ý¼º
                GameObject _currentGhost = Instantiate(_ghost, transform.position, transform.rotation);
                _ghostDelaySeconds += _ghostDelay;
            }
        }
    }
}
