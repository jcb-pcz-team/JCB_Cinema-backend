﻿namespace JCB_Cinema.Application.Requests
{
    public class PutAppUserDetails
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? Town { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DialCode { get; set; }
    }
}