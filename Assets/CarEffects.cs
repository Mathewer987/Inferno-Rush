using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    public TrailRenderer[] tireMarks;
    public AudioSource skidmark;
    public ParticleSystem[] smoke;
    private ControlPosta RR;
    private inputManager IM;
    private bool lightsFlag = false, tireMarksFlag;

    private bool smokeFlag = false;

    private void Start()
    {
        RR = gameObject.GetComponent<ControlPosta>();
        IM = gameObject.GetComponent<inputManager>();

    }

    private void FixedUpdate()
    {
        CheckDrift();


        if (RR.playPauseSmoke)
        {
            startSmoke();
        }
        else
        {
            stopSmoke();
        }

        if (smokeFlag)
        {
            for (int i = 0; i < smoke.Length; i++)
            {
                var emmision = smoke[i].emission;
                emmision.rateOverTime = ((int)RR.KPH * 2 <= 2000) ? (int)RR.KPH * 2 : 2000;
            }
        }

    }

    public void startSmoke()
    {
        if (smokeFlag) return;
        for (int i = 0; i < smoke.Length; i++)
        {
            var emmision = smoke[i].emission;
            emmision.rateOverTime = ((int)RR.KPH * 10 <= 2000) ? (int)RR.KPH * 10 : 2000;
            smoke[i].Play();
        }
        smokeFlag = true;
    }

    public void stopSmoke()
    {
        if (!smokeFlag) return;
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].Stop();
        }
        smokeFlag = false;

    }

    private void CheckDrift()
    {

        if (IM.FrenoDeMano == true)
        {
            startEmmiter();
        }
        else
        {
            stopEmmiter();
        }
    }

    private void startEmmiter()
    {
        if (tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = true;
        }
        skidmark.Play();
        tireMarksFlag = true;

    }

    private void stopEmmiter()
    {
        if (!tireMarksFlag) return;
        foreach (TrailRenderer T in tireMarks)
        {
            T.emitting = false;
        }
        skidmark.Stop();
        tireMarksFlag = false;
    }

}
