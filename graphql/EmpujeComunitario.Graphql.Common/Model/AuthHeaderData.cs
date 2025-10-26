using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmpujeComunitario.Graphql.Common.Model
{
    [XmlRoot(Namespace = "auth.headers")]
    public class AuthHeaderData
    {
        [XmlElement("Grupo")]
        public string Grupo { get; set; }

        [XmlElement("Clave")]
        public string Clave { get; set; }
    }
}
