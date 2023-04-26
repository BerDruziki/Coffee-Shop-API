
using System.ComponentModel.DataAnnotations.Schema;

public class coffeeSchema
    {
        [Column("name")]
        public string name { get; set; }
//--------------------------------------------------------------------
        [Column("category")]
        public string category { get; set;}
//--------------------------------------------------------------------
        [Column("price")]
        public double price { get; set; }
//--------------------------------------------------------------------
        [Column("promotionalPrice")]
        public double? promotionPrice { get; set; }
//--------------------------------------------------------------------
        [Column("description")]
        public string description { get; set; }
//--------------------------------------------------------------------
        [Column("image")]
        public string image { get; set; }
    }