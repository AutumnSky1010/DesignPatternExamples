using GoFDesignPatternExamples.Creational.SingletonPattern;
using GoFDesignPatternExamples.Creational.FactoryMethod;
using GoFDesignPatternExamples.Behavioural.TemplateMethod;
using GoFDesignPatternExamples;
using GoFDesignPatternExamples.Creational.BuilderPattern;
using GoFDesignPatternExamples.Creational.AbstractFactory;
using GoFDesignPatternExamples.Creational.Prototype;
using DesignPatternExamples;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace GoFDesignPatternExamples;
class Program
{
	private static List<DesignPattern> CreationalPatterns = new()
	{
		new DesignPattern("Abstract Factory", new AbstractFactoryUser()),
        new DesignPattern("Factory Method", new FactoryMethodUser()),
        new DesignPattern("Builder", new BuilderUser()),
        new DesignPattern("Prototype", new PrototypeUser()),
        new DesignPattern("Singleton", new SingletonUser())
	};

	private static List<DesignPattern> StructuralPatterns = new();

	private static List<DesignPattern> BehaviouralPatterns = new()
	{
		new DesignPattern("Template Method", new TemplateMethodUser())
	};

	private static int CountOfPatterns
	{
		get
		{
			return CreationalPatterns.Count + StructuralPatterns.Count + BehaviouralPatterns.Count;
		}
	}

	private static void Main()
	{
        bool isEnd = false;
        do
        {
			Console.WriteLine("実行したいデザインパターンの例の番号を入力してください。数字以外を入力すると終了します。");
			ShowAllPatterns();
			string? input = Console.ReadLine();
			if (input is null || !int.TryParse(input, out int inputIndex) || inputIndex >= CountOfPatterns)
			{
				Console.WriteLine("終了します。");
				isEnd = true;
			}
			else
			{
				Console.WriteLine();
				UsePattern(inputIndex);
				Console.WriteLine("続行する場合はエンターキーを押してください。");
				Console.ReadLine();
			}
			
        } while (!isEnd);
    }

	private static void ShowAllPatterns()
	{
		int firstIndex = 0;
		Console.WriteLine("オブジェクトの生成に関するパターン");
		ShowOneTypePatterns(CreationalPatterns);
		Console.WriteLine("オブジェクトの構造に関するパターン");
		firstIndex += CreationalPatterns.Count;
        ShowOneTypePatterns(StructuralPatterns, firstIndex);
		firstIndex += StructuralPatterns.Count;
		Console.WriteLine("オブジェクトの振る舞いに関するパターン");
		ShowOneTypePatterns(BehaviouralPatterns, firstIndex);
	}

	private static void UsePattern(int index)
	{
		var patterns = CreationalPatterns.Concat(StructuralPatterns).ToArray();
		patterns = patterns.Concat(BehaviouralPatterns).ToArray();
		patterns[index].User.Use();
	}

	private static void ShowOneTypePatterns(IReadOnlyList<DesignPattern> patterns, int count = 0)
	{
		for (int i = 0; i < patterns.Count; i++, count++)
		{
			var pattern = patterns[i];
			Console.WriteLine($"{count,3}|{pattern.Name}");
		}
    }
}