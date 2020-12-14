using System.Reflection;

namespace ObjCompare
{
    public class PropertyValue : IComparator
    {
        PropertyInfo _propertyInfo;
        object _object;

        public PropertyValue(PropertyInfo propertyInfo, object @object)
        {
            _propertyInfo = propertyInfo;
            _object = @object;
        }

        public object Validade()
            => _propertyInfo.GetValue(_object);
    }
}
