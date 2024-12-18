﻿namespace JCB_Cinema.Domain.Entities
{
    public class Photo : EntityBase
    {
        public int Id { get; set; }
        public byte[]? Bytes { get; set; }
        public string? Description { get; set; }
        public string FileExtension { get; set; } = null!;
        public double? Size { get; set; }

        public override object Key => Id;
    }
}
