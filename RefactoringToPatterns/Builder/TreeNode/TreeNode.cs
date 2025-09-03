using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactoringToPatterns.Builder.TreeNode;

// 这是我们最终要创建的“产品” (Product)
public class TreeNode<T>
{
    public T Value { get; }
    public IReadOnlyDictionary<string, string> Attributes { get; }
    public IReadOnlyList<TreeNode<T>> Children { get; }

    // 构造函数设为 internal，强制通过 Builder 创建
    internal TreeNode(T value, Dictionary<string, string> attributes, List<TreeNode<T>> children)
    {
        Value = value;
        Attributes = attributes;
        Children = children;
    }

    // 辅助方法：漂亮地打印树结构，用于验证结果
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