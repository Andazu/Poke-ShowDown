using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sprites
{
    public class PokeSprite : MonoBehaviour
    {
        private Sprite[] _spriteArray;
        
        private int _randNum;
        private int _currentSprite;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetUpSprite(int rand)
        {
            AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>("Assets/PokeSprites/BACK/" + rand + ".png");
            spriteHandle.Completed += LoadSpritesWhenReady;
        }
    

        void LoadSpritesWhenReady (AsyncOperationHandle<Sprite[]> handleToCheck)  
        {
            if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
            {
                _spriteArray = handleToCheck.Result;
            }

            StartCoroutine(UpdateAnimation());
        }


        IEnumerator UpdateAnimation()
        {
        
            _spriteRenderer.sprite = _spriteArray[_currentSprite];
            _currentSprite++;
            if (_currentSprite >= _spriteArray.Length || ReferenceEquals(_spriteRenderer.sprite, null)) 
            {
                _currentSprite = 0;
                _spriteRenderer.sprite = _spriteArray[_currentSprite];
            }
            yield return new WaitForSeconds(0.1f);
        
            StartCoroutine(UpdateAnimation());
        }
    }
}