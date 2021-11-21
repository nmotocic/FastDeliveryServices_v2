import json
import pandas as pd
import pyodbc as pdb


#Import CSV
data = pd.read_csv("Data/airports.csv")
data_frame = pd.DataFrame(data)

with open("Data/config.json") as config:
    connection_string_json = json.load(config)
    connection_string = connection_string_json["connection_string"]


#Connect to SQL server
conn = pdb.connect(connection_string)

cursor = conn.cursor()

#Insert the frame into Airports Table
for row in data_frame.itertuples():
    cursor.execute(''' 
                    INSERT INTO Airports (IATA, AirportName, Latitude, Longitude)
                    VALUES(?,?,?,?)
                    ''',
                    row.IATA,
                    row.AIRPORT,
                    row.LATITUDE,
                    row.LONGITUDE
                    )

conn.commit()

