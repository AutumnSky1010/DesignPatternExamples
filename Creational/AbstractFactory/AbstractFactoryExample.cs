
using GoFDesignPatternExamples.Creational.AbstractFactory.Infrastructure.Abstractions;
/**
* 【Abstract Factoryパターン】
* 関連するインスタンス群をまとめ、生成するクラスを実装する。
* 【メリット】
* ・生成するインスタンス群の組み合わせを間違えなくなる。
* ・FactoryのFactoryを実装すると動的に、使用するオブジェクト群を切り替えることができる。
* ・インスタンス群の切り替えが行いやすくなる。
* 
* 【デメリット】
* ・コード量が増える
* ┗特定の組み合わせの制約があるインスタンス群を扱わない限り、このパターンは使わない。
* 
* 【Factory Methodパターンとの違い】
* Factory Methodパターンは「メソッド単位」の抽象化だが、Abstract Factoryパターンは「クラス単位」での抽象化となる。
* ※本リポジトリでのFactory Methodパターンの例ではFactoryクラスとして外に切り出しているので必ずしもそうとは言えない。
* そのため、Abstract Factoryパターンは実装のコストが大きくなるが、使いまわしはききやすい。
* 　⇒「抽象の段階」を考える必要がある。
*/
namespace GoFDesignPatternExamples.Creational.AbstractFactory.Infrastructure.Abstractions
{
    public abstract class FileFactoryBase
    {
        public abstract IFile CreateFile();

        public abstract IDataGenerator CreateDataGenerator();
    }

    public interface IDataGenerator
    {
        byte[] Generate();
    }
    
    public interface IFile
    {
        void Write(byte[] data);
    }
}

namespace GoFDesignPatternExamples.Creational.AbstractFactory.Infrastructure
{
    public enum FileType { Wav, Txt };
    public class FileFactorySelector
    {
        /**
         * Factoryの生成知識をここに集約することで、
         * 利用者は動的に生成するオブジェクトを切り替えられるようになる。
         */
        public static FileFactoryBase Select(FileType fileType)
        {
            return fileType switch
            {
                FileType.Wav => new WavFileFactory(),
                FileType.Txt => new TxtFileFactory(),
                _ => throw new ArgumentException()
            };
        }
    }

    public class WavFileFactory : FileFactoryBase
    {
        public override IDataGenerator CreateDataGenerator()
             => new WavDataGenerator();

        public override IFile CreateFile()
             => new WavFile();
    }

    public class TxtFileFactory : FileFactoryBase
    {
        public override IDataGenerator CreateDataGenerator()
            => new TxtDataGenerator();

        public override IFile CreateFile()
            => new TxtFile();
    }

    public class TxtDataGenerator : IDataGenerator
    {
        public byte[] Generate()
        {
            Console.WriteLine("txtファイル用のデータを生成しました。");
            return new byte[100];
        }
    }

    public class TxtFile : IFile
    {
        public void Write(byte[] data)
        {
            Console.WriteLine("txtファイルに書き込みました。");
        }
    }

    public class WavDataGenerator : IDataGenerator
    {
        public byte[] Generate()
        {
            Console.WriteLine("wavファイル用のデータを生成しました。");
            return new byte[100];
        }
    }

    public class WavFile : IFile
    {
        public void Write(byte[] data)
        {
            Console.WriteLine("wavファイルに書き込みました。");
        }
    }
}
