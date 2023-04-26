using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace client.Model
{
    [Table("Coffee")]
    [Serializable]
    public class Coffee
    {
        [Column("id")]
        public int coffeeId { get; set; }
//--------------------------------------------------------------------
        [Column("uuId")]
        public string uuId { get; set; }
//--------------------------------------------------------------------
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
        public string description { get; set; }
//--------------------------------------------------------------------
        public string image { get; set; }
//--------------------------------------------------------------------
        [Column("sellerId")]
        [ForeignKey("sellerId")]
        public int sellerId { get; set; }//Faz com que o Id do seller seja a FK que atribui um produto a um seller existente.
    }
}