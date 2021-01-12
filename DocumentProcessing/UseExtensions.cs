using System;
using System.Threading.Tasks;

namespace DocumentProcessing
{
    public static class UseExtenisons
    {
        public static IDocumentProcessingBuilder Use(this IDocumentProcessingBuilder builder, Func<ProcessingContext, Func<Task>, Task> step)
        {
            return builder.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return step(context, simpleNext);
                };
            });
        }
    }
}
