using UnityEngine;

namespace Common
{
    public static class GameDebug
    {
        public static void Log(string message)
        {
#if true
            Debug.Log( message );
#endif
        }
        
        public static void LogError(string message)
        {
#if true
            Debug.LogError( message );    
#endif
        }
    }
}