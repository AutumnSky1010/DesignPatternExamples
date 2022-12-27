using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFDesignPatternExamples.Creational.Prototype;
public class PrototypeUser : IUser
{
    public void Use()
    {
        var manager = new ComponentManager();
        string key = "prototypeButton";
        manager.RegisterButton(key);
        
        for (int i = 0; i < 10; i++)
        {
            IComponent component = manager.GetClonedComponent(key);
            component.Name = $"Button{i}";
            component.Draw();
            Console.WriteLine();
        }
    }
}
