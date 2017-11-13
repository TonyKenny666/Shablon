using Shablon;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProject.Models
{
    public class OrderModel
    {
        [Template(PlaceHolder = "Name")]
        public string Name { get; set; }

        [Template(PlaceHolder = "Address")]
        public string Address { get; set; }

        [Template(CollectionStart = "StartOrderLines", CollectionEnd = "EndOrderLines")]
        public List<OrderLineModel> OrderLines { get; set; }

        [Template(CollectionStart = "ContactInfoStart", CollectionEnd = "ContactInfoEnd")]
        public NameValueCollection ContactInfo{ get; set; }

        [Template(CollectionStart = "OfficesStart", CollectionEnd = "OfficesEnd", PlaceHolder = "Office")]
        public List<string> Offices { get; set; }
    }
}
