using System.Text;

namespace RefactoringToPatterns.Visitor.TextExtractor.After
{
    public interface NodeVisitor
    {
        void visitTag(Tag tag);
        void visitLinkTag(LinkTag linkTag);
        void visitStringNode(StringNode stringNode);
    }

    public class TextExtractorVisitor : NodeVisitor
    {
        private StringBuilder results = new StringBuilder();

        public string extracText()
        {
            var nodes = new Node[3] {new Tag(), new LinkTag(), new StringNode()};
            foreach (var node in nodes)
            {
                node.acceptVisitor(this);
            }

            return this.results.ToString();
        }

        public void visitLinkTag(LinkTag linkTag)
        {
            this.results.AppendLine($"visiting LinkTag");
        }

        public void visitStringNode(StringNode stringNode)
        {
            this.results.AppendLine($"visiting StringNode");
        }

        public void visitTag(Tag tag)
        {
            this.results.AppendLine($"visiting Tag");
        }
    }
}
