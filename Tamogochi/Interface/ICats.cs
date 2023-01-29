using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamogochi.Interface;

public interface ICats
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Health { get; set; }
    public int Mood { get; set; }
    public int Satiety { get; set; }
    public int Level { get; set; }

}
