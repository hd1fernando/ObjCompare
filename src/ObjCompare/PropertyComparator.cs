namespace ObjCompare
{
    public class PropertyComparator : IComparator
    {
        public IComparator LeftObject;
        public IComparator RightObject;

        public PropertyComparator(IComparator leftObject, IComparator rightObject)
        {
            LeftObject = leftObject;
            RightObject = rightObject;
        }

        public string Accept(IVisitor visitor)
            => visitor.Print(this);

        public object Validade()
            => LeftObject.Validade().Equals(RightObject.Validade());
    }
}
