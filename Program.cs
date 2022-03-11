using DesignPatternExamples.Creational.SingletonPattern;
using DesignPatternExamples.Behavioural.TemplateMethod;
using DesignPatternExamples;
namespace DesignPatternExamples;
class Program
{
	private static void Main()
	{
		DecorationClassUser user = new DecorationClassUser();
		user.Use();
	}
}