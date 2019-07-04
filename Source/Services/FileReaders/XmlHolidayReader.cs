using System;
using System.Collections.Generic;
using System.Xml;
using DsuDev.BusinessDays.Domain.Entities;
using DsuDev.BusinessDays.Tools.Constants;
using DsuDev.BusinessDays.Tools.FluentBuilders;

namespace DsuDev.BusinessDays.Services.FileReaders
{
    public class XmlHolidayReader : IXmlReader
    {
        public List<Holiday> Holidays { get; set; }

        public XmlHolidayReader()
        {
            this.Holidays = new List<Holiday>();
        }

        public List<Holiday> GetHolidaysFromFile(string absoluteFilePath)
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

        protected List<Holiday> HolidaysFromXml(string absoluteFilePath)
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
