using GoFDesignPatternExamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternExamples;
public class DesignPattern
{
    public DesignPattern(string name, IUser user)
    {
        this.Name = name;
        this.User = user;
    }

    public string Name { get; }

    public IUser User { get; }
}
