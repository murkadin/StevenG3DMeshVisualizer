using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button optionsButton;
    public Button addLightButton;
    public Button addCameraButton;

    public VisualElement optionsContainer;
    public VisualElement effectContainer;


    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        addLightButton = root.Q<Button>("AddLight");
        addCameraButton = root.Q<Button>("AddCamera");
        optionsButton = root.Q<Button>("Options-Button");

        optionsContainer = root.Q<VisualElement>("OptionsContainer");
        effectContainer = root.Q<VisualElement>("EffectContainer");

        optionsButton.clicked += OptionsButton_clicked; ;
        addLightButton.clicked += AddLightButtonPressed;
        addCameraButton.clicked += AddCameraButtonPressed;
    }

    private void OptionsButton_clicked()
    {
        Debug.Log("OptionsButton_clicked");
        optionsContainer.style.display = DisplayStyle.None;
        effectContainer.style.display = DisplayStyle.Flex;

    }

    void AddLightButtonPressed()
    {
        Debug.Log("AddLightButtonPressed");
    }

    void AddCameraButtonPressed()
    {
        Debug.Log("AddCameraButtonPressed");

    }
}
