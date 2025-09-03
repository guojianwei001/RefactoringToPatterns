using System.Text.RegularExpressions;

namespace RefactoringToPatterns.Builder.TreeNode;


public class TreeNodeBuilder<T>
{
    private readonly T _value;
    private readonly Dictionary<string, string> _attributes = new();
    // 关键点：我们不持有 TreeNode<T>，而是持有其子节点的 Builder
    private readonly List<TreeNodeBuilder<T>> _childBuilders = new();

    public TreeNodeBuilder(T value)
    {
        _value = value;
    }

    public TreeNodeBuilder<T> WithAttribute(string key, string value)
    {
        _attributes[key] = value;
        return this; // 返回自身以实现链式调用
    }

    // 添加一个没有子节点的叶子节点
    public TreeNodeBuilder<T> AddChild(TreeNodeBuilder<T> childBuilder)
    {
        _childBuilders.Add(childBuilder);
        return this;
    }

    // *** 核心方法：处理嵌套 ***
    // 通过 Action<...> 委托，我们可以提供一个用于配置子节点的“作用域”
    public TreeNodeBuilder<T> AddChild(T childValue, Action<TreeNodeBuilder<T>> configureChild)
    {
        // 1. 为子节点创建一个新的 Builder
        var childBuilder = new TreeNodeBuilder<T>(childValue);

        // 2. 执行传入的委托，用它来配置这个新的子节点 Builder
        configureChild(childBuilder);

        // 3. 将配置好的子节点 Builder 添加到子节点列表中
        _childBuilders.Add(childBuilder);

        return this;
    }

    // 最终的构建方法
    public TreeNode<T> Build()
    {
        // 递归地为所有子节点的 Builder 调用 Build() 方法
        var children = _childBuilders.Select(builder => builder.Build()).ToList();

        // 创建并返回最终的、不可变的 TreeNode 对象
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

        // 使用一个栈来追踪父节点。元组中包含 Builder 和它的缩进级别。
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

            if (rootBuilder == null) // 这是第一个节点，即根节点
            {
                rootBuilder = currentBuilder;
                parentStack.Push((rootBuilder, indentLevel));
                continue;
            }

            // 关键逻辑：根据缩进级别，找到正确的父节点
            // 如果当前行的缩进小于或等于栈顶节点的缩进，说明层级回退了，需要出栈
            while (parentStack.Count > 0 && indentLevel <= parentStack.Peek().indentLevel)
            {
                parentStack.Pop();
            }

            // 此时，栈顶的节点就是当前节点的父节点
            if (parentStack.Count > 0)
            {
                parentStack.Peek().builder.AddChild(currentBuilder);
            }

            // 将当前节点压入栈，作为后续节点的潜在父节点
            parentStack.Push((currentBuilder, indentLevel));
        }

        return rootBuilder?.Build();
    }

    // 私有辅助方法，用于解析单行文本
    private static (int indentLevel, string value, Dictionary<string, string> attributes) ParseLine(string line)
    {
        // 1. 计算缩进
        int indentLevel = line.TakeWhile(c => char.IsWhiteSpace(c)).Count();
        string content = line.TrimStart();

        // 移除 Markdown 列表标记
        if (content.StartsWith("- "))
        {
            content = content.Substring(2);
        }

        var attributes = new Dictionary<string, string>();
        string value = content;

        // 2. 解析属性
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