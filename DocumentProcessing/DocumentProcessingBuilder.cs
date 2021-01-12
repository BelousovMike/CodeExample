using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentProcessing
{
    public delegate Task DocumentProcessingDelegate(ProcessingContext context);

    public interface IDocumentProcessingBuilder
    {
        IDocumentProcessingBuilder Use(Func<DocumentProcessingDelegate, DocumentProcessingDelegate> step);

        DocumentProcessingDelegate Build();
    }

    internal class DocumentProcessingBuilder : IDocumentProcessingBuilder
    {
        IList<Func<DocumentProcessingDelegate, DocumentProcessingDelegate>> _steps;

        public DocumentProcessingBuilder()
        {
            _steps = new List<Func<DocumentProcessingDelegate, DocumentProcessingDelegate>>();
        }

        public DocumentProcessingDelegate Build()
        {
            DocumentProcessingDelegate processing = context =>
            {
                return Task.CompletedTask;
            };

            foreach (var step in _steps.Reverse())
            {
                processing = step(processing);
            }

            return processing;
        }

        public IDocumentProcessingBuilder Use(Func<DocumentProcessingDelegate, DocumentProcessingDelegate> step)
        {
            _steps.Add(step);
            return this;
        }
    }
}
