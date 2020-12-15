namespace ObjCompare
{
    public interface IVisitor
    {
        public string Print(PropertyName name);
        public string Print(PropertyComparator name);
        public string Print(PropertyInformation name);
    }
}
