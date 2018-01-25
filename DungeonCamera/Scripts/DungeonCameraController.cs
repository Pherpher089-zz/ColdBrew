using UnityEngine;

public class DungeonCameraController : MonoBehaviour {

    /// <summary>
    /// The Camera focus target transform
    /// </summary>
        [Tooltip("The object which the camera should focus and follow. If no target is assigned in the inspector and an object tagged \"Player\" exists in the scene, it will become the focus by default")]
    public Transform m_FocusTarget;

    /// <summary>
    /// The interpolation smoothing modifier. The higher the value, the quicker the interpolation
    /// </summary>
    /// TODO Edit XML Comment Template for m_MovementSmooth
        [Tooltip("The higher the value, the quicker the camera will move to the focus target position.")]
    [Range(1, 10)]public float m_MovementSmooth = 1;


    /// <summary>
    /// If true: Camera Obj will update position until target position is achieved. If false: Camera Obj will not move.
    /// </summary>
    /// TODO Edit XML Comment Template for m_CamMovement
        [Tooltip("If true: The Camera Obj will constantly try to achieve the Targets position")]
    public bool m_UpdatePosition = true;

    /// <summary>
    /// Should the camera rotate to look at the target?
    /// </summary>
        [Tooltip("If true: Camera will update rotation to \"Look\" at the target. If false: Only Camera position is updated.")]
    public bool m_UpdateDirection = false;

    /// <summary>
    /// A reference to the main camera which is also this objects child.
    /// </summary>
    /// TODO Edit XML Comment Template for m_Camera
    private Camera m_Camera;

    void Awake()
    {
        // Assigned via transform hierarchy rather than the static Camera.main variable in case 
        // multiple cameras are needed at once.
        m_Camera = GetComponentInChildren<Camera>();


        // If no other target is assigned, set focus to the player object if any
        GameObject player = GameObject.FindWithTag("Player");

        if(m_FocusTarget == null && player != null)
        {
            m_FocusTarget = player.transform;
        }

    }

    void Update()
    {
        AdjustTargetDistance();

    }

    void LateUpdate()
    {
        UpdateCameraPosition();
        if (m_UpdateDirection)
        {
            UpdateCameraLookDirection();
        }
    }

    private void UpdateCameraPosition()
    {
        if (transform.position != m_FocusTarget.position)
        {
            transform.position = Vector3.Lerp(transform.position, m_FocusTarget.position, Time.deltaTime * m_MovementSmooth);
        }
    }

    private void AdjustTargetDistance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Camera.transform.position += m_Camera.transform.forward;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Camera.transform.position -= m_Camera.transform.forward;
        }
    }

    private void UpdateCameraLookDirection()
    {
        Quaternion dir = Quaternion.LookRotation(m_FocusTarget.transform.position - m_Camera.transform.position);

        m_Camera.transform.rotation = dir;
    }
}
