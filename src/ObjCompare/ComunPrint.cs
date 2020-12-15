namespace ObjCompare
{
    public class ComunPrint : IVisitor
    {
        public string Print(PropertyName name)
            => $"{name.Validade()}:";

        public string Print(PropertyComparator property)
            => $"{property.LeftObject.Validade()},{property.RightObject.Validade()};";

        public string Print(PropertyInformation name)
            => $"{name.PropertyName.Validade()}:{name.LeftObject.Validade()},{name.RightObject.Validade()};";
    }
}
