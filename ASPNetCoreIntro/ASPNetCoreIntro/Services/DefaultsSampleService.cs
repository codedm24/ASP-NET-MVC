
namespace ASPNetCoreIntro.Services
{
    public class DefaultsSampleService : ISampleService
    {
        private List<string> _strings = new List<string>() { "one", "two", "three"};
        public IEnumerable<string> GetSampleStrings() => _strings;
    }
}
