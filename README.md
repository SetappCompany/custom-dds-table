# Custom Dynamic Data Store table in EpiServer 11

This is an implementation of a custom big table in Episerver's DDS and tests which compare performance of a custom DDS table and a default big table.

To run the solution open it in Visual Studio, go to Package Manager Console and run a command `Update-Package -reinstall`. When a question about replacing existing files appears, input `A` for all. Then click F5 to run it.

To run the tests, go to `/performance-tests` url. It may take about 20 minutes for the tests to run. After that time a page with results will be displayed. 