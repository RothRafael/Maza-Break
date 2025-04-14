// [Tool]
// public class ClassInitializer : Node
// {
//     private static ClassInitializer _instance;

//     [Export] public MovementParameters warrior;

//     public static ClassInitializer Instance
//     {
//         get
//         {
//             if (_instance == null)
//                 GD.PrintErr("ClassInitializer instance is not initialized!");
//             return _instance;
//         }
//     }

//     public override void _Ready()
//     {
//         if (_instance == null)
//         {
//             _instance = this;
//             createClasses();
//         }
//         else
//         {
//             QueueFree(); // Prevent duplicate instances
//         }
//     }
// }

