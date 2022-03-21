using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the movement of the model when the user clicks and drags the model.
/// </summary>
public class ModelMovementController : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private float _rotationRate = 0.1f;

    [SerializeField]
    private float _scaleRate = 0.01f;

    private MovementType _movementType;
    private Vector3 _mousePrevPosition;
    private Vector3 _postionOffset;

    /// <summary>
    /// The different movement types supported.
    /// </summary>
    enum MovementType
    {
        Rotate,
        Translate,
        Scale
    }

    /// <summary>
    /// Resets the models Scale, Position and Rotation.
    /// </summary>
    public void RestModelMovement()
    {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Is called via <see cref="IDragHandler"/> when the mouse is dragged after clicking on the mesh.
    /// </summary>
    /// <param name="eventData">Mouse data.</param>
    public void OnDrag(PointerEventData eventData)
    {
        HandleMovement();
    }

    /// <summary>
    /// Is called via <see cref="IPointerDownHandler"/> when the mouse is clicked on the mesh.
    /// </summary>
    /// <param name="eventData">Mouse info.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            _movementType = MovementType.Rotate;
        else if (eventData.button == PointerEventData.InputButton.Right)
            _movementType = MovementType.Scale;
        else if (eventData.button == PointerEventData.InputButton.Middle)
            _movementType = MovementType.Translate;
        SetInitialMovementData();
    }

    private void SetInitialMovementData()
    {
        _mousePrevPosition = Input.mousePosition;

        //Store the difference between where object currently is and the mouse.
        _postionOffset = gameObject.transform.position - GetMouseToWorldPoint();
    }

    /// <summary>
    /// Takes the mouse position and uses the model's z position to calculate the world position.
    /// </summary>
    /// <returns>The mouse position in world space.</returns>
    private Vector3 GetMouseToWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    /// <summary>
    /// Handles the movement of the object based on the <see cref="_movementType"/>.
    /// </summary>
    private void HandleMovement()
    {
        switch(_movementType)
        {
            case MovementType.Translate:
                {
                    //Add the postionOffset to the mouse position so the model does not snap to the mouse if clicked outside of the model's pivot point 
                    transform.position = GetMouseToWorldPoint() + _postionOffset;
                }
                break;
            case MovementType.Rotate:
                {
                    Vector3 delta = Input.mousePosition - _mousePrevPosition;
                    _mousePrevPosition = Input.mousePosition;

                    Vector3 rotAxis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
                    transform.rotation = Quaternion.AngleAxis(delta.magnitude * _rotationRate, rotAxis) * transform.rotation;
                }
                break;
            case MovementType.Scale:
                {
                    Vector3 delta = Input.mousePosition - _mousePrevPosition;
                    _mousePrevPosition = Input.mousePosition;
                    Vector3 newScale = transform.localScale + Vector3.one * delta.y * _scaleRate;

                    //Clamp the scaling so that the user can't make the object so small they can't see it or too big as to take up the whole screen.
                    newScale.x = Mathf.Clamp(newScale.x, 0.3f, 2.0f);
                    newScale.y = Mathf.Clamp(newScale.y, 0.3f, 2.0f);
                    newScale.z = Mathf.Clamp(newScale.z, 0.3f, 2.0f);
                    transform.localScale = newScale;
                }
                break;
        }
    }    
}
