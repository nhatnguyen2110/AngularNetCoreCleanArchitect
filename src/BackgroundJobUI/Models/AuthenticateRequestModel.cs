﻿using System.ComponentModel.DataAnnotations;

namespace BackgroundJobUI.Models;

public class AuthenticateRequestModel
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}
