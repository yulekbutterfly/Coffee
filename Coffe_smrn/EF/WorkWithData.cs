using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffe_smrn.EF
{
    public partial class Employee
    {
        public string FIO { get => $"{SecondName} {FirstName} {Patronumic}"; }
    }
}
