using ArcadeIdle.Helpers.Events;
using ArcadeIdle.ScriptableObjects;
using DG.Tweening;
using UnityEngine;

namespace ArcadeIdle.Popups
{
    [RequireComponent(typeof(Canvas))]
    public class PopupController : MonoBehaviour
    {
        private Canvas _canvas;
        
        [SerializeField] private GameEvent onPopupShownStart;
        [SerializeField] private GameEvent onPopupShownEnd;
        
        [SerializeField] private GameStateSO gameState;
        
        [SerializeField] private RectTransform panel;
        private readonly Vector3 _minimizedPanelScale = Vector3.one * 0.01f;
        private readonly Vector3 _normalPanelScale = Vector3.one;
        [SerializeField] private float animationDuration = 0.2f;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void OpenPopupWithAnimation()
        {
            if (_canvas.enabled)
                return;
            _canvas.enabled = true;
            
            gameState.GameState = GameState.Pause;
            onPopupShownStart.Invoke();

            panel.localScale = _minimizedPanelScale;
            panel.DOScale(_normalPanelScale, animationDuration).SetUpdate(true);

        }
        
        public void ClosePopupWithAnimation()
        {
            panel.DOScale(_minimizedPanelScale, animationDuration).SetUpdate(true).OnComplete(() =>
            {
                _canvas.enabled = false;
                gameState.GameState = GameState.Playing;
                panel.localScale = _normalPanelScale;
                onPopupShownEnd.Invoke();
            });
        }
    }
}