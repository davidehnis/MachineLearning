using Microsoft.ML.Data;

namespace core
{
    /// <summary>
    /// Represents "input" data to help build the categorization model
    /// </summary>
    public class Token
    {
        public Token()
        {
        }

        public Token(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Token(int id, string name, string description)
            : this(id.ToString(), name, description)
        {
        }

        [LoadColumn(0)]
        public string Id { get; set; }

        [LoadColumn(1)]
        public string Name { get; set; }

        [LoadColumn(2)]
        public string Description { get; set; }
    }
}