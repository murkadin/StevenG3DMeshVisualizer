using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles the UI elements and their behaviors
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField]
    private ModelAssetsController _modelOptionsController;

    [SerializeField]
    private ModelMovementController _modelMovementController;

    [SerializeField]
    private EffectsManager _effectsManager;

    /// <summary>
    /// Holds the options Model and Effects so the user can decide which of the two they would like to view in detail.
    /// </summary>
    private VisualElement _optionsCategoryContainer;

    /// <summary>
    /// Holds the general options for the two types of effects. Lights and Post Processing.
    /// </summary>
    private VisualElement _effectCategoryContainer;

    /// <summary>
    /// Holds the general options for the different options to you can do to the model.
    /// </summary>
    private VisualElement _modelCategoryContainer;

    /// <summary>
    /// The ListView which is populated dynamically at runtime with the different options available to the user based on which category they chose.
    /// For example, if the user chooses the Light Effects, it will be populated with all of the light effects available to the user.
    /// </summary>
    private ListView _dynamicOptionsListView;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Store references to UI objects.
        _optionsCategoryContainer = root.Q<VisualElement>("GeneralOptions-Container");
        _effectCategoryContainer = root.Q<VisualElement>("EffectOptions-Container");
        _modelCategoryContainer = root.Q<VisualElement>("ModelOptions-Container");

        _dynamicOptionsListView = root.Q<ListView>("DynamicOptions-ListView");

        //Setup press events for buttons
        root.Q<Button>("Options-Button").clicked += OptionsButton_clicked;

        root.Q<Button>("ModelOptions-Button").clicked += ModelOptions_clicked;
        root.Q<Button>("EffectOptions-Button").clicked += EffectsOptions_clicked;

        root.Q<Button>("LightEffects-Button").clicked += LightButton_clicked;
        root.Q<Button>("PostProcessing-Button").clicked += PostProcessingButton_clicked;

        root.Q<Button>("Textures-Button").clicked += TexturesButton_clicked;
        root.Q<Button>("Meshes-Button").clicked += MeshesButton_clicked;
        root.Q<Button>("Materials-Button").clicked += MaterialsButton_clicked;
        root.Q<Button>("ResetPosition-Button").clicked += () => _modelMovementController.RestModelMovement();

        //On start up, the menu should be in the default closed position.
        ResetOptionsMenu();
    }    

    private void ResetOptionsMenu()
    {
        _optionsCategoryContainer.style.display = DisplayStyle.None;
        _effectCategoryContainer.style.display = DisplayStyle.None;
        _modelCategoryContainer.style.display = DisplayStyle.None;
        _dynamicOptionsListView.style.display = DisplayStyle.None;
    }

    private void OptionsButton_clicked()
    {
        if (_optionsCategoryContainer.style.display == DisplayStyle.None)
            _optionsCategoryContainer.style.display = DisplayStyle.Flex;
        else
            ResetOptionsMenu();
    }

    private void EffectsOptions_clicked()
    {
        if (_effectCategoryContainer.style.display == DisplayStyle.None)
        {
            _effectCategoryContainer.style.display = DisplayStyle.Flex;

            _modelCategoryContainer.style.display = DisplayStyle.None;
            _dynamicOptionsListView.style.display = DisplayStyle.None;
        }
    }

    private void ModelOptions_clicked()
    {
        if (_modelCategoryContainer.style.display == DisplayStyle.None)
        {
            _modelCategoryContainer.style.display = DisplayStyle.Flex;

            _effectCategoryContainer.style.display = DisplayStyle.None;
            _dynamicOptionsListView.style.display = DisplayStyle.None;
        }
    }

    private void MaterialsButton_clicked()
    {
        //Set up the List View to have all of the options for the available materials
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = _modelOptionsController.MaterialOptions[i].ButtonName;
            button.clicked += () => _modelOptionsController.SelectNewMaterial(button.text);

        };

        SetUpListView(_modelOptionsController.MaterialOptions, makeItem, bindItem);
    }

    private void MeshesButton_clicked()
    {
        //Set up the List View to have all of the options for the available meshes
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = _modelOptionsController.MeshOptions[i].ButtonName;
            button.clicked += () => _modelOptionsController.SelectNewMesh(button.text);
        };
        SetUpListView(_modelOptionsController.MeshOptions, makeItem, bindItem);
    }

    private void TexturesButton_clicked()
    {
        //Set up the List View to have all of the options for the available textures
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = _modelOptionsController.TextureOptions[i].ButtonName;
            button.clicked += () => _modelOptionsController.SelectNewTexture(button.text);
        };
        SetUpListView(_modelOptionsController.TextureOptions, makeItem, bindItem);
    }        

    void LightButton_clicked()
    {
        //Set up the List View to have all of the options for the available light effects
        Func<VisualElement> makeItem = () => new Toggle();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Toggle toggle = (e as Toggle);
            toggle.AddToClassList("toggle-style");
            toggle.text = _effectsManager.LightEffects[i].DisplayName;
            toggle.value = _effectsManager.GetLightEffectState(_effectsManager.LightEffects[i].DisplayName);
            toggle.RegisterValueChangedCallback((state) => _effectsManager.SetLightEffectState(state.newValue, _effectsManager.LightEffects[i].DisplayName));
        };

        SetUpListView(_effectsManager.LightEffects, makeItem, bindItem);
    }

    void PostProcessingButton_clicked()
    {
        //Set up the List View to have all of the options for the available post processing effects
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.AddToClassList("button-style");
            button.text = _effectsManager.PostProcessingEffects[i].DisplayName;
            button.clicked += () => _effectsManager.ActivatePostProcessingEffect(_effectsManager.PostProcessingEffects[i].DisplayName);
        };

        SetUpListView(_effectsManager.PostProcessingEffects, makeItem, bindItem);
    }

    private void SetUpListView(IList itemSource, Func<VisualElement> makeItem, Action<VisualElement, int> bindItem)
    {
        _dynamicOptionsListView.makeItem = null;
        _dynamicOptionsListView.bindItem = null;

        _dynamicOptionsListView.style.display = DisplayStyle.Flex;
        _dynamicOptionsListView.itemsSource = itemSource;
        _dynamicOptionsListView.makeItem = makeItem;
        _dynamicOptionsListView.bindItem = bindItem;
    }
}
