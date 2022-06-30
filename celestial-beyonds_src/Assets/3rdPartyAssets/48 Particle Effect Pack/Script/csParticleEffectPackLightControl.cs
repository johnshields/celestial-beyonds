using UnityEngine;

public class csParticleEffectPackLightControl : MonoBehaviour
{
    public Light _light;
    public float Delay = 0.5f;
    public float Down = 1;
    public Light _light2;
    public float Delay2;
    public float Delay3;
    public float Maxintensity;
    public float Up = 1;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        if (_light)
            if (_time > Delay)
            {
                if (_light.intensity > 0)
                    _light.intensity -= Time.deltaTime * Down;

                if (_light.intensity <= 0)
                    _light.intensity = 0;
            }

        if (_light2)
        {
            if (_time > Delay2 && _time < Delay2 + Delay3)
            {
                if (_light2.intensity <= Maxintensity)
                    _light2.intensity += Time.deltaTime * Up;
                if (_light2.intensity > Maxintensity)
                    _light2.intensity = Maxintensity;
            }

            if (_time > Delay2 + Delay3)
            {
                if (_light2.intensity > 0)
                    _light2.intensity -= Time.deltaTime * Down;

                if (_light2.intensity <= 0)
                    _light2.intensity = 0;
            }
        }
    }
}