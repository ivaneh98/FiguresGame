using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 touch;
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private float minForce = 100f;
    [SerializeField]
    private float maxForce = 600f;
    [SerializeField]
    private float maxTime = 60f;
    private float forcePerSec;
    private GameObject ball;
    private float time;
    private bool isPlaying=true;
    // Start is called before the first frame update
    void Start()
    {
        forcePerSec = (maxForce - minForce) / maxTime;
        EventManager.OnPVPResult += StopGame;
        EventManager.OnPlayerDie += StopGame;
        EventManager.OnContinue += Continue;

    }

    private void StopGame(string arg1, int arg2)
    {
        isPlaying = false;
    }
    private void StopGame(int arg2)
    {
        isPlaying = false;
    }
    private void Continue()
    {
        isPlaying = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
            return;
       
        float angle = 0;
        touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        angle = CalculateAngle(transform, touch);
        transform.Rotate(0, 0, -angle);

        if (Input.GetMouseButtonDown(0))
        {
            EventManager.SendChannellingStarted(maxTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EventManager.SendChannellingStoped();

            ball = Instantiate(obj, transform.position, Quaternion.identity);
            float newObjectAngle = CalculateAngle(ball.transform, touch);
            ball.transform.Rotate(0, 0, -newObjectAngle);
            Rigidbody2D RB;
            RB = ball.GetComponent<Rigidbody2D>();

            RB.AddForce(ball.transform.up * (minForce + forcePerSec*time));
            time = 0;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            time += Time.fixedDeltaTime;
            if (time > maxTime)
                time = maxTime;
        }
    }
    private float CalculateAngle(Transform _transform, Vector3 target)
    {
        float angle = 0;

        Vector3 relative = _transform.InverseTransformPoint(target);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        return angle;
    }
    private void OnDestroy()
    {
        EventManager.OnPVPResult -= StopGame;
        EventManager.OnPlayerDie -= StopGame;
        EventManager.OnContinue -= Continue;
    }
}
