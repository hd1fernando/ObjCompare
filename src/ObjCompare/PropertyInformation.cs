using System;

namespace ObjCompare
{
    public class PropertyInformation : IComparator
    {
        public IComparator PropertyName;
        public IComparator LeftObject;
        public IComparator RightObject;

        public PropertyInformation(IComparator propertyName, IComparator leftObject, IComparator rightObject)
        {
            PropertyName = propertyName;
            LeftObject = leftObject;
            RightObject = rightObject;
        }

        public string Accept(IVisitor visitor)
            => visitor.Print(this);

        public object Validade()
            => LeftObject.Validade().Equals(RightObject.Validade());

    }
}
