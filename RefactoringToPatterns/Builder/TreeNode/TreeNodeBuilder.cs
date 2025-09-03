using System.Text.RegularExpressions;

namespace RefactoringToPatterns.Builder.TreeNode;


public class TreeNodeBuilder<T>
{
    private readonly T _value;
    private readonly Dictionary<string, string> _attributes = new();

    // key points: not hold TreeNode, but hold TreeNodeBuilder
    private readonly List<TreeNodeBuilder<T>> _childBuilders = new();

    public TreeNodeBuilder(T value)
    {
        _value = value;
    }

    public TreeNodeBuilder<T> WithAttribute(string key, string value)
    {
        _attributes[key] = value;
        return this; // return this for Fluent Interface
    }

    public TreeNodeBuilder<T> AddChild(TreeNodeBuilder<T> childBuilder)
    {
        _childBuilders.Add(childBuilder);
        return this;
    }

    public TreeNodeBuilder<T> AddChild(T childValue, Action<TreeNodeBuilder<T>> configureChild)
    {
        var childBuilder = new TreeNodeBuilder<T>(childValue);

        configureChild(childBuilder);

        _childBuilders.Add(childBuilder);

        return this;
    }

    public TreeNode<T> Build()
    {
        var children = _childBuilders.Select(builder => builder.Build()).ToList();

        return new TreeNode<T>(_value, _attributes, children);
    }
}


public static class MarkdownTreeParser
{
    public static TreeNode<string> Parse(string markdownText)
    {
        var lines = markdownText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0)
        {
            return null;
        }

        var parentStack = new Stack<(TreeNodeBuilder<string> builder, int indentLevel)>();

        TreeNodeBuilder<string> rootBuilder = null;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var (indentLevel, value, attributes) = ParseLine(line);

            var currentBuilder = new TreeNodeBuilder<string>(value);
            foreach (var attr in attributes)
            {
                currentBuilder.WithAttribute(attr.Key, attr.Value);
            }

            if (rootBuilder == null) // root node
            {
                rootBuilder = currentBuilder;
                parentStack.Push((rootBuilder, indentLevel));
                continue;
            }

            while (parentStack.Count > 0 && indentLevel <= parentStack.Peek().indentLevel)
            {
                parentStack.Pop();
            }

            if (parentStack.Count > 0)
            {
                parentStack.Peek().builder.AddChild(currentBuilder);
            }

            parentStack.Push((currentBuilder, indentLevel));
        }

        return rootBuilder?.Build();
    }

    private static (int indentLevel, string value, Dictionary<string, string> attributes) ParseLine(string line)
    {
        int indentLevel = line.TakeWhile(c => char.IsWhiteSpace(c)).Count();
        string content = line.TrimStart();

        if (content.StartsWith("- "))
        {
            content = content.Substring(2);
        }

        var attributes = new Dictionary<string, string>();
        string value = content;

        var attrMatch = Regex.Match(content, @"\((.+)\)");
        if (attrMatch.Success)
        {
            value = content.Substring(0, attrMatch.Index).Trim();
            string attrString = attrMatch.Groups[1].Value;

            var attrPairs = attrString.Split(',', StringSplitOptions.TrimEntries);
            foreach (var pair in attrPairs)
            {
                var parts = pair.Split(':', 2, StringSplitOptions.TrimEntries);
                if (parts.Length == 2)
                {
                    attributes[parts[0]] = parts[1].Trim('\'', '"');
                }
            }
        }

        return (indentLevel, value, attributes);
    }
}