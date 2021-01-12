namespace DocumentProcessing
{
    public interface IDocumentProcessingBuilderFactory
    {
        IDocumentProcessingBuilder Create();
    }

    internal class DocumentProcessingBuilderFactory : IDocumentProcessingBuilderFactory
    {
        public IDocumentProcessingBuilder Create()
        {
            return new DocumentProcessingBuilder();
        }
    }
}
