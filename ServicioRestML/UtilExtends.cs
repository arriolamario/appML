using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ServicioRestML
{
    public static class UtilExtends
    {
        public static T Deserealizar<T>(this string valor)
        {
            return JsonConvert.DeserializeObject<T>(valor);
        }
    }
}
