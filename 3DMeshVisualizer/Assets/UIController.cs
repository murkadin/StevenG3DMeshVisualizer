using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles the UI elements and their behaviors
/// </summary>
public class UIController : MonoBehaviour
{
    public ModelMovementController movementController;

    public Button optionsButton;
    public Button addLightButton;
    public Button addCameraButton;

    public Button translateModelButton;
    public Button rotateModelButton;
    public Button scaleModelButton;

    public VisualElement optionsContainer;
    public VisualElement effectContainer;


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

        //Setup press events for buttons
        optionsButton.clicked += OptionsButton_clicked; ;
        addLightButton.clicked += AddLightButtonPressed;
        addCameraButton.clicked += AddCameraButtonPressed;

        translateModelButton.clicked += TranslateModelButton_clicked; ;
        rotateModelButton.clicked += RotateModelButton_clicked; ;
        scaleModelButton.clicked += ScaleModelButton_clicked; ;
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
