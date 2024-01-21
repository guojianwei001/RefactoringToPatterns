namespace RefactoringToPatterns.Visitor.TextExtractor.After
{
    public interface Node
    {
        void acceptVisitor(NodeVisitor visitor);
    }

    public class Tag : Node
    {
        public void acceptVisitor(NodeVisitor visitor)
        {
            visitor.visitTag(this);
        }
    }

    public class LinkTag : Node
    {
        public void acceptVisitor(NodeVisitor visitor)
        {
            visitor.visitLinkTag(this);
        }
    }

    public class StringNode : Node
    {
        public void acceptVisitor(NodeVisitor visitor)
        {
            visitor.visitStringNode(this);
        }
    }
}
