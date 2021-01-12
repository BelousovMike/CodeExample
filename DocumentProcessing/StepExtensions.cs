using System;

namespace DocumentProcessing
{
    public static class StepExtensions
    {
        public static IDocumentProcessingBuilder UseStep<TStep>(this IDocumentProcessingBuilder builder, Func<TStep> step)
            where TStep : IDocumentProcessingStep
        {
            return builder.Use(next =>
            {
                return async context =>
                {
                    await step().InvokeAsync(context, next);
                };
            });
        }
    }
}
