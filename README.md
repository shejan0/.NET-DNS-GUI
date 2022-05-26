# .NET based DNS Lookup, Pinger, and IP Information Tool
### by Shejan Shuza

Made for fun, tool to lookup Domain Name IP Addresses, Ping those IP Addresses, and reverse IP lookup information about the address using [IP-API](https://ip-api.com/)

BUILT FOR USE IN WINDOWS, Utilizes [JSON.NET](https://www.newtonsoft.com/json)

Built in Visual Studio Community 2019

## Map rendering

This application supports using MapBox, TomTom, and Bing Maps API keys to display a map of the geolocation of the selected IP.

By Default, the program will look for a file titled "keys.json", or the first command line argument for a path to a json formatted file.

The keys in the JSON are: "mapBoxApiKey", "tomTomApiKey", and "bingMapsApiKey" as string inputs

For example:
```
{
    "mapBoxApiKey": "Mapboxdddfdddddddddddddddd",
    "tomTomApiKey": "OldTomTomGPS",
    "bingMapsApiKey": "GOOGLECOMPETITOR"
}
```
would be a valid JSON input.