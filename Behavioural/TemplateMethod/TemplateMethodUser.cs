using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples.Behavioural.TemplateMethod;
// Decorationクラスを使うクラスの例
public class TemplateMethodUser
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
