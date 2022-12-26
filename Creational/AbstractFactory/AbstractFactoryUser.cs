using DesignPatternExamples.Creational.AbstractFactory.Infrastructure.Abstractions;
using DesignPatternExamples.Creational.AbstractFactory.Infrastructure;

namespace DesignPatternExamples.Creational.AbstractFactory;
public class AbstractFactoryUser
{
    public void Use()
    {
        // 今回は２種類とも生成しているが、引数でファクトリの種類を切り替えることができる。
        var wavFileFactory = FileFactorySelector.Select(FileType.Wav);
        var txtFileFactory = FileFactorySelector.Select(FileType.Txt);

        var wavFileData = wavFileFactory.CreateDataGenerator().Generate();
        wavFileFactory.CreateFile().Write(wavFileData);

        var txtFileData = txtFileFactory.CreateDataGenerator().Generate();
        txtFileFactory.CreateFile().Write(txtFileData);
    }
}
