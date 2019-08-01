# BusinessDaysCalculator
Library to calculate dates by Business Days (Adding weekends and holidays).
Supports different file formats to load the Holidays.

# JSON example:
```sh
{
  "Holidays":
  [
    {
      "HolidayDate": "2011-05-01",
      "Name": "Workers Day",
      "Description": ""
    },
    {
      "HolidayDate": "2011-12-25",
      "Name": "Christmas",
      "Description": ""
    }
  ]
}
```

# CSV example:
```sh
HolidayDate; Name; Description
"2217-12-25"; "Christmas"; "Xmas"
"2217-05-01"; "Workers Day"; " "
```

# XML example:
```sh
<holidays>
  <Holiday>
    <HolidayDate>2017-05-01</HolidayDate>
    <Name>Workers Day</Name>
    <Description> </Description>
  </Holiday>
  <Holiday>
    <HolidayDate>2017-12-25</HolidayDate>
    <Name>Christmas</Name>
    <Description>Xmas</Description>
  </Holiday>
</holidays>
```
