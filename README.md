# parser
Simple html xml parser

To run console app you must have dotnet SDK
After installing SDK, navigate to ParserMain project and use "dotnet run" command to run application

ex. For file:  
  dotnet run "C:\test.xml"  
  
For web page:  
  dotnet run http://delfi.lt  
  dotnet run http://www.15min.lt  

Known issues:  
  exceptions is not handled well.  
  Web project does not show api exceptions  
  Gathering all different statistics - traversing document from start (could be done with one traverse unless we would use different statistics separately)
