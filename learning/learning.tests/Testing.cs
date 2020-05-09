using System.IO;
using core;

namespace learning.tests
{
    public class Testing : ISettings
    {
        public Testing()
        {
            ModelPath = $"{Path.GetTempPath()}\\learning.tests\\skills.model";
            var dir = Path.GetDirectoryName(ModelPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        public string ModelPath { get; }
    }
}