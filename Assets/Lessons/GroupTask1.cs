// //4 Команда - Владимир Телеьнов и Николай Еремеев и Путиенко Евгений.
//
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Game.Gameplay.Player;
// using Lessons.AI.Lesson_GOAP2;
// using Lessons.Architecture.DI;
// using Lessons.Meta;
// using Sirenix.Utilities;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// /// Упражнение №1
// /// 1 балл
// class InputController
// {
//     public Action GamePaused;
//     public Action Jumped;
//
//     public void HandleInput()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             GamePaused?.Invoke();
//         }
//
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             Jumped?.Invoke();
//         }
//     }
// }
//
// class InputObserver : MonoBehaviour
// {
//     [SerializeField] private GameManager _gameManager;
//     [SerializeField] private Player _player;
//
//     private InputController _inputController;
//
//     void OnEnable()
//     {
//         _inputController.GamePaused += _gameManager.PauseGame();
//         _inputController.Jumped += _player.Jump();
//     }
//
//     void OnDisable()
//     {
//         _inputController.GamePaused -= _gameManager.PauseGame();
//         _inputController.Jumped -= _player.Jump();
//     }
// }
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №2
// // 1 Балл
// public interface IAttackable
// {
//     void Attack();
// }
//
// public interface IMovable
// {
//     void Move(Vector3 direction);
// }
//
// public interface IHealth
// {
//     int HitPoints { get; }
// }
//
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №3 
// //2 балла
// public class ObjectPool<T> where T : Component
// {
//     private readonly Queue<T> _pool;
//
//     public ObjectPool(T pref, int size)
//     {
//         _pool = new Queue<T>(size);
//         for (var i = 0; i < size; i++)
//         {
//             _pool.Enqueue(GameObject.Instantiate(pref));
//         }
//     }
//
//     public T Get()
//     {
//         return _pool.Dequeue();
//     }
//
//     public void Release(T item)
//     {
//         _pool.Enqueue(item);
//     }
// }
//
// class Example
// {
//     private ObjectPool<Enemy> _enemyPool;
//     private ObjectPool<Bullet> _bulletPool;
// }
//
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №4
// // 2 балла
// public sealed class MyPlayer : MonoBehaviour
// {
//     private MoveComponent _moveComponent;
//     private HealthComponent _healthComponent;
//
//     public void MoveTowards(Vector3 direction)
//     {
//         _moveComponent.MoveTowards(direction);
//     }
//
//     public void TakeDamage(int damage)
//     {
//         _healthComponent.TakeDamage(damage);
//     }
//
//     public void Initialize(int health, float speed)
//     {
//         _moveComponent = new MoveComponent(transform, speed);
//         _healthComponent = new HealthComponent(health);
//     }
//
//     public int GetHitPoints()
//     {
//         return _healthComponent.GetHitPoints();
//     }
//
//     public float GetSpeed()
//     {
//         return _moveComponent.getSpeed();
//     }
// }
//
// [Serializable]
// public class MoveComponent
// {
//     [SerializeField] private Transform _transform;
//     [SerializeField] private float _speed;
//
//     public MoveComponent(Transform transform, float speed)
//     {
//         _transform = transform;
//         _speed = speed;
//     }
//
//     public void MoveTowards(Vector3 direction)
//     {
//         _transform.position += direction * _speed * Time.deltaTime;
//     }
//
//     public float getSpeed()
//     {
//         return _speed;
//     }
// }
//
// [Serializable]
// public class HealthComponent
// {
//     [SerializeField] private int _hitPoints;
//
//     public HealthComponent(int hitPoints)
//     {
//         _hitPoints = hitPoints;
//     }
//
//     public void TakeDamage(int damage)
//     {
//         _hitPoints -= damage;
//     }
//
//     public int GetHitPoints()
//     {
//         return _hitPoints;
//     }
// }
//
// [Serializable]
// public class SaveManger
// {
//     [SerializeField] private MyPlayer _player;
//
//     private const string HITPOINTS = "hitPoints";
//     private const string SPEED = "speed";
//
//     public void Save()
//     {
//         PlayerPrefs.SetInt(HITPOINTS, _player.GetHitPoints());
//         PlayerPrefs.SetFloat(SPEED, _player.GetSpeed());
//     }
//
//     public void Load()
//     {
//         int _hitPoints = PlayerPrefs.GetInt(HITPOINTS);
//         float _speed = PlayerPrefs.GetFloat(SPEED);
//
//         _player.Initialize(_hitPoints, _speed);
//     }
// }
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №5
// //2 балла
// public interface IGameStarter
// {
//     void OnGameStart();
// }
//
// public class HeroSpawner : IGameStarter
// {
//     private Hero hero;
//
//     public void OnGameStart()
//     {
//         hero.Spawn();
//     }
// }
//
// class EnemyEnabler : IGameStarter
// {
//     private GameObject[] enemies;
//
//     public void OnGameStart()
//     {
//         enemies.ForEach(it => it.SetActive(true));
//     }
// }
//
// class QuestStarter : IGameStarter
// {
//     private QuestManager questManager;
//
//     public void OnGameStart()
//     {
//         questManager.Start();
//     }
// }
//
// class CameraEnabler : IGameStarter
// {
//     private CameraService cameraService;
//
//     public void OnGameStart()
//     {
//         cameraService.Initialize();
//     }
// }
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №6
// /// 3 балла
// public class MyQuestManager
// {
//     private readonly QuestConfig[] questPool;
//     private readonly MoneyStorage moneyStorage;
//
//     public Quest CurrentQuest { get; private set; }
//
//
//     public MyQuestManager(QuestConfig[] questPool)
//     {
//         this.questPool = questPool;
//     }
//
//     public void StartNewQuest()
//     {
//         CurrentQuest = CreateNewQuest();
//         CurrentQuest.Start();
//     }
//
//     public void ReceiveReward()
//     {
//         var quest = this.CurrentQuest;
//         if (quest is { IsCompleted: true })
//         {
//             moneyStorage.EarnMoney(quest.MoneyReward);
//             quest.Dispose();
//             CurrentQuest = null;
//         }
//     }
//
//     private Quest CreateNewQuest()
//     {
//         var questConfig = SelectQuestConfig();
//         return questConfig.InstantiateQuest();
//     }
//
//     private QuestConfig SelectQuestConfig()
//     {
//         var randomIndex = Random.Range(0, this.questPool.Length);
//         return this.questPool[randomIndex];
//     }
// }
// ///////////////////////////////////////////////////////////////////////////////
//
//
// /// Упражнение №7 (*)
// /// 4 балла
// public enum PaymentType
// {
//     SoftCurrency,
//     HardCurrency,
//     Tokens,
//     IAP,
//
//     ADS
//     //TODO: add other payment types
// }
//
//
// public interface IPaymentStrategy
// {
//     Task<bool> Pay(ShopProduct product);
// }
//
// class SoftCurrencyPaymentStrategy : IPaymentStrategy
// {
//     public Task<bool> Pay(ShopProduct product)
//     {
//         return MoneyBank.TrySpendSoftCurrency(product.m_softCurrencyPrice);
//     }
// }
//
// class HardCurrencyPaymentStrategy : IPaymentStrategy
// {
//     public Task<bool> Pay(ShopProduct product)
//     {
//         return MoneyBank.TrySpendHardCurrency(product.m_hardCurrencyPrice);
//     }
// }
//
// class TokenPaymentStrategy : IPaymentStrategy
// {
//     public async Task<bool> Pay(ShopProduct product)
//     {
//         var result = await TokenServer.TrySpendTokens(product);
//         return result.success;
//     }
// }
//
// class AdsPaymentStrategy : IPaymentStrategy
// {
//     public async Task<bool> Pay(ShopProduct product)
//     {
//         var result = await AdsManager.ShowRewardedVideo();
//         return result.success;
//     }
// }
//
// class IapPaymentStrategy : IPaymentStrategy
// {
//     public async Task<bool> Pay(ShopProduct product)
//     {
//         var result = await IAPManager.Purchase(product);
//         return result.success;
//     }
// }
//
// public sealed class PaymentService
// {
//     [Inject]
//     public Dictionary<PaymentType, IPaymentStrategy> Strategies;
//
//     public async Task<bool> Purchase(ShopProduct product, PaymentType paymentType)
//     {
//         var paymentStrategy = Strategies[paymentType];
//         return await paymentStrategy.Pay(product);
//     }
// }
//
// public sealed class ShopProduct : ScriptableObject
// {
//     [field: SerializeField] public string m_titleCode { get; private set; }
//
//     [field: SerializeField] public string m_descCode { get; private set; }
//
//     [field: SerializeField] public Sprite m_iconSprite { get; private set; }
//
//     [field: SerializeField] public PaymentType m_paymentType { get; private set; }
//     [field: SerializeField] public int m_softCurrencyPrice { get; private set; }
//     [field: SerializeField] public int m_hardCurrencyPrice { get; private set; }
//
//     [field: SerializeField] public string m_inAppProductId { get; private set; }
//
//     [field: SerializeField] public int m_tokenPrice { get; private set; }
// }
// ///////////////////////////////////////////////////////////////////////////////
//
//
// //Упражнение №8 (**)
// /// 5 баллов
// public sealed class MyMoveController : MonoBehaviour
// {
//     [SerializeField] private MoveInput moveInput;
//     [SerializeReference] private IMovable movableObject;
//
//     public void OnEnable()
//     {
//         moveInput.OnMove += this.OnMove;
//     }
//
//     public void OnDisable()
//     {
//         moveInput.OnMove -= this.OnMove;
//     }
//
//     private void OnMove(Vector3 direction)
//     {
//         movableObject.MoveTowards(direction);
//     }
// }