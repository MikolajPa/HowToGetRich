﻿using System.ComponentModel.DataAnnotations;

namespace EnovationAssignment.Models
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool isDistributor { get; set; }
        public string walletId { get; set; }
        public string accountId { get; set; }
    }

    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDistributor { get; set; }
        public string WalletId { get; set; }
        public string AccountId { get; set; }
    }

}
