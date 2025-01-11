using JCB_Cinema.Tools;
using System.ComponentModel;

namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents the various genres of movies available in the cinema system.
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter<Genre>))]
    public enum Genre
    {
        /// <summary>
        /// Action genre, typically featuring fast-paced, high-energy sequences.
        /// </summary>
        [Description("Action")]
        Action,

        /// <summary>
        /// Adventure genre, often involving an exciting journey or quest.
        /// </summary>
        [Description("Adventure")]
        Adventure,

        /// <summary>
        /// Comedy genre, intended to entertain and amuse the audience.
        /// </summary>
        [Description("Comedy")]
        Comedy,

        /// <summary>
        /// Drama genre, focusing on emotional and relational conflicts.
        /// </summary>
        [Description("Drama")]
        Drama,

        /// <summary>
        /// Horror genre, designed to invoke fear and suspense in the audience.
        /// </summary>
        [Description("Horror")]
        Horror,

        /// <summary>
        /// Thriller genre, characterized by tension, suspense, and excitement.
        /// </summary>
        [Description("Thriller")]
        Thriller,

        /// <summary>
        /// Science Fiction genre, often set in futuristic or speculative worlds.
        /// </summary>
        [Description("Science Fiction")]
        ScienceFiction,

        /// <summary>
        /// Fantasy genre, often involving magical or supernatural elements.
        /// </summary>
        [Description("Fantasy")]
        Fantasy,

        /// <summary>
        /// Animation genre, typically involving animated characters and scenes.
        /// </summary>
        [Description("Animation")]
        Animation,

        /// <summary>
        /// Documentary genre, focusing on factual information or real-life events.
        /// </summary>
        [Description("Documentary")]
        Documentary,

        /// <summary>
        /// Crime genre, focusing on criminal activities and investigations.
        /// </summary>
        [Description("Crime")]
        Crime,

        /// <summary>
        /// Romance genre, focusing on romantic relationships and love stories.
        /// </summary>
        [Description("Romance")]
        Romance,

        /// <summary>
        /// Musical genre, typically featuring song and dance sequences.
        /// </summary>
        [Description("Musical")]
        Musical,

        /// <summary>
        /// War genre, focused on military conflict and its impact.
        /// </summary>
        [Description("War")]
        War,

        /// <summary>
        /// Spy genre, often involving espionage and secret agents.
        /// </summary>
        [Description("Spy")]
        Spy
    }
}
