/**
 * 【Singletonパターン】
 * インスタンスが一つであることを保証する。
 * ※行16を理解する必要がある。
 * 
 * 【シングルトンパターンが適さない例】
 * ・マルチスレッド環境下　⇒　同期処理をしないといけない場合がある
 * ・多くの状態を持つ　⇒　グローバル変数と同様になる
 * ・単体テストの順序に制約を生む　⇒　テストコードが書きにくくなる
 * ・常に決まった値を渡す・出力する　⇒　クラスメソッドにすべき
 * 
 * 参照：https://debimate.jp/2020/04/26/%E3%80%90singelton%E3%83%91%E3%82%BF%E3%83%BC%E3%83%B3%E3%80%91%E8%80%83%E3%81%88%E6%96%B9%E3%81%AF%E5%8D%98%E7%B4%94%E3%81%A0%E3%81%8C%E3%80%81%E4%BD%BF%E3%81%84%E3%81%A9%E3%81%93%E3%82%8D%E3%81%8C/
 */
/**
 * 【静的クラスを使わない理由】
 * 静的クラスで事足りる機能を実装したいが、インターフェイスを実装できないから、インスタンスメソッドとして実装する。
 * この時、複数のインスタンスの作成を避けるためにシングルトンパターンを用いる。
 * ⇒　「継承」や「インターフェイスの実装」が絡んでいないとシングルトンパターンの恩恵は少ない。
 * 　　具体的には、上位レイヤからログを出力したい時に使えると思われる。
 * 
 * static class Logger : IShowable { }
 * ↑
 * エラー！！！！
 * 
 * 参照：https://qiita.com/kyabetsuda/items/e570bdb61b1345f5f5a8
 * 
 */

namespace DesignPatternExamples.Creational.SingletonPattern;
interface IShowable
{
	void ShowWithTime(string content);
}
class Logger : IShowable
{
	// 自身を自身で持つ
	public static Logger Instance { get; } = new Logger();

	// コンストラクタを隠蔽し、外部からLoggerのインスタンスを生成することを禁止する。
	private Logger() { }

	// 例）時刻（ローカル）と共にcontentを出力する
	public void ShowWithTime(string content) => Console.WriteLine($"{DateTime.Now}\t{content}");
}

