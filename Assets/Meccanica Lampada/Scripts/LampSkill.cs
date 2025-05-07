using System.Collections.Generic;
using UnityEngine;

public class LampSkill : MonoBehaviour
{

    //Illuminazione
    [SerializeField] private Light ambiente, localLight;
    [SerializeField] private ParticleSystem bodyLights;
    [SerializeField] private float localIllumination = 5f;
    [SerializeField] private float environmentLightIntensityStart, environmentLightIntensity;
    public bool localLightOn = false;
    public float localLightRadius = 5f;
    [SerializeField] private KeyCode lightKey;
    [SerializeField] private string envLightName = "Environment_Light";
    [SerializeField] private Light[] env_lights;
    [SerializeField] private List<float> envLightsStartIntensities;



    

    void Awake()
    {
        ambiente = GameObject.Find(envLightName).GetComponent<Light>();
        if(ambiente != null) {
            environmentLightIntensityStart = ambiente.intensity;
        }

        localLight = GetComponentInChildren<Light>();
        localLight.range = localLightRadius;
        localLight.intensity = 0f;

        if(env_lights.Length != 0) {
            envLightsStartIntensities = new List<float>();
            foreach(Light l in env_lights) {
                envLightsStartIntensities.Add(l.intensity);
            }
        }
        bodyLights = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleIlluminationSkill();
    }

    private void HandleIlluminationSkill()
    {
        if(Input.GetKeyDown(lightKey))
        {
            if(env_lights.Length == 0) {
                localLightOn = !localLightOn;
                if(localLightOn)
                {
                    ambiente.intensity = environmentLightIntensity;
                    localLight.intensity = localIllumination;
                    bodyLights.Play();
                }
                else
                {
                    ambiente.intensity = environmentLightIntensityStart;
                    localLight.intensity = 0f;
                    bodyLights.Stop();
                    bodyLights.Clear();
                }
            } else {
                localLightOn = !localLightOn;
                if(localLightOn)
                {
                    for(int i = 0; i < env_lights.Length; i++) {
                        env_lights[i].intensity = environmentLightIntensity;
                    }
                    localLight.intensity = localIllumination;
                    bodyLights.Play();
                }
                else
                {
                    for(int i = 0; i < env_lights.Length; i++) {
                        env_lights[i].intensity = envLightsStartIntensities[i];
                    }
                    localLight.intensity = 0f;
                    bodyLights.Stop();
                    bodyLights.Clear();
                }
            }
        }
    }
}
