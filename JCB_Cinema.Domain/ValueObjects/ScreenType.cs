using JCB_Cinema.Tools;
using System.ComponentModel;

namespace JCB_Cinema.Domain.ValueObjects
{
    /// <summary>
    /// Represents the different types of screens available for movie screenings.
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter<ScreenType>))]
    public enum ScreenType
    {
        /// <summary>
        /// Represents a standard 2D screen.
        /// </summary>
        [Description("2D")]
        TwoD,   // 2D

        /// <summary>
        /// Represents a 3D screen.
        /// </summary>
        [Description("3D")]
        ThreeD, // 3D

        /// <summary>
        /// Represents an IMAX screen.
        /// </summary>
        [Description("IMAX")]
        IMAX    // IMAX
    }
}
