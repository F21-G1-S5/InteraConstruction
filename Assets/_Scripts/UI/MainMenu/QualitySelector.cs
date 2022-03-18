using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

/// <summary>
/// Provides methods for modifying the Unity Player's quality settings
/// </summary>
public class QualitySelector : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        //dropdown.value = QualitySettings.GetQualityLevel();
    }

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }
}
