using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/*namespace Com.MyCompany.MyGame
{*/
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        /*[Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;*/

        private PlayerNetworking target; // player networking

        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

        float characterHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        private Transform height;

        #endregion


        #region MonoBehaviour Callbacks

        void Update()
        {
            // Reflect the Player Health

            /*if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }*/

            // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network

            if (target == null)
            {
                Destroy(this.gameObject);

                return;
            }
        }

        void Awake()
        {
            this.transform.SetParent(GameObject.Find("MainCanvas").GetComponent<Transform>(), false);

            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        void LateUpdate()
        {
            // Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.

            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            // #Critical
            // Follow the Target GameObject on screen.

            if (targetTransform != null)
            {
            Camera playerCamera = target.transform.Find("Player Camera").GetComponent<Camera>();
            //Debug.Log(characterHeight);
                targetPosition = targetTransform.position;
                targetPosition.y += characterHeight;
            //Debug.Log(playerCamera.ViewportToScreenPoint(targetPosition));
            /*Debug.Log(target.name);
            Debug.Log(this.name);
            Debug.Log(targetPosition);
            Debug.Log(target.transform.position);*/
                this.transform.position = /*Camera..WorldToScreenPoint(targetPosition)*/ playerCamera.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }

        #endregion


        #region Public Methods

        public void SetTarget(PlayerNetworking _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayNetworking target for PlayerUI.SetTarget.", this);

                return;
            }

            // Cache references for efficiency

            target = _target;
        //targetTransform = this.GetComponent<Transform>();
        targetTransform = target.transform;
        //targetRenderer = this.GetComponent<Renderer>();
        targetRenderer = target.GetComponent<SpriteRenderer>();
            //CharacterController characterController = _target.GetComponent<CharacterController>(); // ceiling check
            height = _target.transform.Find("Ceiling Check");

            // Get data from the Player that won't change during the lifetime of this Component

            if (height != null)
            {
                characterHeight = height.position.y;
            }

            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }

        #endregion
    }
//}
