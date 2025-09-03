using System.Text;

namespace RefactoringToPatterns.Builder.TreeNode;

public class TreeNode<T>
{
    public T Value { get; }
    public IReadOnlyDictionary<string, string> Attributes { get; }
    public IReadOnlyList<TreeNode<T>> Children { get; }

    // set ctor as internal，force to use Builder to create object
    internal TreeNode(T value, Dictionary<string, string> attributes, List<TreeNode<T>> children)
    {
        Value = value;
        Attributes = attributes;
        Children = children;
    }

    // print for verify
    public string PrintTree(string indent = "")
    {
        var sb = new StringBuilder();
        sb.Append(indent);
        sb.Append($"- {Value}");

        if (Attributes.Any())
        {
            sb.Append(" (");
            sb.Append(string.Join(", ", Attributes.Select(kvp => $"{kvp.Key}: '{kvp.Value}'")));
            sb.Append(")");
        }

        sb.AppendLine();

        foreach (var child in Children)
        {
            sb.Append(child.PrintTree(indent + "  "));
        }

        return sb.ToString();
    }
}