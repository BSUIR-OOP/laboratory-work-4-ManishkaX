using System.Reflection;
using DependencyLibrary.Exceptions;

namespace DependencyLibrary
{
    public class DependencyInjectionContainer
    {
        private readonly Dictionary<Type, object> _singletons;

        private readonly Dictionary<Type, ConstructorInfo> _constructors;


        public DependencyInjectionContainer()
        {
            _singletons = new Dictionary<Type, object>();
            _constructors = new Dictionary<Type, ConstructorInfo>();
        }


        public void RegisterType<T>()
        {
            var type = typeof(T);
            if (_constructors.ContainsKey(type))
                throw new RegisteredTypeException();

            _constructors.Add(type, type.GetConstructors()[0]);
        }


        public T? GetTransient<T>()
        {
            return (T)CreateObject(typeof(T), new HashSet<Type>());
        }

        public T GetSingleton<T>()
        {
            var type = typeof(T);

            if (!_singletons.TryGetValue(type, out var result))
            {
                var obj = (T)CreateObject(type, new HashSet<Type>());
                _singletons.Add(type, obj);
                return obj;
            }
            else
                return (T)result;
        }

        private object? CreateObject(Type type, ISet<Type> types)
        {
            if (types.Contains(type))
                throw new CyclicException();

            if (TryGetDefaultValue(type, out var obj))
                return obj;

            if (_constructors.TryGetValue(type, out var constructor))
            {
                var parameters = new List<object?>();

                foreach (var paramInfo in constructor.GetParameters())
                {
                    var paramType = paramInfo.ParameterType;
                    types.Add(type);
                    var paramInstance = CreateObject(paramType, types);
                    parameters.Add(paramInstance);
                    types.Remove(type);
                }

                return constructor.Invoke(parameters.ToArray());
            }
            else
                throw new UnregisteredTypeException();
        }

        private static bool TryGetDefaultValue(Type type, out object? result)
        {
            if (type == typeof(string))
            {
                result = default(string);
                return true;
            }
            else if (type.IsValueType)
            {
                result = Activator.CreateInstance(type);
                return true;
            }

            result = null;
            return false;
        }
    }
}
