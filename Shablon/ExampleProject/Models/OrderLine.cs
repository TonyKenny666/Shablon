using Shablon;
using System;

namespace ExampleProject.Models
{
    public class OrderLineModel
    {
        [Template(PlaceHolder = "ID")]
        public int ID { get; set; }

        [Template(PlaceHolder ="Description")]
        public string Description { get; set; }

        [Template(PlaceHolder = "Price")]
        public double Price { get; set; }

        [Template(PlaceHolder = "Quantity")]
        public int Quantity { get; set; }

        [Template(PlaceHolder = "Total")]
        public double Total { get; set; }
    }
}
