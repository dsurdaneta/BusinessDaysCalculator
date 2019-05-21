using System;
using System.Collections.Generic;
using System.Xml;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Services.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public class XmlHolidayReader : IHolidayFileReader
    {
        private const int DateIndex = 0;
        private const int NameIndex = 1;
        private const int DescriptionIndex = 2;

        public List<Holiday> Holidays { get; set; }

        public XmlHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> HolidaysFromFile(string absoluteFilePath)
        {
            if (string.IsNullOrWhiteSpace(absoluteFilePath))
            {
                throw new ArgumentException(nameof(absoluteFilePath));
            }

            if (!absoluteFilePath.EndsWith($".{FileExtension.Xml}"))
            {
                throw new InvalidOperationException($"File extension {FileExtension.Xml} was expected");
            }

            return this.HolidaysFromXml(absoluteFilePath);
        }

        protected List<Holiday> HolidaysFromXml(string fullFilePath)
        {
            this.Holidays = new List<Holiday>();
            using (XmlReader file = XmlReader.Create(fullFilePath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                var holidayBuilder = new HolidayBuilder();
                foreach (XmlNode node in xDoc.ChildNodes[1])
                {
                    if (node.ChildNodes.Count >= 3)
                    {
                        holidayBuilder.Create()
                            .WithDate(Convert.ToDateTime(node.ChildNodes[DateIndex].InnerText))
                            .WithName(node.ChildNodes[NameIndex].InnerText)
                            .WithDescription(node.ChildNodes[DescriptionIndex].InnerText);

                        this.Holidays.Add(holidayBuilder.Build());
                    }
                }
            }
            return this.Holidays;
        }
    }
}
