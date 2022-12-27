using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoFDesignPatternExamples.Creational.Prototype;
/**
 * 【Prototypeパターン】
 * ひな形となる「オブジェクト」を基に複製して、オブジェクトを生成するパターン
 * 
 * 【メリット】
 * ・初期化に時間がかかるオブジェクトを何個も作らないといけない場合で、複製が可能な場合は、複製したほうが動作が早くなる場合がある。
 * ・クローンメソッドを呼び出すクラスは具体的な型名を知らないで使える。
 * 　　⇒new式を書く場所を減らせるとも言えそう。
 * 
 * 【デメリット】
 * ・ディープコピーが手間となる場合がある。(酷いとJsonかXmlかに一度シリアライズしてまたデシリアライズすることになる。)
 * 　　⇒　ディープコピーに時間がかかるならば効果は薄くなる。
 * ・クローン後のオブジェクトの一部を変更する場合、セッターを開放する必要がある。
 */

/**
 * クローン可能
 */
public interface ICloneable<T>
{
    T Clone();
}

public interface IComponent : ICloneable<IComponent>
{
    string Name { get; set; }

    void Draw();
}

public class Button : IComponent
{
    public Button(string name)
    {
        // 初期化で重い処理をしている(3秒)
        Task.Delay(3000).Wait();
        this.ButtonStrings = new string[3];
        string top = $"----------------";
        this.ButtonStrings[0] = top;
        this.Name = name;
        this.ButtonStrings[2] = top;
    }

    public Button(string[] buttonStrings)
    {
        this.ButtonStrings = buttonStrings;
    }

    private string _name = "";
    public string Name
    {
        get { return this._name; }
        set
        {
            this._name = value;
            this.ButtonStrings[1] = $"|{value,14}|";
        }
    }

    private string[] ButtonStrings { get; }

    public IComponent Clone()
    {
        var clonedStrings = new string[this.ButtonStrings.Length];
        for (int i = 0; i < this.ButtonStrings.Length; i++)
        {
            clonedStrings[i] = new string(this.ButtonStrings[i]);
        }
        return new Button(clonedStrings);
    }

    public void Draw()
    {
        foreach(var buttonString in this.ButtonStrings)
        {
            Console.WriteLine(buttonString);
        }
    }
}

public class ComponentManager
{
    private Dictionary<string, IComponent> KeyComponentPairs { get; } = new();

    public void RegisterButton(string key)
    {
        this.KeyComponentPairs.Add(key, new Button(""));
    }

    public IComponent GetClonedComponent(string key) => this.KeyComponentPairs[key].Clone();
}
