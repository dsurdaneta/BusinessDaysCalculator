using System;
using System.ComponentModel.DataAnnotations;

namespace DsuDev.BusinessDays.DataAccess.Models.Base;

/// <summary>
/// Base class for all database entities. with general porpouse fields.
/// </summary>
public abstract class DbModelBase
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    /// <value>
    /// The created date.
    /// </value>
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    /// <value>
    /// The updated date.
    /// </value>
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }
}
