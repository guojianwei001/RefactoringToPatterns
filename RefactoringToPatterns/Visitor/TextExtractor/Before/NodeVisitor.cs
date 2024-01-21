using System.Text;

namespace RefactoringToPatterns.Visitor.TextExtractor.Before
{
    public class TextExtractorVisitor
    {
        private StringBuilder results = new StringBuilder();

        public string extracText()
        {
            var nodes = new Node[3] {new Tag(), new LinkTag(), new StringNode()};
            foreach (var node in nodes)
            {
                if (node is Tag tag)
                {
                    this.results.AppendLine($"visiting Tag");
                }
                else if (node is LinkTag linkTag)
                {
                    this.results.AppendLine($"visiting LinkTag");
                }
                else if (node is StringNode stringNode)
                {
                    this.results.AppendLine($"visiting StringNode");
                }
            }

            return this.results.ToString();
        }
    }
}
