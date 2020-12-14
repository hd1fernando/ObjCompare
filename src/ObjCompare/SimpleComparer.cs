using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjCompare
{
    public class SimpleComparer
    {
        public string Compare(object oldObject, object newObject)
        {
            if (IsTheSameType(oldObject, newObject) is false)
                throw new Exception();

            (var oldProperties, var newProperties) = GetAllProperties(oldObject, newObject);

            StringBuilder result = new();

            for (int i = 0; i < oldProperties.Length; i++)
            {
                IComparator oldProp = new PropertyValue(oldProperties[i], oldObject);
                IComparator newProp = new PropertyValue(newProperties[i], newObject);

                if (new PropertyComparator(oldProp, newProp).Validade() is false)
                    result.Append($"{oldProperties[i].Name}:{oldProp.Validade()},{newProp.Validade()};");
            }

            return result.ToString();
        }

        public (PropertyInfo[] leftProperties, PropertyInfo[] rightProperties) GetAllProperties(object leftObject, object rightObject)
        {
            var left = leftObject.GetType().GetProperties();
            var right = rightObject.GetType().GetProperties();
            return (left, right);
        }

        private bool IsTheSameType(object oldObject, object newObject)
            => oldObject.GetType() == newObject.GetType();
    }
}
