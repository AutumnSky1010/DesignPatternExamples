using DesignPatternExamples.Creational.FactoryMethod.Domain;
/**
* 【Factory Methodパターン】
* Virtual Constructorパターンとも呼ばれる。
* オブジェクトの作り方をファクトリの抽象クラスないしI/Fを継承したサブクラスに任せる。
* そうすることで、アプリケーションに特化した処理を記述しているのにもかかわらず再利用が簡単になり、
* それを目的とする。
* 
* 【メリット】
* ・具象クラスに依存せずにインスタンスを生成出来るので、「依存性の逆転」を実現できる。
* 　┗間にインターフェイスを挟んでいるので、呼び出し側、具象クラスはインターフェイスに依存する。
* ・上位レイヤに実装出来ない知識を下位レイヤで拡張しつつ、それを上位レイヤで生成・利用できる。
* 
* 【デメリット】
* ・コード量が増える
* 　┗シンプルではなくなるうえに依存性の逆転は必ず守る必要があるものではないので、使いどころを見極める必要がある。
* 　　具体例：階層化アーキテクチャを採用し、レイヤ間の依存方向を一方向にそろえたいとき　かつ　下位レイヤで拡張する必要が生まれた場合
* 
* 【なぜファクトリを使うのか？】
* DIだけでもプロダクトクラスの拡張は行えるが、以下の問題があると考える。
* ・初期化でたくさんのサブクラスが必要になる場合、コンストラクタで行うにしては煩雑かつ、意図が読めない。
* ・用途ごとにコンストラクタをオーバーロードしても、各コンストラクタの目的がコードから読み取れない。
* ・プロダクトクラスの初期化方法の変更の影響を強く受ける。
* 
* このように、コンストラクタだけで表現できる情報には限りがある。また、生成方法の知識を自身だけに持たせると分かりづらくなってしまう場合がある。
* それを回避する策としてファクトリを使う。
* ・生成の処理をコンストラクタから追い出す事で、プロダクトクラスが表すものの表現だけに集中できる。
* ・生成の処理に名前を付けられるため、コードで表現できる知識を増やすことができる。（コンストラクタで表現できなかった内容も、ファクトリメソッドで表現する。）
* ・「生成の処理をどこで行うか？」という設計の判断によっても変わる。
*/
namespace DesignPatternExamples.Creational.FactoryMethod.Domain
{
    public class GeneralUser
    {
        public GeneralUser(int id) 
        {
            this.Id = id;
        }

        private int Id { get; }

        private List<PlanBase> Subscriptions { get; } = new List<PlanBase>();

        public IReadOnlyCollection<PlanBase> ReadOnlySubscriptions => this.Subscriptions;

        public int CalculateTotalAmount()
        {
            int sum = 0;
            foreach (var plan in Subscriptions)
            {
                sum += plan.MonthlyAmountYen;
            }
            return sum;
        }

        public void Subscribe(IPlanFactory planFactory)
        {
            this.Subscriptions.Add(planFactory.CreateNewPlan());
        }
    }

    /**
     * 具体的なプランについてのビジネスルールが無いとする。
     * そのプランの内容についてはアプリケーションに任せる。
     * （この結果、「プラン」を使うアプリケーションならば、この抽象クラスを再利用出来る可能性が高まる）
     */
    public abstract record PlanBase
    {
        public PlanBase(string name) => this.Name = name;

        public string Name { get; }

        public abstract int MonthlyAmountYen { get; }

        public DateTime EffectiveDate { get; init; }
    }
    
    /// <summary>
    /// Planを生成するファクトリのインターフェイス
    /// </summary>
    public interface IPlanFactory
    {
        PlanBase CreateNewPlan();
    }
}

namespace DesignPatternExamples.Creational.FactoryMethod.Application
{
    public record BasicPlan : PlanBase
    {
        public BasicPlan() : base("基本的なプラン") { }

        public override int MonthlyAmountYen { get; } = 500;
    }

    public class BasicPlanFactory : IPlanFactory
    {
        // ファクトリを使うことで、生成の処理に名前を付けることができる。DDDの文脈では、ユビキタス言語で生成メソッドの名前を付けると良いかもしれない。
        // 本来はもっと複雑な生成処理の時に使う事が多いと思われる。
        public PlanBase CreateNewPlan()
        {
            var effectiveDate = DateTime.Now;
            effectiveDate = effectiveDate.AddDays(30);
            var plan = new BasicPlan()
            {
                EffectiveDate = effectiveDate,
            };
            return plan;
        }
    }

    public class GeneralUserUseCase
    {
        private static Dictionary<int, GeneralUser> IdUserPairs = new()
        {
            { 1000, new GeneralUser(1000) }
        };

        public void SubscribeNewPlan(int userId, IPlanFactory factory)
        {
            var user = IdUserPairs[userId];
            user.Subscribe(factory);
            Console.WriteLine($"新規プランに加入しました。");
            Console.WriteLine($"ID: {userId}の加入プラン一覧");
            foreach (var plan in user.ReadOnlySubscriptions)
            {
                Console.WriteLine($"{plan.Name} 月額: {plan.MonthlyAmountYen}円\n期限: {plan.EffectiveDate}");
            }
        }
    }

}