using UnityEngine;

namespace DefaultNamespace
{
    public class ItemController : MonoBehaviour
    {
        [field:SerializeField]
        public ItemModel Model { get; private set; }
        [field:SerializeField]
        public ItemView View { get; set; }

        void Awake()
        {
            Model.AmountChangeEvent += (val) =>
            {
                View.AmountChanged(val);
            };


        }
    }
}