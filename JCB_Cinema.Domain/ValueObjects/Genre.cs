﻿using System.ComponentModel;

namespace JCB_Cinema.Domain.ValueObjects
{
    public enum Genre
    {
        [Description("Action")]
        Action,

        [Description("Adventure")]
        Adventure,

        [Description("Comedy")]
        Comedy,

        [Description("Drama")]
        Drama,

        [Description("Horror")]
        Horror,

        [Description("Thriller")]
        Thriller,

        [Description("Science Fiction")]
        ScienceFiction,

        [Description("Fantasy")]
        Fantasy,

        [Description("Animation")]
        Animation,

        [Description("Documentary")]
        Documentary,

        [Description("Crime")]
        Crime,

        [Description("Romance")]
        Romance,

        [Description("Musical")]
        Musical
    }
}