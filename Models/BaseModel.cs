using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace signalR101.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
