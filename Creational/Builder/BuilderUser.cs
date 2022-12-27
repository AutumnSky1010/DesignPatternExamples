using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFDesignPatternExamples.Creational.BuilderPattern;
public class BuilderUser : IUser
{
    public void Use()
    {
        var builder = new CallInformationBuilder(new List<(string, string)>()
        {
            ("太郎", "職員室"),
            ("花子", "体育館裏")
        });
        var information = new InformationDirector(builder).Construct();
        Console.WriteLine(information);
    }
}
