using System.Reflection.Metadata;
using System.Text;

namespace GoFDesignPatternExamples.Creational.BuilderPattern;
/**
 * 【Builderパターン】
 * オブジェクトの生成過程を抽象化したパターン。
 * コンストラクタの引数の数が多く、デフォルト引数が多い場合に使える。
 * また、「どれか一つでもプロダクトクラスのフィールドの初期化が欠けていると動作しない」というクラスでは使うことは無さそう。
 * 　↑Builderの全てのメソッドが必ずしも呼ばれるとは限らないうえに、その制約は分かりづらいから。
 * 　Directorクラスを用いることで、初期化忘れを防ぐことは可能（Directorクラスの実装が間違っていた場合は失敗するが）。
 * 
 * 上のユースケースからははずれるが、Informationクラスのように、汎用的な使い方をするオブジェクトの生成に使うこともできると考えた。
 * 今回の「呼び出し情報」を作成する例では、「オブジェクトの使い方」の知識をBuilderクラスに隠蔽している。
 * 
 * つまり、全く同じ構造だが、別の性質を持つインスタンスの生成に使うことができる。
 * (今回のように、中身の文字列で性質が変わるような場合は、「型」の仕組みで分離することが困難。型で分離できる場合は、抽象クラスを用いて各クラスに派生する形で実装する。)
 * 【メリット】
 * オブジェクトの生成を柔軟に行える。
 * 
 * 【デメリット】
 * コードの利用者にBuilderの存在を知らせる必要がある。
 */

/**
 * BuilderのI/F
 */
public interface IInformationBuilder
{
    void SetTitle();

    void SetFields();

    Information Build();
}

/**
 * Directorクラスに生成工程を実装
 */
public class InformationDirector
{
    public InformationDirector(IInformationBuilder builder)
    {
        this.Builder = builder;
    }

    private IInformationBuilder Builder { get; }

    public Information Construct()
    {
        this.Builder.SetTitle();
        this.Builder.SetFields();
        return this.Builder.Build();
    }
}

/**
 * Builderの具象クラス。今回の例では、呼び出し情報を作成するBuilderとして実装している。
 */
public class CallInformationBuilder : IInformationBuilder
{
    public CallInformationBuilder(IReadOnlyCollection<(string targetPersonName, string location)> data)
    {
        this.Data = data;
    }

    private IReadOnlyCollection<(string targetPersonName, string location)> Data { get; }

    private List<Field> Fields { get; set; } = new();

    private string Title { get; set; } = "";

    public Information Build()
    {
        return new Information(this.Title, this.Fields);
    }

    public void SetFields()
    {
        var fields = new List<Field>(this.Data.Count);
        foreach (var datum in this.Data)
        {
            var field = new Field()
            {
                Title = $"{datum.targetPersonName}さんの呼び出し",
                Content = $"{datum.targetPersonName}さんは至急{datum.location}まで来てください。"
            };
            fields.Add(field);
        }
        this.Fields = fields;
    }

    public void SetTitle()
        => this.Title = "呼び出し情報";
}

/*
 * 以下生成物関連のクラス
 */
public class Field
{
    public string Title { get; init; } = "";

    public string Content { get; init; } = "";
}

public class Information
{
    public Information(string title = "", List<Field>? fields = null)
    {
        this.Title = title;
        fields ??= new List<Field>();
        this.Fields = fields;
    }

    public string Title { get; }

    private List<Field> Fields { get; }

    public void AddField(Field field) => this.Fields.Add(field);

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"【{this.Title}】\n");
        foreach ( var field in this.Fields)
        {
            builder.AppendLine($"<{field.Title}>");
            builder.AppendLine($"{field.Content}\n");
        }
        return builder.ToString();
    }
}