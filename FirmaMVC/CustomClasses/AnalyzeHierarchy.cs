using FirmaMVC.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaMVC.CustomClasses
{
    public class AnalyzeHierarchy
    {
        public bool czyMaszDzieci(int id, List<Department> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (id  == l[i].Parent)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
