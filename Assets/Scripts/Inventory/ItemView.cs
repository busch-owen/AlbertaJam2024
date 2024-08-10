using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        public void AmountChanged(int amount)
        {
            _text.text = "" + amount;
            //Display Logic for UI
        }
    }
}