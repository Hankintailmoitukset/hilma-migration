# Hilma Migration example

**This example is meant for reference purposes and it is not actively maintained currently**

## Prerequisites
  
  * This is a .Net Standard 2.0 libary - Please see compatibility https://dotnet.microsoft.com/platform/dotnet-standard#versions
  
## Building

Run  `dotnet build` to build the solution

## Example app

The sample app accepts notice xml as input and outputs EtsContract as output

Instructions: give 4 arguments:
  * [0] Filename, give xml file as first argument. i.e: ./notice.xml
  * [1] Form number, i.e: 2
  * [2] Notice special type (string), if not known:  0, example: 'PRI_REDUCING_TIME_LIMITS'
  * [3] Notice OJS Number, if not applicable: null

To run the code:
  * navigate to `./Hilma.MigrationExample`
  * execute `dotnet run notice.xml 2 0 '2019/S 1234567'`
  
  
