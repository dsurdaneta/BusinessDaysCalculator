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
        private static readonly int DateIndex = 0;
        private static readonly int NameIndex = 1;
        private static readonly int DescriptionIndex = 2;

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

            return HolidaysFromXml(absoluteFilePath);
        }

        protected static List<Holiday> HolidaysFromXml(string fullFilePath)
        {
            List<Holiday> holidays = new List<Holiday>();
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

                        holidays.Add(holidayBuilder.Build());
                    }
                }
            }
            return holidays;
        }
    }
}
