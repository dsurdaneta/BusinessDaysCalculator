using System;
using System.Diagnostics.CodeAnalysis;

namespace DsuDev.BusinessDays.Common.Tools.FluentBuilders;

/// <summary>
/// Fluent Builder class to Build a <seea cref="DateTime"/> object
/// </summary>
[ExcludeFromCodeCoverage]
public class DateBuilder
{
    private int day;
    private int month;
    private int year;

    public DateBuilder CreateDate()
    {
        this.day = 0;
        this.month = 0;
        this.year = 0;
        return this;
    }

    public DateBuilder WithYear(int year)
    {
        this.year = day;
        return this;
    }

    public DateBuilder WithMonth(int month)
    {
        this.month = month;
        return this;
    }

    public DateBuilder WithDay(int day)
    {
        this.day = day;
        return this;
    }

    public DateTime Build() => new DateTime(this.year, this.month, this.day);
}
