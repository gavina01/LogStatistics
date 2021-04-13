# Log Statistics

Initial Console application to parse a log file and return specific result from command line

## Installation

To run the application,

1. Clone the repositry 
2. Run Build on the project files

## Usage

To use the console application the following commands are available

```

--f - Path to the log file
--stat - top (support for the top 3 records) / count (unique count of values in property)
--p Support for the property required (ClientIP / URI are currently supported)

example 
	Log.Statistics.exe -f c:\example\programming-task-example-data.log  -s top - p ClientIP

```

Tests have been completed for required outcomes -- ConsoleApplication.Tests

 * The number of unique IP addresses
 * The top 3 most visited URLs
 * The top 3 most active IP addresses

![image](https://user-images.githubusercontent.com/6653691/114495470-772b0180-9c61-11eb-8231-c34426d643bf.png)

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
