using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Game Physics")]

    public float minDistance;

    public float target_angle;
    public float rope_angle;


    public float changeDrivingAngle;
    bool driftInfo = false;

    public bool driftFlag;
    public GameObject removableBridge;
    public int score;

    [Header("Refrences")]

    public CarView carView;


    private void Start()
    {

    }

    private void Update()
    {
        CheckDrift();

        carView.SetScoreText();

        if (carView.rope_radius <= minDistance && Input.GetButton("Fire1"))
        {

            carView.DriftState();
            driftInfo = true;
        }
        else
        {
            driftInfo = false;
            carView.NoDriftState();
        }


    }

    public void CheckDrift()
    {
        if (driftInfo) carView.startEmiiter();
        else carView.stopEmitter();
    }


}
