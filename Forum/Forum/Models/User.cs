﻿using Microsoft.AspNetCore.Identity;

namespace Forum.Models;

public class User : IdentityUser<int>
{
    public string Avatar { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public ICollection<Message>? Messages { get; set; }
    
    public int MessageCount { get { return Messages?.Count() ?? 0; } }
}