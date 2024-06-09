using EnovationApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.Models
{
    public class Nft
    {
        public string NftId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Metadata
    {
        public string Author { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImageUrl { get; set; }
    }

    public class NftCollection
    {
        public List<Nft> Result { get; set; }
    }
}
