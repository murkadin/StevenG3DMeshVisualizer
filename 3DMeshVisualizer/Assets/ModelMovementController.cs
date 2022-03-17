using UnityEngine;

/// <summary>
/// Handles the movement of the model when the user clicks and drags the model
/// </summary>
public class ModelMovementController : MonoBehaviour
{
    /// <summary>
    /// The rate at which we rotate the object
    /// </summary>
    [SerializeField]
    protected float rotationRate = 0.1f;

    /// <summary>
    /// The rate at which we scale the object
    /// </summary>
    [SerializeField]
    protected float scaleRate = 0.01f;

    /// <summary>
    /// The 
    /// </summary>
    private MovementType movementType;

    /// <summary>
    /// The previous mouse position used for calculating the difference between the current mouse position
    /// </summary>
    private Vector3 mousePrevPosition;

    /// <summary>
    /// The offset between the mouse and the model
    /// </summary>
    private Vector3 postionOffset;

    /// <summary>
    /// The different movement types supported
    /// </summary>
    public enum MovementType
    {
        Rotate,
        Translate,
        Scale
    }

    /// <summary>
    /// Sets <see cref="movementType"/> to the value passed in.
    /// </summary>
    /// <param name="type">The type of movement the object should be doing</param>
    public void SetMovementType(MovementType type)
    {
        movementType = type;
    }

    /// <summary>
    /// Sets initial values to help with movement behavior when the <see cref="OnMouseDrag"/> occurs.
    /// </summary>
    void OnMouseDown()
    {
        mousePrevPosition = Input.mousePosition;

        //Store the difference between where object currently is and the mouse
        postionOffset = gameObject.transform.position - GetMouseToWorldPoint();
    }

    /// <summary>
    /// Takes the mouse position and uses the model's z position to calculate the world position
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseToWorldPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    /// <summary>
    /// Handles the object behavior when the mouse is dragged.
    /// </summary>
    void OnMouseDrag()
    {
        HandleMovement();
    }

    /// <summary>
    /// Handles the movement of the object based on the <see cref="movementType"/>.
    /// </summary>
    private void HandleMovement()
    {
        switch(movementType)
        {
            case MovementType.Translate:
                {
                    //Add the postionOffset to the mouse position so the model does not snap to the mouse if clicked outside of the model's pivot point 
                    transform.position = GetMouseToWorldPoint() + postionOffset;
                }
                break;
            case MovementType.Rotate:
                {
                    Vector3 delta = Input.mousePosition - mousePrevPosition;
                    mousePrevPosition = Input.mousePosition;

                    Vector3 rotAxis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
                    transform.rotation = Quaternion.AngleAxis(delta.magnitude * rotationRate, rotAxis) * transform.rotation;
                }
                break;
            case MovementType.Scale:
                {
                    Vector3 delta = Input.mousePosition - mousePrevPosition;
                    mousePrevPosition = Input.mousePosition;
                    Vector3 newScale = transform.localScale + Vector3.one * delta.y * scaleRate;
                    newScale.x = Mathf.Clamp(newScale.x, 0.3f, 2.0f);
                    newScale.y = Mathf.Clamp(newScale.y, 0.3f, 2.0f);
                    newScale.z = Mathf.Clamp(newScale.z, 0.3f, 2.0f);
                    transform.localScale = newScale;
                }
                break;
        }
    }
}
