
# How To Use
Ckeck_CSV converts a csv file to CHIP-0007 compatable jsons, calcalutes the SHA-256 hash for each new json file, and then appends the hash value to
the original csv. This program was done with .NET 6.


HOW TO GET FILE PATH?
dotnet app.dll {pathToFolder} {filename.csv}

Example:
Below is an example of the system symtem
C:
├───CsvFolder
│   └───CSVFileName.csv

Then in the console
dotnet app.dll C:\\CsvFolder CSVFileName.csv
