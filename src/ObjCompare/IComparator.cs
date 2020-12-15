namespace ObjCompare
{
    public interface IComparator : IAcceptVisitor
    {
        object Validade();
    }

    public interface IAcceptVisitor
    {
        string Accept(IVisitor visitor);
    }
}
