using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles the UI elements and their behaviors
/// </summary>
public class UIController : MonoBehaviour
{
    public ModelMovementController movementController;
    public ModelOptionsController modelOptionsController;
    public EffectsManager effectsManager;

    public Button translateModelButton;
    public Button rotateModelButton;
    public Button scaleModelButton;

    public VisualElement optionsContainer;
    public VisualElement effectOptionsContainer;
    public VisualElement modelOptionsContainer;

    public ListView modelOptionsListView;

    void Start()
    {
        //Store references to UI objects
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        optionsContainer = root.Q<VisualElement>("GeneralOptions-Container");
        effectOptionsContainer = root.Q<VisualElement>("EffectOptions-Container");
        modelOptionsContainer = root.Q<VisualElement>("ModelOptions-Container");

        modelOptionsListView = root.Q<ListView>("DynamicOptions-ListView");

        //Setup press events for buttons
        root.Q<Button>("Options-Button").clicked += OptionsButton_clicked;

        root.Q<Button>("ModelOptions-Button").clicked += () => GeneralOptionSelect(true);
        root.Q<Button>("EffectOptions-Button").clicked += () => GeneralOptionSelect(false);

        root.Q<Button>("TranslationModel-Button").clicked += () => movementController.SetMovementType(ModelMovementController.MovementType.Translate);
        root.Q<Button>("RotateModel-Button").clicked += () => movementController.SetMovementType(ModelMovementController.MovementType.Rotate);
        root.Q<Button>("ScaleModel-Button").clicked += () => movementController.SetMovementType(ModelMovementController.MovementType.Scale);

        root.Q<Button>("LightEffects-Button").clicked += LightButton_clicked;
        root.Q<Button>("PostProcessing-Button").clicked += PostProcessingButton_clicked;

        root.Q<Button>("Textures-Button").clicked += TexturesButton_clicked;
        root.Q<Button>("Meshes-Button").clicked += MeshesButton_clicked;
        root.Q<Button>("Materials-Button").clicked += MaterialsButton_clicked;

        ResetOptionsMenu();
    }

    private void SetUpListView(IList itemSource, Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
    {
        modelOptionsListView.makeItem = null;
        modelOptionsListView.bindItem = null;

        modelOptionsListView.style.display = DisplayStyle.Flex;
        modelOptionsListView.itemsSource = itemSource;
        modelOptionsListView.makeItem = makeItem;
        modelOptionsListView.bindItem = bindItem;
    }

    private void MaterialsButton_clicked()
    {
        //Set up the List View to have all of the options for the available materials
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = modelOptionsController.materialOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewMaterial(button.text);

        };

        SetUpListView(modelOptionsController.materialOptions, makeItem, bindItem);
    }

    private void MeshesButton_clicked()
    {
        //Set up the List View to have all of the options for the available meshes
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = modelOptionsController.meshOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewMesh(button.text);
        };
        SetUpListView(modelOptionsController.meshOptions, makeItem, bindItem);
    }

    private void TexturesButton_clicked()
    {
        //Set up the List View to have all of the options for the available textures
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = modelOptionsController.textureOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewTexture(button.text);
        };
        SetUpListView(modelOptionsController.textureOptions, makeItem, bindItem);
    }

    private void OptionsButton_clicked()
    {
        if (optionsContainer.style.display == DisplayStyle.None)
            optionsContainer.style.display = DisplayStyle.Flex;
        else
            ResetOptionsMenu();
    }

    private void GeneralOptionSelect(bool modelOptionsSelected)
    {
        if(modelOptionsSelected)
        {
            //If this container is not already displaying, display it and hide the rest of them
            if (modelOptionsContainer.style.display == DisplayStyle.None)
            {
                modelOptionsContainer.style.display = DisplayStyle.Flex;

                effectOptionsContainer.style.display = DisplayStyle.None;
                modelOptionsListView.style.display = DisplayStyle.None;
            }
        }
        else
        {
            if (effectOptionsContainer.style.display == DisplayStyle.None)
            {
                effectOptionsContainer.style.display = DisplayStyle.Flex;

                modelOptionsContainer.style.display = DisplayStyle.None;
                modelOptionsListView.style.display = DisplayStyle.None;
            }
        }
    }

    private void ResetOptionsMenu()
    {
        optionsContainer.style.display = DisplayStyle.None;
        effectOptionsContainer.style.display = DisplayStyle.None;
        modelOptionsContainer.style.display = DisplayStyle.None;
        modelOptionsListView.style.display = DisplayStyle.None;
    }

    void LightButton_clicked()
    {
        Debug.Log("AddLightButtonPressed");
    }

    void PostProcessingButton_clicked()
    {
        //Set up the List View to have all of the options for the available post processing effects
        Func<VisualElement> makeItem = () => new Toggle();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Toggle toggle = (e as Toggle);
            toggle.AddToClassList("toggle-style");
            toggle.text = effectsManager.postProcessingEffects[i].displayName;
            toggle.RegisterValueChangedCallback((state) => effectsManager.SetPostProcessingEffectState(state.newValue, effectsManager.postProcessingEffects[i].effectName));
        };

        SetUpListView(effectsManager.postProcessingEffects, makeItem, bindItem);
    }
}
