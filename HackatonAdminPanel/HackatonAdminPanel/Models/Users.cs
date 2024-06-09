using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackatonAdminPanel.Models;
public class Users
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool isDistributor { get; set; }
    public string walletId { get; set; }
}
