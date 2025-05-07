using System.Collections.Generic;
using UnityEngine;

public class LampSkill : MonoBehaviour
{

    //Illuminazione
    [SerializeField] private Light ambiente, localLight;
    [SerializeField] private float localIllumination = 5f;
    [SerializeField] private float environmentLightIntensityStart, environmentLightIntensity;
    public bool localLightOn = false;
    public float localLightRadius;
    [SerializeField] private KeyCode lightKey;
    [SerializeField] private string envLightName = "Environment_Light";
    [SerializeField] private Light[] env_lights;
    [SerializeField] private List<float> envLightsStartIntensities;



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
                }
                else
                {
                    ambiente.intensity = environmentLightIntensityStart;
                    localLight.intensity = 0f;
                }
            } else {
                localLightOn = !localLightOn;
                if(localLightOn)
                {
                    for(int i = 0; i < env_lights.Length; i++) {
                        env_lights[i].intensity = environmentLightIntensity;
                    }
                    localLight.intensity = localIllumination;
                }
                else
                {
                    for(int i = 0; i < env_lights.Length; i++) {
                        env_lights[i].intensity = envLightsStartIntensities[i];
                    }
                    localLight.intensity = 0f;
                }
            }
        }
    }

    void Awake()
    {
        ambiente = GameObject.Find(envLightName).GetComponent<Light>();
        environmentLightIntensityStart = ambiente.intensity;
        localLight = GetComponentInChildren<Light>();
        localLight.range = localLightRadius;
        localLight.intensity = 0f;

        if(env_lights.Length != 0) {
            envLightsStartIntensities = new List<float>();
            foreach(Light l in env_lights) {
                envLightsStartIntensities.Add(l.intensity);
            }
        }
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
}
