using System;
using System.Collections;
using System.Linq;
using PokemonData;
using PokeStatus;
using Sprites;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Battles
{
    public class Battle : MonoBehaviour
    {
        // Start is called before the first frame update
        [FormerlySerializedAs("P1")] [SerializeField] private PokeStatusTeam p1;
        [FormerlySerializedAs("P2")] [SerializeField] private PokeStatusTeam p2;

        HealthBar _p1HealthBarSource;
        HealthBar _p2HealthBarSource;

        PokeSprite _p1Sprite;
        PokeSprite _p2Sprite;

        [Header("Audio")]
        [SerializeField] private AudioSource[] effectivenessSound;
        [SerializeField] private AudioSource mainBGM;
        [SerializeField] private AudioSource victoryBGM;
        [FormerlySerializedAs("lowHP")] [SerializeField] private AudioSource lowHp;
        [SerializeField] private AudioSource youLost;
    
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private SelectBattleDialogue battleDialogue;

        private int _indexVarP1;//onClick Variable
        private int _indexVarP2; //onClick Variable
        private float _actionCounterP1;
        private float _actionCounterP2;
        private int _damageResultP1;
        private int _damageResultP2;
        private float _p2HealthbarVal;

        private bool _p1Turn = true;
        private bool _p2Turn;

        private string _p1Name;
        private string _p2Name;

        private bool _p1Alive = true;
        private bool _p2Alive = true;
        private int _p1Input;

        private bool _superEffectiveSound;
        private bool _notEffectiveSound;
        private bool _normalEffectSound;
        private bool _noEffectSound;

        private bool _superEffective;
        private bool _notEffective;
        private bool _normalEffect = false;
        private bool _noEffect;

        private bool _critHit;

        private bool _moveMissP1;
        private bool _moveMissP2;
        private bool _canChooseMove = true;
        private float _currentHealth;
        private int _enemyRandomMove;
        
        private void Start()
        {
            mainBGM.Play();
        
            _p1Name = p1.text.text;
            _p2Name = p2.text.text;
            
            _p1Sprite = p1.sprite;
            _p2Sprite = p2.sprite;

            _p1HealthBarSource = p1.healthBar;
            _p2HealthBarSource = p2.healthBar;

            battleDialogue.WhatWillPokeDo(_p1Name);
            _currentHealth = p1.statsGlobal[0];
            
            battleDialogue.InitMoveMenu(this,p1.moveSource);
        }


        private void Update()
        {
            if (_superEffectiveSound)
            {
                effectivenessSound[1].Play();
                _superEffective = true;
                _superEffectiveSound = false;
            }
            else if (_notEffectiveSound)
            {
                effectivenessSound[2].Play();
                _notEffective = true;
                _notEffectiveSound = false;
            }
            else if (_normalEffectSound)
            {
                effectivenessSound[0].Play();
                _normalEffect = true;
                _normalEffectSound = false;
            }

            else if (_noEffectSound)
            {
                _noEffect = true;
                _noEffectSound = false;
            }

            //TODO: Fix move miss and no effect
            if (_noEffectSound && !_p2Turn || _moveMissP1 && !_p2Turn)
            {
                Debug.Log("NO EFFECT");
            
                StartCoroutine(EnemyMove());
                _moveMissP1 = false;

            }
            if(_noEffectSound && !_p1Turn || _moveMissP2 && !_p1Turn)
            {
                Debug.Log("NO EFFECT");
            
                StartCoroutine(P1Move());
                _moveMissP2 = false;
            }


            if (_actionCounterP1 < _damageResultP1 && _p1HealthBarSource.slider.value != 0)
            {
                _p2HealthBarSource.SetHealth(_p2HealthBarSource.slider.value - 0.5f);
                _actionCounterP1 += 0.5f;
                if(_actionCounterP1 >= _damageResultP1)
                {
                    _actionCounterP1 = 0;
                    _damageResultP1 = 0;

                    if(_p2HealthBarSource.slider.value != 0 && !_p2Turn)
                    {
                        StartCoroutine(EnemyMove());
                    }
                }
            }

            if (_actionCounterP2 < _damageResultP2)
            {
            
                _p1HealthBarSource.SetHealth(_p1HealthBarSource.slider.value - 0.5f);
                _actionCounterP2 += 0.5f;
                if(_actionCounterP2 >= _damageResultP2)
                {
                    _actionCounterP2 = 0;
                    _damageResultP2 = 0;
                    if(_p1HealthBarSource.slider.value != 0 && !_p1Turn)
                    {
                        StartCoroutine(P1Move());
                    }
                }
            }
            
            if (_p2HealthBarSource.slider.value == 0 && _p2Alive)
            {
                _p2Alive = false;
                StartCoroutine(HandleFaint(false));
            }

            if(_p1HealthBarSource.slider.value == 0 && _p1Alive)
            {
                _p1Alive = false;
                StartCoroutine(HandleFaint(true));
            }
        }
        
        void EffectivenessText()
        {
            if (_superEffective)
            {
                dialogueText.text = "It's super effective!";
                _superEffective = false;
            }
            else if (_notEffective)
            {
                dialogueText.text = "It's not very effective..."; 
                _notEffective = false;
            }
            else if (_noEffect)
            {
                dialogueText.text = "It has no effect";
                _noEffect = false;
            }
            else if (_moveMissP1) 
            {
                dialogueText.text = p1.text.text + " missed!";
                _moveMissP1 = false;
            }
            else if (_moveMissP2)
            {
                dialogueText.text = "The foe's " + p2.text.text + " missed!";
                _moveMissP2 = false;
            }
            else
            {
                _normalEffect = false;
            }
        }

        private IEnumerator CritHitText()
        {
            yield return new WaitForSeconds(1f);
            dialogueText.text = "A critical hit!";
            _critHit = false;
        }

        private void P1DialogueText()
        {
            battleDialogue.WhatWillPokeDo(_p1Name);
            _canChooseMove = true;
        }

        IEnumerator HandleFaint(bool humanPlayer)
        {
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }
            EffectivenessText();
            yield return new WaitForSeconds(1f);
            dialogueText.text = "";
            dialogueText.text = humanPlayer? _p1Name + " fainted!": "The foe's " + _p2Name + " fainted!";
            if (humanPlayer)
            {
                p1.text.text = "";
                _p1Sprite.gameObject.SetActive(false);
                _p1HealthBarSource.gameObject.SetActive(false);
            }
            else
            {
                p2.text.text = "";
                _p2Sprite.gameObject.SetActive(false);
                _p2HealthBarSource.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1f);
            mainBGM.FadeOut(2f);
            if (humanPlayer)
            {
                lowHp.FadeOut(1f);
                SetLossText();
            }
            else
            {
                SetVictoryText();
            }
        }

        public void SetVictoryText()
        {
            dialogueText.text = "You defeated the enemy trainer!";
            victoryBGM.Play();
        }

        public void SetLossText()
        {
            dialogueText.text = "You lost";
            youLost.Play(); //change later lol
        }

        private IEnumerator P1Move()
        {
            _p1Turn = true;
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }

            EffectivenessText();
            yield return new WaitForSeconds(1f);
            PlayerChooseMove(_p1Input,true);
            if (_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }
            yield return new WaitForSeconds(0.5f);
            EffectivenessText();
            yield return new WaitForSeconds(1f);
            if(_p2Alive)
            {
                P1DialogueText();
            }
        }

        private IEnumerator EnemyMove()
        {
            _p2Turn = true;
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            } 
            EffectivenessText();
            yield return new WaitForSeconds(1f);
            _enemyRandomMove = Random.Range(0, 3);
            PlayerChooseMove(_enemyRandomMove,false);
            if (_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }
            yield return new WaitForSeconds(0.5f);
            EffectivenessText();
            yield return new WaitForSeconds(1f);
            if(_p1Alive)
            {
                P1DialogueText();
            }
        }
        
        public void ChooseMove(int input)
        {
            if (_canChooseMove && _p2HealthBarSource.slider.value != 0 && _p1HealthBarSource.slider.value != 0)
            {
                _enemyRandomMove = Random.Range(0, 3);
                _p1Input = input;

                if (p1.statsGlobal[5] > p2.statsGlobal[5])
                {
                    _p1Turn = true;
                    _p2Turn = false;
                    PlayerChooseMove(input,true);
                }
                else if (p1.statsGlobal[5] < p2.statsGlobal[5])
                {
                    _p2Turn = true;
                    _p1Turn = false;
                    PlayerChooseMove(_enemyRandomMove,false);
                }
                else
                {
                    int speedTie = Random.Range(0, 1);
                    if (speedTie == 0)
                    {
                        _p1Turn = true;
                        _p2Turn = false;
                        PlayerChooseMove(input,true);
                    }
                    else
                    {
                        _p2Turn = true;
                        _p1Turn = false;
                        PlayerChooseMove(_enemyRandomMove,false);
                    }
                }
                _canChooseMove = false;

            }
        }

        private void HandeEffectivenessSound(DamageCalculator.Effectiveness effectiveness)
        {
            switch (effectiveness)
            {
                case DamageCalculator.Effectiveness.SuperEffective:
                    _superEffectiveSound = true;
                    break;
                case DamageCalculator.Effectiveness.NoEffect:
                    _noEffectSound = true;
                    break;
                case DamageCalculator.Effectiveness.NotEffective:
                    _notEffectiveSound = true;
                    break;
                case DamageCalculator.Effectiveness.Regular:
                    _normalEffectSound = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effectiveness), effectiveness, null);
            }
        }

        private void PlayerChooseMove(int input, bool isP1)
        {
            var team = isP1 ? p1 : p2;
            
            var accRandom = Random.Range(1, 100);
            //accuracy
            if (accRandom > team.moveSource.moveSet[input].accuracy) 
            {
                if (isP1)
                {
                    _moveMissP1 = true;
                }
                else
                {
                    _moveMissP2 = true;
                }
                return;
            }
            //check for speed here
            var moveCap = char.ToUpper(team.moveSource.moveSet[input].name[0]) + team.moveSource.moveSet[input].name.Substring(1);
            dialogueText.text = isP1? p1.text.text + " used " + moveCap + "!" : "The foe's " + p2.text.text + " used " + moveCap + "!";

            var unParsed = team.moveSource.moveSet[input];
            Enum.TryParse(unParsed.type.name, true, out PokemonData.Type type);
            Enum.TryParse(unParsed.damage_class.name, true, out MoveClass damageClass);

            var move = new MonMove
            {
                power = unParsed.power.Value,
                type = type,
                moveClass = damageClass
            };
            var attackerStats = p1.statsGlobal.Select(s => Pokemon.CalculateOtherStat(s, 50)).ToArray();
            var attacker = new Pokemon
            {
                stats = attackerStats,
                level = 50,
                type1 = p1.typeGlobal[0],
                type2 = p1.typeGlobal[1],
            };
            
            var defenderStats = p2.statsGlobal.Select(s => Pokemon.CalculateOtherStat(s, 50)).ToArray();
            var defender = new Pokemon
            {
                stats = defenderStats,
                level = 50,
                type1 = p2.typeGlobal[0],
                type2 = p2.typeGlobal[1],
            };

            var (dmg, effectiveness) = DamageCalculator.CalculateDamage(attacker, defender, move);
            HandeEffectivenessSound(effectiveness);

            if (!isP1)
            {
                float targetHealth = p1.statsGlobal[0];
                _currentHealth -= dmg;
                if (_currentHealth / targetHealth <= 0.160 && _currentHealth / targetHealth > 0) //normalize percent value?
                {
                    mainBGM.Stop();
                    lowHp.Play();
                }
                _damageResultP2 = dmg;
            }
            else
            {
                _damageResultP1 = dmg;
            }

        }
    }
}
