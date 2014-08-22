using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole
{
    public interface IWebRole
    {
        string RunInternalProcess(string stringParams);
    }
}
