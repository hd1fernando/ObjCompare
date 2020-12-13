using System;
using System.Text;

namespace ObjCompare
{
    public class SimpleComparer
    {
        public string Compare(object oldObject, object newObject)
        {
            var oldProperties = oldObject.GetType().GetProperties();
            var newProperties = newObject.GetType().GetProperties();

            StringBuilder result = new();

            for (int i = 0; i < oldProperties.Length; i++)
            {
                var oldProp = oldProperties[i].GetValue(oldObject);
                var newProp = newProperties[i].GetValue(newObject);
                if (oldProp != newProp)
                    result.Append($"{oldProperties[i].Name}:{oldProp},{newProp}");
            }

            return result.ToString();
        }
    }
}
