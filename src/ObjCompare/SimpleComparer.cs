using System;
using System.Reflection;
using System.Text;

namespace ObjCompare
{
    public class SimpleComparer
    {
        public string Compare(object oldObject, object newObject)
        {
            NullValidation(oldObject, newObject);
            if (IsTheSameType(oldObject, newObject) is false)
                throw new Exception();

            StringBuilder result = new();

            (var oldProperties, var newProperties) = GetAllProperties(oldObject, newObject);

            for (int i = 0; i < oldProperties.Length; i++)
            {
                IComparator propName = new PropertyName(oldProperties[i], oldObject);
                IComparator leftProp = new PropertyValue(oldProperties[i], oldObject);
                IComparator rightProp = new PropertyValue(newProperties[i], newObject);


                IComparator propInformation = new PropertyInformation(propName, leftProp, rightProp);

                if (propInformation.Validade() is false)
                {
                    IVisitor visitor = new ComunPrint();
                    result.Append(propInformation.Accept(visitor));
                }
            }

            return result.ToString();
        }

        private void NullValidation(object oldObject, object newObject)
        {
            if (oldObject is null)
                throw new ArgumentNullException(nameof(oldObject), $"Can't be null");
            if (newObject is null)
                throw new ArgumentNullException(nameof(newObject), $"Can't be null");
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
