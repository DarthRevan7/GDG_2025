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



    private void HandleIlluminationSkill()
    {
        if(Input.GetKeyDown(lightKey))
        {
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
        }
    }

    void Awake()
    {
        ambiente = GameObject.Find("Environment_Light").GetComponent<Light>();
        environmentLightIntensityStart = ambiente.intensity;
        localLight = GetComponentInChildren<Light>();
        localLightRadius = localLight.range;
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
