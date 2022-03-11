
/* 
 * 【テンプレートメソッドパターン】
 * スーパークラスで処理の「手順」を定義したテンプレートメソッドを用意して、具体的な処理はサブクラスに行わせる。
 * 処理手順は同じだが具体的な処理が異なる場合に用いられる。
 * 
 * 【メリット　⇔　デメリット】
 * サブクラスで抽象メソッドを実装するだけで具体的な処理を定義できるので拡張性が高まる。　⇔　サブクラスの数が増えやすい。
 * 全ての工程(アルゴリズム)はスーパークラス、一つ一つの工程はサブクラスで定義されているので、焦点を当てる部分を分離できる（全体の工程と一つ一つの工程の処理を同時に考えないで済む）
 * ⇔　サブクラスを実装する時にそれぞれの工程で何をする必要があるのか(責任)を完全に理解している必要がある。
 */
namespace DesignPatternExamples.Behavioural.TemplateMethod;
// 所謂、AbstractClass
abstract class TextDecorationBase
{
	public TextDecorationBase(string text)
	{
		this.Text = text;
	}

	public string Text { get; set; } = "";
	
	// 所謂テンプレートメソッド。利用者はこのメソッドを呼び出す。
	public string Decorate() => $"{this.DecorateTop()}\n{this.DecorateSide()}\n{this.DecorateBottom()}";

	// 下の三つのメソッドが具体的な処理。サブクラスに実装させる。
	protected abstract string DecorateTop();

	protected abstract string DecorateSide();

	protected abstract string DecorateBottom();
}
// 所謂ConcreteClass。今回の例では半角文字列を「+」で囲う装飾を行う。
class DecorationPlus : TextDecorationBase
{
	public DecorationPlus(string text) : base(text) { }

	protected override string DecorateTop() => this.DecorateBottom();

	protected override string DecorateSide() => $"+{this.Text}+";

	protected override string DecorateBottom() => new string('+', this.Text.Length + 2);
}

// 所謂ConcreteClass。今回の例では、半角文字列を「+*」を繰り返したもので囲う装飾を行う。
class DecorationPlusAndAsterisk : TextDecorationBase
{
	public DecorationPlusAndAsterisk(string text) : base(text) { }
	protected override string DecorateTop() => this.DecorateBottom();

	protected override string DecorateSide() => $"+{this.Text}*";

	protected override string DecorateBottom()
	{
		string result = string.Empty;
		for (int i = 0; i < this.Text.Length + 2; i++)
		{
			if (i % 2 == 0)
			{
				result += '+';
				continue;
			}
			result += '*';
		}
		return result;
	}
}

// Decorationクラスを使うクラスの例
class DecorationClassUser
{
	public void Use()
	{
		TextDecorationBase plusDeco = new DecorationPlus("abcdefgh123");
		TextDecorationBase asteriskDeco = new DecorationPlusAndAsterisk("abcdefgh123");

		// 使うときはテンプレートメソッドを呼び出す。
		Console.WriteLine(plusDeco.Decorate());
		Console.WriteLine(asteriskDeco.Decorate());
	}
}