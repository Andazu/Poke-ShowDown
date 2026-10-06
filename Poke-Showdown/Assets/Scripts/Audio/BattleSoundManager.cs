using System;
using Battles;
using UnityEngine;

namespace Audio
{
    public class BattleSoundManager:MonoBehaviour
    {
        public static BattleSoundManager Instance { get; private set; }
        
        
        [SerializeField] private AudioClip[] effectivenessSounds;
        [SerializeField] private AudioClip[] bgms;
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            bgmSource.clip = bgms[0];
            bgmSource.loop = true;
            bgmSource.Play();
        }

        private void PlayEffectivenessSound(DamageCalculator.Effectiveness effectiveness)
        {
            if (effectiveness != DamageCalculator.Effectiveness.NoEffect)
            {
                sfxSource.PlayOneShot(effectivenessSounds[(int)effectiveness]);
            }
        }

        private void ChangeBgm(int bgmIndex)
        {
            bgmSource.clip = bgms[0];
            bgmSource.Play();
        }
    }
}