// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using UnityEngine;
//
// namespace Lessons.I.Architecture.Work
// {
//     public class GameInitializationManager : MonoBehaviour
//     {
//         private List<IInitializable> initializers;
//
//         private void Awake()
//         {
//             PrepareInitializers();
//             StartInitialization();
//         }
//
//         private void PrepareInitializers()
//         {
//             var componentInChildren = gameObject.GetComponentsInChildren<IInitializable>();
//
//             initializers = componentInChildren
//                 .OrderBy(component => component.GetPriority())
//                 .ToList();
//         }
//
//         private void StartInitialization()
//         {
//             foreach (var initializable in initializers)
//             {
//                 bool success = initializable.Initialize();
//                 if (!success)
//                 {
//                     Debug.LogError("Failed to initialize");
//                     break;
//                 }
//             }
//         }
//     }
//
//     public interface IInitializable
//     {
//         public bool Initialize();
//         public int GetPriority();
//     }
//
//     public class LoadGraphics : MonoBehaviour, IInitializable
//     {
//         [SerializeField] private int priority;
//
//         public bool Initialize()
//         {
//             throw new NotImplementedException("Do something here!");
//         }
//
//         public int GetPriority()
//         {
//             return priority;
//         }
//     }
//
//     public class LoadLogic : MonoBehaviour, IInitializable
//     {
//         [SerializeField] private int priority;
//
//         public Task<bool> Initialize()
//         {
//             throw new NotImplementedException("Do something here!");
//         }
//
//         public int GetPriority()
//         {
//             return priority;
//         }
//     }
// }