using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    public ParticleSystem[] smoke;
    private ControlPosta RR;
    private bool smokeFlag = false;

    private void Start()
    {
        RR = gameObject.GetComponent<ControlPosta>();
    }

    private void FixedUpdate()
    {
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
        for (int i = 0; i< smoke.Length; i++)
        {
            var emmision = smoke[i].emission;
            emmision.rateOverTime = ((int)RR.KPH * 10 <= 2000) ? (int) RR.KPH * 10 : 2000;
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
}
