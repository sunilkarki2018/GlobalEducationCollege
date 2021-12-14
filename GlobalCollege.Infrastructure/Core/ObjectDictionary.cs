using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure.Core
{
    public static class ObjectDictionary
    {
        public static Dictionary<string, string> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
                .Union(
                    myObj.GetType()
                    .GetFields()
                    .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
                 )
                .ToDictionary(ks => ks.Name, vs => vs.Value.ToString());
        }
    }
}
