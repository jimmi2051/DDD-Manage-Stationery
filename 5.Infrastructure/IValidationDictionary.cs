namespace MyProject.Infrastructure
{
    public interface IValidationDictionary
    {
        void Clear();
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
