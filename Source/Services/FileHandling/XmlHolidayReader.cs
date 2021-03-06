﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using DsuDev.BusinessDays.Common.Constants;
using DsuDev.BusinessDays.Common.Tools.FluentBuilders;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Interfaces.FileHandling;

namespace DsuDev.BusinessDays.Services.FileHandling
{
    /// <summary>
    /// A class to read the holiday information from a XML file
    /// </summary>
    public class XmlHolidayReader : FileReaderBase, IXmlReader
    {
        public List<Holiday> Holidays { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlHolidayReader"/> class.
        /// </summary>
        public XmlHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        /// <inheritdoc />
        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
        {
            ValidatePath(absoluteFilePath, FileExtension.Xml);

            return this.ReadHolidaysFromFile(absoluteFilePath);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override List<Holiday> ReadHolidaysFromFile(string absoluteFilePath)
        {
            this.Holidays = new List<Holiday>();
            using (XmlReader file = XmlReader.Create(absoluteFilePath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                var holidayBuilder = new HolidayBuilder();
                foreach (XmlNode node in xDoc.ChildNodes[1])
                {
                    if (node.ChildNodes.Count >= 3)
                    {
                        holidayBuilder.Create()
                            .WithDate(Convert.ToDateTime(node.ChildNodes[FieldIndex.Date].InnerText))
                            .WithName(node.ChildNodes[FieldIndex.Name].InnerText)
                            .WithDescription(node.ChildNodes[FieldIndex.Description].InnerText);

                        this.Holidays.Add(holidayBuilder.Build());
                    }
                }
            }
            return this.Holidays;
        }
    }
}
