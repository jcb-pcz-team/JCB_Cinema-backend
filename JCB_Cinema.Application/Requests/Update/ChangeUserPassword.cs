﻿namespace JCB_Cinema.Application.Requests.Update
{
    public class ChangeUserPassword
    {
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
