namespace DocRefList.Data.Repository.Abstraction
{
    public interface IDataAccess
    {
        IDocumentRepository Document { get; }
        IFamiliarizationRepository Familiarization { get; }
        void Save();
    }
}
