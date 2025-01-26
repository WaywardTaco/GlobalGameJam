using Sirenix.OdinInspector;
using UnityEngine;

[ExecuteInEditMode]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Material skyBox;
    [SerializeField] private LightingPreset Preset;

    [SerializeField, PropertyRange(0, 24)] private float TimeOfDay;

    private void Update() {
        if(Preset == null) return;
        UpdateLighting(TimeOfDay/24f);
    }

    private void UpdateLighting(float timePercent) {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        if(DirectionalLight != null) {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 300f) - 90f, 170f, 0));
        }
        if(skyBox != null) {
            //skyBox.color = Color.white;
            skyBox.SetColor("_Tint", Preset.DirectionalColor.Evaluate(timePercent));
            skyBox.SetFloat("_Rotation", (timePercent * 300f) - 90f);
        }
    }

    private void OnValidate() {
        if(DirectionalLight != null) return;
        if(RenderSettings.sun != null) DirectionalLight = RenderSettings.sun;
        if(skyBox != null) skyBox = RenderSettings.skybox;
    }
}