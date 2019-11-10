using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGamesClient.Models
{
    public class Registration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
