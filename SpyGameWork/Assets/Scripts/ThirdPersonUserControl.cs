using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public GameObject BulletPrefab;
        public Transform bulletSpawn;
        public float bulletSpeed;
        private Animator animator;


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
            animator = m_Character.GetComponent<Animator>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (Input.GetKeyDown("e") && GameManager.instance.isAggressive == false)
            {
                GameManager.instance.isAggressive = true;
            }

            else if (Input.GetKeyDown("e") && GameManager.instance.isAggressive == true)
            {
                GameManager.instance.isAggressive = false;
            }

            if (Input.GetKeyDown("r") && GameManager.instance.isNaughty == true)
            {
                GameManager.instance.isNaughty = false;
            }

            else if (Input.GetKeyDown("r") && GameManager.instance.isNaughty == false)
            {
                GameManager.instance.isNaughty = true;
            }


        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }
            //#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
            //#endif
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }

        void Fire()
        {
            Vector3 lookPos = new Vector3();
            RaycastHit hit;
            var layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                lookPos = hit.point;
            }
            else
            {
                lookPos = Input.mousePosition;
                //lookPos.z = defaultAimDistance;
                lookPos = Camera.main.ScreenToWorldPoint(lookPos);
            }
            var bullet = (GameObject)Instantiate(BulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.transform.LookAt(lookPos);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 5.0f);
            Debug.Log("Pew pew");
        }
    }
}
