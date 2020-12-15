using System.Reflection;

namespace ObjCompare
{
    public class PropertyName : IComparator
    {
        PropertyInfo _propertyInfo;
        object _object;

        public PropertyName(PropertyInfo propertyInfo, object @object)
        {
            _propertyInfo = propertyInfo;
            _object = @object;
        }

        public string Accept(IVisitor visitor)
            => visitor.Print(this);

        public object Validade()
            => _propertyInfo.Name.ToString();
    }
}
