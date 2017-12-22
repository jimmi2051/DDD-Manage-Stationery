using System.Collections.Generic;
namespace MyProject.Infrastructure
{
    public class ModelStateDictionary : Dictionary<string, string>
    {
         internal void AddModelError(string key, string errorMessage)
        {
            this.Add(key, errorMessage);
        }

        public bool IsValid
        {
            get { return (this.Count <= 0); }
        }
    }
}
