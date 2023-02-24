namespace Common
{
    public abstract class Singleton<T> where T : class
    {
        private static T instance;
        
        public static T Instance => instance ?? CreateInstance;
        public static bool HasInstance => instance != null;

        private static T CreateInstance => instance ??= System.Activator.CreateInstance(typeof(T)) as T;
    }
}