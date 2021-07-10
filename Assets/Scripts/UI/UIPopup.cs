using UnityEngine;

namespace ZombieApocalypse
{
    public abstract class UIPopup : MonoBehaviour
    {
        public bool Showed => gameObject.activeSelf;

        public void Show()
        {
            gameObject.SetActive(true);
            OnShowed();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHided();
        }

        private void OnDestroy()
        {
            if (Showed)
                OnHided();
        }

        protected virtual void OnShowed()
        {
            Time.timeScale = 0f;
        }

        protected virtual  void OnHided()
        {
            Time.timeScale = 1f;
        }
    }
}
