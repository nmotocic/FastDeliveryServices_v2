# FastDeliveryServices

## Database

`FAD/Database scripts`

* contains SQL scripts for making the tables in SQL Server

`FAD/Data`

* `airports.csv` - CSV file with American airports (link provided in the task document didn't include full list of IATA codes)
* `dbImport.py` - Python script for importing airport data into database
* `requirements.txt` - list of Python libraries needed for script to work
* `config.json` - configuration file for connection string **NOTE:** Change it accordingly to the setup

NOTE: If there's no python present on the pc this will be tested on, head into `build/dbImport` folder and find `dbImport.exe` which should 
populate the database if configuration string is correct

## Web API

`FAD/FAD`

ASP.NET Web Api for "FastDeliveryServices"

**NOTE:** Change the connection string in `App.config` accordingly

Swagger is included in the project, so API can be manually tested in browser as well.




 
