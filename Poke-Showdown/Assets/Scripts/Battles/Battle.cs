using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PokeStatus;
using Sprites;
using TMPro;
using Typing;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battles
{
    public class Battle : MonoBehaviour
    {
        // Start is called before the first frame update
        [FormerlySerializedAs("P1")] [SerializeField] private PokeStatusTeam p1;
        [FormerlySerializedAs("P2")] [SerializeField] private PokeStatusTeam p2;

        Moves _p1MovesSource;
        Moves _p2MovesSource;

        HealthBar _p1HealthBarSource;
        HealthBar _p2HealthBarSource;

        PokeSprite _p1Sprite;
        PokeSprite _p2Sprite;

        [Header("Audio")]
        [SerializeField] private AudioSource[] effectivenessSound;
        [SerializeField] private AudioSource mainBGM;
        [SerializeField] private AudioSource victoryBGM;
        [FormerlySerializedAs("lowHP")] [SerializeField] private AudioSource lowHp;
        [SerializeField] private AudioSource YOULOST;
    
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private SelectBattleDialogue battleDialogue;

        private int _indexVarP1;//onClick Variable
        private int _indexVarP2; //onClick Variable
        private float _actionCounterP1 = 0;
        private float _actionCounterP2 = 0;
        private double _damageResultP1;
        private double _damageResultP2;
        private float _p2HealthbarVal;

        private bool _p1Turn = true;
        private bool _p2Turn = false;

        private string _p1Name;
        private string _p2Name;

        private bool _p1Alive = true;
        private bool _p2Alive = true;
        private int _p1Input;

        private bool _superEffectiveSound = false;
        private bool _notEffectiveSound = false;
        private bool _normalEffectSound = false;
        private bool _noEffectSound = false;

        private bool _superEffective = false;
        private bool _notEffective = false;
        private bool _normalEffect = false;
        private bool _noEffect = false;

        private bool _critHit = false;

        private bool _moveMissP1 = false;
        private bool _moveMissP2 = false;
        private bool _canChooseMove = true;
        private float _currentHealth;
        private int _enemyRandomMove;
        
        private void Start()
        {
            mainBGM.Play();
        
            _p1Name = p1.text.text;
            _p2Name = p2.text.text;

            _p1MovesSource = p1.gameObject.GetComponent<Moves>();
            _p2MovesSource = p2.gameObject.GetComponent<Moves>();
            
            _p1Sprite = p1.sprite;
            _p2Sprite = p2.sprite;

            _p1HealthBarSource = p1.healthBar;
            _p2HealthBarSource = p2.healthBar;

            battleDialogue.WhatWillPokeDo(_p1Name);
            _currentHealth = p1.statsGlobal[0].base_stat;
            
            battleDialogue.InitMoveMenu(this,_p1MovesSource);
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


            if (_actionCounterP1 < (float)_damageResultP1 && _p1HealthBarSource.slider.value != 0)
            {
                _p2HealthBarSource.SetHealth(_p2HealthBarSource.slider.value - 0.5f);
                _actionCounterP1 += 0.5f;
                if(_actionCounterP1 >= (float)_damageResultP1)
                {
                    _actionCounterP1 = 0;
                    _damageResultP1 = 0;

                    if(_p2HealthBarSource.slider.value != 0 && !_p2Turn)
                    {
                        StartCoroutine(EnemyMove());
                    }
                }
            }

            if (_actionCounterP2 < (float)_damageResultP2)
            {
            
                _p1HealthBarSource.SetHealth(_p1HealthBarSource.slider.value - 0.5f);
                _actionCounterP2 += 0.5f;
                if(_actionCounterP2 >= (float)_damageResultP2)
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
                StartCoroutine(P2Faint());
            }

            if(_p1HealthBarSource.slider.value == 0 && _p1Alive)
            {
                _p1Alive = false;
                StartCoroutine(P1Faint());
            }
        }
        
        IEnumerator EffectivenessText()
        {
            if (_superEffective)
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
                dialogueText.text = "It's super effective!";
                _superEffective = false;
            }
            else if (_notEffective)
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
                dialogueText.text = "It's not very effective..."; 
                _notEffective = false;
            }
            else if (_noEffect)
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
                dialogueText.text = "It has no effect";
                _noEffect = false;
            }
            else if (_moveMissP1) 
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
                dialogueText.text = p1.text.text + " missed!";
                _moveMissP1 = false;
            }
            else if (_moveMissP2)
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
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
            if(_critHit)
            {
                yield return new WaitForSeconds(1f);
                dialogueText.text = "";
                dialogueText.text = "A critical hit!";
                _critHit = false;
            }
        }
        public void P1DialogueText()
        {
            battleDialogue.WhatWillPokeDo(_p1Name);
            _canChooseMove = true;
        }


        IEnumerator P1Faint()
        {
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }

            yield return StartCoroutine(EffectivenessText());
            yield return new WaitForSeconds(1f);
            dialogueText.text = "";
            dialogueText.text = _p1Name + " fainted!";
            p1.text.text = "";
            _p1Sprite.gameObject.SetActive(false);
            _p1HealthBarSource.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            lowHp.FadeOut(1f);
            mainBGM.FadeOut(2f);
            SetLossText();
        }


        IEnumerator P2Faint()
        {
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }

            yield return StartCoroutine(EffectivenessText());
            yield return new WaitForSeconds(1f);
            dialogueText.text = "";
            dialogueText.text = "The foe's " + _p2Name + " fainted!";
            p2.text.text = "";
            _p2Sprite.gameObject.SetActive(false);
            _p2HealthBarSource.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            mainBGM.FadeOut(2f);
            SetVictoryText();
        }



        public void SetVictoryText()
        {
            dialogueText.text = "";
            dialogueText.text = "You defeated the enemy trainer!";
            victoryBGM.Play();
        }

        public void SetLossText()
        {
            dialogueText.text = "";
            dialogueText.text = "You lost";

            YOULOST.Play(); //change later lol
        }

        private IEnumerator P1Move()
        {
            _p1Turn = true;
            if(_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }

            yield return StartCoroutine(EffectivenessText());
            yield return new WaitForSeconds(1f);
            P1ChooseMove(_p1Input);
            if (_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(EffectivenessText());
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
            yield return StartCoroutine(EffectivenessText());
            yield return new WaitForSeconds(1f);
            _enemyRandomMove = Random.Range(0, 3);
            EnemyChooseMove(_enemyRandomMove);
            if (_critHit)
            {
                yield return StartCoroutine(CritHitText());
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(EffectivenessText());
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

                if (p1.statsGlobal[5].base_stat > p2.statsGlobal[5].base_stat)
                {
                    _p1Turn = true;
                    _p2Turn = false;
                    P1ChooseMove(input);
                }
                else if (p1.statsGlobal[5].base_stat < p2.statsGlobal[5].base_stat)
                {
                    _p2Turn = true;
                    _p1Turn = false;
                    EnemyChooseMove(_enemyRandomMove);
                }
                else
                {
                    int speedTie = Random.Range(0, 1);
                    if (speedTie == 0)
                    {
                        _p1Turn = true;
                        _p2Turn = false;
                        P1ChooseMove(input);
                    }
                    else
                    {
                        _p2Turn = true;
                        _p1Turn = false;
                        EnemyChooseMove(_enemyRandomMove);
                    }
                }
                _canChooseMove = false;

            }
        }
        public void EnemyChooseMove(int input) //make it wait for moves to generate
        {
            string p2MoveCap = char.ToUpper(_p2MovesSource.moveSet[input].name[0]) + _p2MovesSource.moveSet[input].name.Substring(1);
            dialogueText.text = "";
            dialogueText.text = "The foe's " + p2.text.text + " used " + p2MoveCap + "!";
            int? enemyPower = _p2MovesSource.moveSet[input].power;
            float targetHealth = p1.statsGlobal[0].base_stat;
            
            float enemyAttack = p2.statsGlobal[1].base_stat;
            float enemySpAttack = p2.statsGlobal[3].base_stat;

            float targetDefense = p2.statsGlobal[2].base_stat;
            float targetSpDefense = p2.statsGlobal[4].base_stat;

            int calcRandom = Random.Range(80, 100);
            int accRandom = Random.Range(1, 100);

            double damageCalc;

            if (accRandom > _p2MovesSource.moveSet[input].accuracy)
            {
                _moveMissP2 = true;
                return;
            }

            if (_p2MovesSource.moveSet[input].damage_class.name == "physical")
            {
                damageCalc = ((((int)enemyPower * (enemyAttack / targetDefense) * 10) / 50) * calcRandom) / 100;
                
            }
            else
            {
                damageCalc = ((((int)enemyPower * (enemySpAttack / targetSpDefense) * 10) / 50) * calcRandom) / 100;
            }

            //crit
            int critRand = Random.Range(1, 100);
            Debug.Log(critRand);
            if (critRand < 7)
            {
                damageCalc = damageCalc * 1.5;
                _critHit = true;
            }
        
            List<string> p1Types = new List<string>();
            if (p1.typeGlobal.Count == 2)
            {
                p1Types.Add(p1.typeGlobal[0].type.name);
                p1Types.Add(p1.typeGlobal[1].type.name);
            }
            else
            {
                p1Types.Add(p1.typeGlobal[0].type.name);
            }

            damageCalc = EffectivenessCalc(damageCalc, input, _p2MovesSource.moveSet[input].type.name, p1Types);

            //STAB
            damageCalc = StabCalc(damageCalc, p1Types, _p2MovesSource.moveSet[input].type.name);
        
            _currentHealth -= (float)damageCalc;
        
            if (_currentHealth / targetHealth <= 0.160 && _currentHealth / targetHealth > 0) //normalize percent value?
            {

                mainBGM.Stop();
                lowHp.Play();
            }

            _damageResultP2 = damageCalc;
            Mathf.Floor((float)_damageResultP2);
        }


        public void P1ChooseMove(int input)
        {
            //check for speed here
            string p1MoveCap = char.ToUpper(_p1MovesSource.moveSet[input].name[0]) + _p1MovesSource.moveSet[input].name.Substring(1);
            dialogueText.text = "";
            dialogueText.text = p1.text.text + " used " + p1MoveCap + "!";

            int? playerPower = _p1MovesSource.moveSet[input].power;
            float targetHealth = p2.statsGlobal[0].base_stat;

            //Attacks
            float playerAttack = p1.statsGlobal[1].base_stat;
            float playerSpAttack = p1.statsGlobal[3].base_stat;
            
            //Defenses
            float targetDefense = p2.statsGlobal[2].base_stat;
            float targetSpDefense = p2.statsGlobal[4].base_stat;

            int calcRandom = Random.Range(80, 100);
            int accRandom = Random.Range(1, 100);

            //normal effect, 2x super effective, 4x super effective, 0.5 not effective, 0.25 not effective, no effect

            //accuracy
            double damageCalc;

            // Debug.Log("P1 rand:" + accRandom);
            if (accRandom > _p1MovesSource.moveSet[input].accuracy) 
            {

                //  Debug.Log("P1: MISS");
                _moveMissP1 = true;
                return;
            }

            if(_p1MovesSource.moveSet[input].damage_class.name == "physical") 
            {
                damageCalc = ((((int)playerPower * (playerAttack / targetDefense) * 10) / 50) * calcRandom) / 100;
                //Debug.Log("p1: PHYS");
            }
                
            else
            {
                damageCalc = ((((int)playerPower * (playerSpAttack / targetSpDefense) * 10) / 50) * calcRandom) / 100;
                // Debug.Log("p1: SP");
            }

            //crit
            int critRand = Random.Range(1, 100);
            Debug.Log(critRand);
            if (critRand < 7)
            {
                damageCalc *= 1.5;

                _critHit = true;
            }

            List<string> p2Types = new List<string>();
            if (p2.typeGlobal.Count == 2)
            {
                p2Types.Add(p2.typeGlobal[0].type.name);
                p2Types.Add(p2.typeGlobal[1].type.name);
            }
            else
            {
                p2Types.Add(p2.typeGlobal[0].type.name);
            }

            damageCalc = EffectivenessCalc(damageCalc, input, _p1MovesSource.moveSet[input].type.name, p2Types);

            //STAB
            damageCalc = StabCalc(damageCalc, p2Types, _p1MovesSource.moveSet[input].type.name);

            _damageResultP1 = damageCalc;
    
            Mathf.Floor((float)_damageResultP1);
        }


        private double StabCalc(double damageCalc, List<string> pokeTypes, string moveType )
        {
            if (pokeTypes.Contains(moveType))
            {
                damageCalc *= 1.5f;
            }
            return damageCalc;
        }
    

        private double EffectivenessCalc(double damageCalc, int input, string dealingDamage, List<string> takingDamage)
        {
            //TYPE CALC
            //Debug.Log("THIS IS THE MOVE TYPE DEALING DAMAGE" + dealingDamage);

            //Debug.Log("Before Calcs: " + damageCalc);

            double beforeDamageCalc = damageCalc;

            //Debug.Log("P HAS THIS MANY TYPES" + P2.typeGlobalP2.Count);
            var superEffectives = TypeEffectiveness.superEffective[dealingDamage];
            var notEffectives = TypeEffectiveness.superEffective[dealingDamage];
            var immunes = TypeEffectiveness.superEffective[dealingDamage];
            
            foreach (var type in takingDamage)
            {
                if (superEffectives.Contains(type))
                {
                    damageCalc *= 2;
                }else if (notEffectives.Contains(type))
                {
                    damageCalc *= .5f;
                }else if (immunes.Contains(type))
                {
                    damageCalc *= 0;
                    break;
                }
            }
            
            //check which type of Effectiveness
            if(damageCalc >= beforeDamageCalc * 2)
            {
                _superEffectiveSound = true;
            }
            else if (damageCalc != 0 && damageCalc <= beforeDamageCalc * 0.5f)
            {
                _notEffectiveSound = true;
            }
            else if (beforeDamageCalc == damageCalc)
            {
                _normalEffectSound = true;
            }
            else if(damageCalc == 0)
            {
                _noEffectSound = true;
            }
            else
            {
                _noEffectSound = false;
                _superEffectiveSound = false;
                _notEffectiveSound = false;
                _normalEffectSound = false;
            }

            return damageCalc;
        }
    }
}
