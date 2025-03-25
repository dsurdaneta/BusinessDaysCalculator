using System;
using System.ComponentModel.DataAnnotations;
using DsuDev.BusinessDays.DataAccess.Models.Base;

namespace DsuDev.BusinessDays.DataAccess.Models;

/// <summary>
/// The Holiday DbModelBase
/// </summary>
/// <seealso cref="DbModelBase" />
public class Holiday : DbModelBase
{
    /// <summary>
    /// Gets or sets the year.
    /// </summary>
    /// <value>
    /// The year.
    /// </value>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the holiday date.
    /// </summary>
    /// <value>
    /// The holiday date.
    /// </value>
    [DataType(DataType.Date)]
    public DateTime HolidayDate { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>
    /// The description.
    /// </value>
    [StringLength(100)]
    public string Description { get; set; }
}
