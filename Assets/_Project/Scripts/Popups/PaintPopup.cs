using System.Collections.Generic;
using ArcadeIdle.Helpers.Events;
using ArcadeIdle.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArcadeIdle.Popups
{
    public class PaintPopup : MonoBehaviour
    {
        [SerializeField] private List<PaintSO> paintSos;
        private PaintSO _paintSo;
        [SerializeField] private List<Button> paintButtons;
        [SerializeField] private List<TextMeshProUGUI> paintAmountTexts;

        [SerializeField] private Button pickButton;
        
        [SerializeField] private GameObject paintWallCarpetButton;

        
        [SerializeField] private GameEvent onPaintButtonTapped;

        private void Awake()
        {
            SetPaintAmountTexts();
            pickButton.interactable = false;
            paintWallCarpetButton.SetActive(false);
        }

        public void SetPaintAmountTexts()
        {
            for (int i = 0; i < paintSos.Count; i++)
            {
                paintAmountTexts[i].text = paintSos[i].Amount.ToString();
            }
        }

        public void ChangePaint(PaintSO p)
        {
            _paintSo = p;
            
            if (CanChoose())
            {
                pickButton.interactable = true;
            }
        }

        private void SelectPaint()
        {
            _paintSo.SelectPaint();
        }

        private bool CanChoose()
        {
            if (_paintSo == null) return false;
            if (_paintSo.Amount <= 0) return false;
            return true;
        }

        public void OnPickButtonTapped()
        {
            SelectPaint();
            paintWallCarpetButton.SetActive(true);
            pickButton.interactable = false;
            _paintSo = null;
        }

        public void PaintCarpetButtonTapped()
        {
            onPaintButtonTapped.Invoke();
        }
    }
}