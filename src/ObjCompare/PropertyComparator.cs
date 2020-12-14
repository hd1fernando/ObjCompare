using System;

namespace ObjCompare
{
    public class PropertyComparator : IComparator
    {
        IComparator _leftObject;
        IComparator _rightObject;

        public PropertyComparator(IComparator leftObject, IComparator rightObject)
        {
            _leftObject = leftObject;
            _rightObject = rightObject;
        }

        public object Validade()
            => (bool)_leftObject.Validade().Equals(_rightObject.Validade());
    }
}
