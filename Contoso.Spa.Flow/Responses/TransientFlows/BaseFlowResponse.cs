namespace Contoso.Spa.Flow.Responses.TransientFlows
{
    public abstract class BaseFlowResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = [];
        public string TypeFullName => GetType().AssemblyQualifiedName!;
    }
}
