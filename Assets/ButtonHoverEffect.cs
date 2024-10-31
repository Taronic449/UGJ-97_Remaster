using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


    public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _originalScale;
        public float scaleFactor = 1.2f;
        public float scaleDuration = 0.2f; // Duration for the scaling animation
        public AudioSource audioSource;
        public ButtonEffectType effectType;
        public bool hoverAudio;

        private TextMeshProUGUI _textAsset;
        public string originalText;

        void Start()
        {
            
            _originalScale = transform.localScale;
            _textAsset = GetComponentInChildren<TextMeshProUGUI>();
            originalText = _textAsset.text;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (GetComponent<Button>() != null && !GetComponent<Button>().interactable)
            {
                return;
            }

            switch (effectType)
            {
                case ButtonEffectType.Expand:
                    // Use LeanTween to scale the button up smoothly
                    LeanTween.scale(gameObject, _originalScale * scaleFactor, scaleDuration).setEaseOutBack();
                    break;

                case ButtonEffectType.Programmer:
                    _textAsset.text = _textAsset.text.Substring(2);
                    _textAsset.text = "> " + _textAsset.text;
                    break;

                default:
                    Debug.Log("Choose an effect, please.");
                    break;
            }

            if (hoverAudio)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (GetComponent<Button>() != null && !GetComponent<Button>().interactable)
            {
                return;
            }
            
            switch (effectType)
            {
                case ButtonEffectType.Expand:
                    // Use LeanTween to scale the button back to its original size smoothly
                    LeanTween.scale(gameObject, _originalScale, scaleDuration).setEaseOutBack();
                    break;

                case ButtonEffectType.Programmer:
                    _textAsset.text = _textAsset.text.Substring(2);
                    _textAsset.text = "  " + _textAsset.text;
                    break;

                default:
                    Debug.Log("Choose an effect, please.");
                    break;
            }
        }

        void OnEnable()
        {
            if (_originalScale.magnitude == 0)
            {
                return;
            }

            switch (effectType)
            {
                case ButtonEffectType.Expand:
                    transform.localScale = _originalScale;  // Reset scale to the original size
                    break;

                case ButtonEffectType.Programmer:
                    _textAsset.text = originalText;
                    break;

                default:
                    Debug.Log("Choose an effect, please.");
                    break;
            }
        }
    }

    public enum ButtonEffectType
    {
        Expand,
        Programmer,
    }

