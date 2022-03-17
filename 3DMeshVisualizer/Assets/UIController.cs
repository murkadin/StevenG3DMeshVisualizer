using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles the UI elements and their behaviors
/// </summary>
public class UIController : MonoBehaviour
{
    public ModelMovementController movementController;
    public ModelOptionsController modelOptionsController;

    public Button optionsButton;
    public Button addLightButton;
    public Button addCameraButton;

    public Button translateModelButton;
    public Button rotateModelButton;
    public Button scaleModelButton;

    public VisualElement optionsContainer;
    public VisualElement effectContainer;

    public ListView modelOptionsListView;


    void Start()
    {
        //Store references to UI objects
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        addLightButton = root.Q<Button>("AddLight");
        addCameraButton = root.Q<Button>("AddCamera");
        optionsButton = root.Q<Button>("Options-Button");
        translateModelButton = root.Q<Button>("TranslationModel-Button");
        rotateModelButton = root.Q<Button>("RotateModel-Button");
        scaleModelButton = root.Q<Button>("ScaleModel-Button");        

        optionsContainer = root.Q<VisualElement>("OptionsContainer");
        effectContainer = root.Q<VisualElement>("EffectContainer");

        modelOptionsListView = root.Q<ListView>("ModelOptions-ListView");

        //Setup press events for buttons
        optionsButton.clicked += OptionsButton_clicked;
        addLightButton.clicked += AddLightButtonPressed;
        addCameraButton.clicked += AddCameraButtonPressed;

        translateModelButton.clicked += TranslateModelButton_clicked;
        rotateModelButton.clicked += RotateModelButton_clicked;
        scaleModelButton.clicked += ScaleModelButton_clicked;

        root.Q<Button>("Textures-Button").clicked += TexturesButton_clicked;
        root.Q<Button>("Meshes-Button").clicked += MeshesButton_clicked;
        root.Q<Button>("Materials-Button").clicked += MaterialsButton_clicked;
    }

    private void MaterialsButton_clicked()
    {
        //Set up the List View to have all of the options for the available materials
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.text = modelOptionsController.materialOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewMaterial(button.text);

        };
        modelOptionsListView.makeItem = null;
        modelOptionsListView.bindItem = null;

        modelOptionsListView.itemsSource = modelOptionsController.materialOptions;
        modelOptionsListView.makeItem = makeItem;
        modelOptionsListView.bindItem = bindItem;
    }

    private void MeshesButton_clicked()
    {
        //Set up the List View to have all of the options for the available meshes
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.text = modelOptionsController.meshOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewMesh(button.text);
        };
        modelOptionsListView.makeItem = null;
        modelOptionsListView.bindItem = null;

        modelOptionsListView.itemsSource = modelOptionsController.meshOptions;
        modelOptionsListView.makeItem = makeItem;
        modelOptionsListView.bindItem = bindItem;
    }

    private void TexturesButton_clicked()
    {
        //Set up the List View to have all of the options for the available textures
        Func<VisualElement> makeItem = () => new Button();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            Button button = (e as Button);
            button.text = modelOptionsController.textureOptions[i].buttonName;
            button.clicked += () => modelOptionsController.SelectNewTexture(button.text);
        };
        modelOptionsListView.makeItem = null;
        modelOptionsListView.bindItem = null;

        modelOptionsListView.itemsSource = modelOptionsController.textureOptions;
        modelOptionsListView.makeItem = makeItem;
        modelOptionsListView.bindItem = bindItem;
    }

    private void ScaleModelButton_clicked()
    {
        movementController.SetMovementType(ModelMovementController.MovementType.Scale);
    }

    private void RotateModelButton_clicked()
    {
        movementController.SetMovementType(ModelMovementController.MovementType.Rotate);
    }

    private void TranslateModelButton_clicked()
    {
        movementController.SetMovementType(ModelMovementController.MovementType.Translate);
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
