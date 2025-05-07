using System;
using UnityEngine;
using System.Collections; // Required for Coroutines
using Random = UnityEngine.Random;

// Removed namespace UnityStandardAssets.Characters.FirstPerson

#pragma warning disable 618, 649 // Preserving original pragmas, consider addressing warnings in your project

// --- Helper Class: SimpleMouseLook (Replaces UnityStandardAssets.Characters.FirstPerson.MouseLook) ---
[System.Serializable]
public class SimpleMouseLook
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Transform m_CharacterTransform;
    private Transform m_CameraTransform;

    public void Init(Transform character, Transform camera)
    {
        m_CharacterTransform = character;
        m_CameraTransform = camera;
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void LookRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        if (smooth)
        {
            m_CharacterTransform.localRotation = Quaternion.Slerp(m_CharacterTransform.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            m_CameraTransform.localRotation = Quaternion.Slerp(m_CameraTransform.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            m_CharacterTransform.localRotation = m_CharacterTargetRot;
            m_CameraTransform.localRotation = m_CameraTargetRot;
        }

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Or any other button to re-lock
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}


// --- Helper Class: SimpleFOVKick (Replaces UnityStandardAssets.Utility.FOVKick) ---
[System.Serializable]
public class SimpleFOVKick
{
    public Camera Camera;                           // The camera to FOV kick
    public float originalFov;                       // The original FOV of the camera
    [SerializeField] private float FOVIncrease = 10f;         // The amount the FOV increases by
    [SerializeField] private float TimeToIncrease = 0.2f;       // The time it takes to kick in
    [SerializeField] private float TimeToDecrease = 0.2f;       // The time it takes to kick back
    private Coroutine m_RunningCoroutine;

    public void Setup(Camera camera)
    {
        Camera = camera;
        if (Camera != null)
        {
            originalFov = camera.fieldOfView;
        }
    }

    public void ChangeCamera(Camera camera)
    {
        Camera = camera;
    }

    public IEnumerator FOVKickUp()
    {
        if (Camera == null) yield break;
        float t = 0f;
        float currentFOV = Camera.fieldOfView; // Use current FOV as starting point
        float targetFOV = originalFov + FOVIncrease;
        while (t < TimeToIncrease)
        {
            if (Camera == null) yield break; // Camera might be destroyed
            Camera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, t / TimeToIncrease);
            t += Time.deltaTime;
            yield return null;
        }
        if (Camera != null) Camera.fieldOfView = targetFOV;
    }

    public IEnumerator FOVKickDown()
    {
        if (Camera == null) yield break;
        float t = 0f;
        float currentFOV = Camera.fieldOfView; // Use current FOV as starting point
        float targetFOV = originalFov;
        while (t < TimeToDecrease)
        {
            if (Camera == null) yield break; // Camera might be destroyed
            Camera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, t / TimeToDecrease);
            t += Time.deltaTime;
            yield return null;
        }
        if (Camera != null) Camera.fieldOfView = targetFOV;
    }

    // Helper to start coroutines from a non-MonoBehaviour class, requires a MonoBehaviour instance
    public void StartFOVKickUp(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null || Camera == null) return;
        if (m_RunningCoroutine != null) monoBehaviour.StopCoroutine(m_RunningCoroutine);
        m_RunningCoroutine = monoBehaviour.StartCoroutine(FOVKickUp());
    }
    public void StartFOVKickDown(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null || Camera == null) return;
        if (m_RunningCoroutine != null) monoBehaviour.StopCoroutine(m_RunningCoroutine);
        m_RunningCoroutine = monoBehaviour.StartCoroutine(FOVKickDown());
    }
    public void StopAllFOVKickCoroutines(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null) return;
        if (m_RunningCoroutine != null) monoBehaviour.StopCoroutine(m_RunningCoroutine);
        m_RunningCoroutine = null; // Clear the reference
    }
}


// --- Helper Class: SimpleHeadBob (Replaces UnityStandardAssets.Utility.CurveControlledBob & parts of LerpControlledBob) ---
[System.Serializable]
public class SimpleHeadBob
{
    public Transform Head;                          // The camera transform (or a parent 'head' transform)
    public float BobbingSpeed = 0.18f;              // Walking bobbing speed
    public float BobbingAmount = 0.05f;             // Walking bobbing amount
    public float RunningBobbingSpeedMultiplier = 1.5f; // How much faster to bob when running
    public float RunningBobbingAmountMultiplier = 1.5f; // How much more to bob when running

    public float JumpBobAmount = 0.3f;              // Amount camera moves down/up during jump
    public float JumpBobDuration = 0.3f;            // Duration of the jump bob
    public float LandBobAmount = 0.4f;              // Amount camera moves down then up upon landing
    public float LandBobDuration = 0.4f;            // Duration of the landing bob

    [HideInInspector] public Vector3 m_OriginalLocalPos; // Made public for FPC to access, but hidden from inspector
    private float m_Timer = 0f;
    private float m_JumpBobOffset = 0f; // This stores the current Y offset from jump/land bobs
    private Coroutine m_RunningBobCoroutine; // Stores the currently active jump or land bob coroutine


    public void Setup(Transform head)
    {
        Head = head;
        if (Head != null)
        {
            m_OriginalLocalPos = Head.localPosition;
        }
        else
        {
            Debug.LogError("SimpleHeadBob: Head transform is null in Setup.");
        }
    }

    // This method applies the walking/running bob directly to the Head's localPosition.
    public void DoHeadBob(float characterVelocityMagnitude, bool isWalking)
    {
        if (Head == null) return;

        float speed = isWalking ? BobbingSpeed : BobbingSpeed * RunningBobbingSpeedMultiplier;
        float amount = isWalking ? BobbingAmount : BobbingAmount * RunningBobbingAmountMultiplier;

        // Calculate walking/running bob components
        float waveSlice = 0f;
        float horizontalBob = 0f;

        if (characterVelocityMagnitude > 0.1f) // Only bob if moving
        {
            m_Timer += Time.deltaTime * speed;
            waveSlice = Mathf.Sin(m_Timer); // Vertical bob
            horizontalBob = Mathf.Cos(m_Timer * 0.5f); // Slower horizontal bob

            Vector3 currentPos = m_OriginalLocalPos; // Start from original position
            currentPos.y += waveSlice * amount;
            currentPos.x += horizontalBob * amount * 0.5f; // Less horizontal bob
            Head.localPosition = currentPos;
        }
        else
        {
            m_Timer = 0; // Reset timer
            // Smoothly return to original local position when not moving
            Head.localPosition = Vector3.Lerp(Head.localPosition, m_OriginalLocalPos, Time.deltaTime * speed * 5f);
        }
    }

    // Returns an IEnumerator to be started by the FirstPersonController (MonoBehaviour)
    public IEnumerator DoJumpBob()
    {
        if (Head == null) yield break;
        // FirstPersonController should handle stopping existing coroutine if necessary.
        yield return PerformBob(JumpBobAmount, JumpBobDuration, true);
    }

    // Returns an IEnumerator to be started by the FirstPersonController (MonoBehaviour)
    public IEnumerator DoLandBob()
    {
        if (Head == null) yield break;
        // FirstPersonController should handle stopping existing coroutine if necessary.
        yield return PerformBob(LandBobAmount, LandBobDuration, false);
    }

    private IEnumerator PerformBob(float amount, float duration, bool jumpBob) // True if jump, false if land
    {
        if (Head == null) yield break;
        float startTime = Time.time;
        // amount is the peak displacement.
        // For jump, it's a quick dip then return. For land, it's a more pronounced dip.

        float initialYOffset = m_JumpBobOffset; // Capture current offset if any (e.g., if already in a bob)

        // Phase 1: Move Down
        float bobDownDuration = jumpBob ? duration * 0.4f : duration * 0.5f; // Time to reach peak downward displacement
        float peakDisplacement = -amount; // Negative for downward movement

        float journeyTimer = 0f;
        while (journeyTimer < bobDownDuration)
        {
            if (Head == null) yield break;
            journeyTimer += Time.deltaTime;
            float fraction = journeyTimer / bobDownDuration;
            // Using a sinusoidal curve for smooth start and end of this phase
            m_JumpBobOffset = initialYOffset + Mathf.Lerp(0, peakDisplacement, Mathf.Sin(fraction * Mathf.PI * 0.5f));
            yield return null;
        }

        // Phase 2: Move Up (back to initialYOffset)
        float bobUpDuration = duration - bobDownDuration;
        float startYOffsetForUpPhase = m_JumpBobOffset; // Should be peakDisplacement relative to initialYOffset

        journeyTimer = 0f;
        while (journeyTimer < bobUpDuration)
        {
            if (Head == null) yield break;
            journeyTimer += Time.deltaTime;
            float fraction = journeyTimer / bobUpDuration;
            // Using a sinusoidal curve for smooth start and end of this phase
            m_JumpBobOffset = startYOffsetForUpPhase + Mathf.Lerp(0, -peakDisplacement, Mathf.Sin(fraction * Mathf.PI * 0.5f)); // -peakDisplacement to move upwards
            yield return null;
        }
        m_JumpBobOffset = initialYOffset; // Ensure it ends precisely at the starting offset of this particular bob
        // m_RunningBobCoroutine should be set to null by the caller (FirstPersonController)
    }

    public float GetJumpBobOffset()
    {
        return m_JumpBobOffset;
    }

    // Coroutine management (start/stop) should be handled by the MonoBehaviour (FirstPersonController)
    // These stubs are reminders and were causing Debug.LogError previously.
    // The FirstPersonController now correctly calls StartCoroutine on the IEnumerators returned by DoJumpBob/DoLandBob.
}


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed = 5f;
    [SerializeField] private float m_RunSpeed = 10f;
    [SerializeField][Range(0f, 1f)] private float m_RunStepLengthen = 0.7f;
    [SerializeField] private float m_JumpSpeed = 8f;
    [SerializeField] private float m_StickToGroundForce = 10f;
    [SerializeField] private float m_GravityMultiplier = 2f;
    [Tooltip("Determines how high the character can step up. Adjust based on your stair height.")]
    [SerializeField] private float m_StepOffset = 0.4f; // Added for stair stepping

    [Header("Mouse Look")]
    [SerializeField] private SimpleMouseLook m_MouseLook = new SimpleMouseLook();

    [Header("FOV Kick")]
    [SerializeField] private bool m_UseFovKick = true;
    [SerializeField] private SimpleFOVKick m_FovKick = new SimpleFOVKick();

    [Header("Head Bob")]
    [SerializeField] private bool m_UseHeadBob = true;
    [SerializeField] private SimpleHeadBob m_HeadBob = new SimpleHeadBob();

    [Header("Audio")]
    [SerializeField] private float m_StepInterval = 0.5f;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private AudioClip m_JumpSound;
    [SerializeField] private AudioClip m_LandSound;

    // Internal state variables
    private Camera m_Camera;
    private bool m_Jump;
    private Vector2 m_Input;
    private Vector3 m_MoveDir = Vector3.zero;
    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    private bool m_PreviouslyGrounded;
    // m_OriginalCameraPosition is now managed by SimpleHeadBob as m_OriginalLocalPos
    private float m_StepCycle;
    private float m_NextStep;
    private bool m_Jumping; // True if player is currently in the air from a jump
    private AudioSource m_AudioSource;
    private Coroutine m_ActiveBobCoroutine; // For managing jump/land bob coroutines


    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        if (m_CharacterController == null)
        {
            Debug.LogError("CharacterController component not found on this GameObject.", this);
            enabled = false;
            return;
        }
        // Apply step offset for stair climbing
        m_CharacterController.stepOffset = m_StepOffset;


        m_Camera = Camera.main;
        if (m_Camera == null)
        {
            Debug.LogError("Main Camera not found. Please tag your main camera with 'MainCamera'. Disabling controller.", this);
            enabled = false;
            return;
        }

        // Setup helper classes
        m_MouseLook.Init(transform, m_Camera.transform);
        if (m_UseFovKick) m_FovKick.Setup(m_Camera);
        if (m_UseHeadBob) m_HeadBob.Setup(m_Camera.transform);


        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_Jumping = false;
        m_AudioSource = GetComponent<AudioSource>();
        if (m_AudioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on this GameObject. Sounds will not play.", this);
        }
    }


    private void Update()
    {
        if (m_Camera == null || m_CharacterController == null) return;

        RotateView();

        // Read jump input
        if (!m_Jump && m_CharacterController.isGrounded) // Only allow jump input if grounded
        {
            m_Jump = Input.GetButtonDown("Jump");
        }

        // Handle landing
        if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
        {
            if (m_Jumping) // Only trigger landing effects if we were actually jumping
            {
                if (m_UseHeadBob && m_HeadBob.Head != null)
                {
                    if (m_ActiveBobCoroutine != null) StopCoroutine(m_ActiveBobCoroutine);
                    m_ActiveBobCoroutine = StartCoroutine(m_HeadBob.DoLandBob());
                }
                PlayLandingSound();
            }
            m_MoveDir.y = 0f; // Stop vertical movement upon landing
            m_Jumping = false;
        }

        // Handle falling off edges (not a jump)
        if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            // Character has become ungrounded without jumping (e.g., walked off a ledge)
            // m_MoveDir.y will be handled by gravity in FixedUpdate
        }

        m_PreviouslyGrounded = m_CharacterController.isGrounded;
    }


    private void FixedUpdate()
    {
        if (m_Camera == null || m_CharacterController == null) return;

        float speed;
        GetInput(out speed);

        // Calculate desired movement direction based on camera orientation
        Vector3 desiredMoveRelative = m_Camera.transform.forward * m_Input.y + m_Camera.transform.right * m_Input.x;

        // Project desiredMoveRelative onto the XZ plane for ground movement, keep original Y for air control if needed (not typical for this controller type)
        Vector3 desiredMove = new Vector3(desiredMoveRelative.x, 0, desiredMoveRelative.z);


        // Get a normal for the surface character is standing on to move along it
        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                           m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized; // Project onto ground normal

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.z = desiredMove.z * speed;

        if (m_CharacterController.isGrounded)
        {
            m_MoveDir.y = -m_StickToGroundForce; // Apply force to stick to ground

            if (m_Jump) // m_Jump is true only for one frame due to GetButtonDown
            {
                m_MoveDir.y = m_JumpSpeed;
                PlayJumpSound();
                m_Jump = false; // Consume jump input
                m_Jumping = true; // Set jumping state
                if (m_UseHeadBob && m_HeadBob.Head != null)
                {
                    if (m_ActiveBobCoroutine != null) StopCoroutine(m_ActiveBobCoroutine);
                    m_ActiveBobCoroutine = StartCoroutine(m_HeadBob.DoJumpBob());
                }
            }
        }
        else
        {
            // Apply gravity when in air
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }

        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed); // Handles head bobbing

        // m_MouseLook.UpdateCursorLock(); // Already called in RotateView
    }

    private void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool waswalking = m_IsWalking;

#if !MOBILE_INPUT // This preprocessor directive might not be relevant if you're not using Standard Assets' mobile input setup
        m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
        speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
        m_Input = new Vector2(horizontal, vertical);

        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }

        // FOV Kick logic
        if (m_UseFovKick && m_CharacterController != null) // Added null check for safety
        {
            if (m_IsWalking != waswalking && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                // Stop any existing FOV coroutine before starting a new one
                m_FovKick.StopAllFOVKickCoroutines(this);
                if (!m_IsWalking)
                {
                    m_FovKick.StartFOVKickUp(this);
                }
                else
                {
                    m_FovKick.StartFOVKickDown(this);
                }
            }
        }
    }

    private void RotateView()
    {
        m_MouseLook.LookRotation();
    }

    private void UpdateCameraPosition(float speed)
    {
        if (m_Camera == null || !m_UseHeadBob || m_HeadBob.Head == null)
        {
            return;
        }

        // Apply walking/running bob. SimpleHeadBob.DoHeadBob modifies Head.localPosition directly.
        // It sets Head.localPosition to m_OriginalLocalPos + walkingBobEffect, or lerps to m_OriginalLocalPos if not moving.
        m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunStepLengthen)), m_IsWalking);

        // Start with the position set by DoHeadBob (which is original + walk/run bob)
        Vector3 newCameraPosition = m_HeadBob.Head.localPosition;

        // Add the jump/land bob offset. GetJumpBobOffset() is signed (negative for down).
        // This ensures the jump bob is applied relative to the walking bobbed position's Y,
        // but effectively it's OriginalLocalPos.y + walkingBob.y + jumpBob.y
        newCameraPosition.y = m_HeadBob.m_OriginalLocalPos.y + (newCameraPosition.y - m_HeadBob.m_OriginalLocalPos.y) + m_HeadBob.GetJumpBobOffset();

        // Apply the final calculated position
        m_HeadBob.Head.localPosition = newCameraPosition;
    }

    private void PlayJumpSound()
    {
        if (m_AudioSource != null && m_JumpSound != null)
        {
            m_AudioSource.PlayOneShot(m_JumpSound);
        }
    }

    private void PlayLandingSound()
    {
        if (m_AudioSource != null && m_LandSound != null)
        {
            m_AudioSource.PlayOneShot(m_LandSound);
            m_NextStep = m_StepCycle + .5f; // Reset step cycle for immediate footstep after landing if moving
        }
    }

    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunStepLengthen))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;
        PlayFootStepAudio();
    }

    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded || m_AudioSource == null || m_FootstepSounds == null || m_FootstepSounds.Length == 0)
        {
            return;
        }

        int n;
        if (m_FootstepSounds.Length == 1)
        {
            n = 0; // Play the only sound
        }
        else
        {
            // Pick a random sound, excluding the one at index 0 (which was the last one played)
            n = Random.Range(1, m_FootstepSounds.Length);
        }

        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);

        // Move the played sound to index 0 so it's not picked immediately again (if more than one sound)
        if (m_FootstepSounds.Length > 1)
        {
            AudioClip temp = m_FootstepSounds[n];
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = temp;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (m_CollisionFlags == CollisionFlags.Below) // Don't push rigidbodies if character is on top of them
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}
