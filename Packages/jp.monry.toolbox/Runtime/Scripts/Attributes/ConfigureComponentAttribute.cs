using System;
using System.Collections.Generic;

namespace Monry.Toolbox.Attributes;

/// <summary>
/// この属性を設定したクラスは、Source Generator により以下の機能が自動実装されます
/// <list type="bullet">
/// <item>Reset() メソッドを追加</item>
/// <item>コンストラクタ引数に渡されたフィールド・プロパティを自動的に GetComponent を用いて初期化</item>
/// <item>VContainerUtility.TryAddAutoInjectGameObjects(gameObject) を呼び出す</item>
/// </list>
/// </summary>
public class ConfigureComponentAttribute : Attribute
{
    public ConfigureComponentAttribute(params string[] fieldNames)
    {
        FieldNames = fieldNames;
    }

    public IEnumerable<string> FieldNames { get; }
    public bool ShouldAutoInject { get; set; }
}
