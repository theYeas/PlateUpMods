using System;
using System.Collections.Generic;
using System.Reflection;

namespace theYeas.Core.Helpers
{
    public class Reflection
    {
        private static readonly Lazy<Reflection> lazy = new(() => new Reflection());
        public static Reflection Instance { get { return lazy.Value; } }

        private Dictionary<(Type, string), PropertyInfo> _PropertyCache = new Dictionary<(Type, string), PropertyInfo>();
        private Dictionary<(Type, string), FieldInfo> _FieldCache = new Dictionary<(Type, string), FieldInfo>();
        private Reflection()
        {

        }

        private PropertyInfo GetProperty(object obj, string property)
        {
            Type type = obj.GetType();
            PropertyInfo prop;
            if (!_PropertyCache.TryGetValue((type, property), out prop))
            {
                prop = type.GetProperty(property, BindingFlags.NonPublic | BindingFlags.Instance);
                _PropertyCache.Add((type, property), prop);
            }
            return prop;
        }
        private FieldInfo GetField(object obj, string property)
        {
            Type type = obj.GetType();
            FieldInfo field;
            if (!_FieldCache.TryGetValue((type, property), out field))
            {
                field = type.GetField(property, BindingFlags.NonPublic | BindingFlags.Instance);
                _FieldCache.Add((type, property), field);
            }
            return field;
        }
        public T GetInstanceProperty<T>(object obj, string property)
        {
            var prop = GetProperty(obj, property);
            MethodInfo getter = prop.GetGetMethod(nonPublic: true);
            object result = getter.Invoke(obj, null);
            return (T)Convert.ChangeType(result, typeof(T));
        }
        public T GetInstanceField<T>(object obj, string property)
        {
            var field = GetField(obj, property);
            object result = field.GetValue(obj);
            return (T)Convert.ChangeType(result, typeof(T));
        }
        public void SetInstanceField<T>(object obj, object value, string property)
        {
            var field = GetField(obj, property);
            field.SetValue(obj, value);
        }
    }
}
