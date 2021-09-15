# PicNet Power BI Command Line Tools

This is a set of command line tools to make working collaboratively on Power BI projects simpler.

## Warning
- Backup your pbix files regularly.  All commands below are destructive and any crashes could result in corrupt pbix files.
- Due to the size of DataModel files, these are added to .gitignore files by default.  This means that checking out the code
  on a computer with no DataModel files will fail to export a valid pbix file.  You can modify the default .gitignore file
  and add a small model to your git repo if you would like a self contained repo.

## Overview
Unfortunately the Power BI `pbix` file format is a binary file format making it incompatible with:
- version control tools
- diff/merge tools
- search/replace
- general text editors

However, the file itself is just a zip file.  Using this fact we can simply extract the file contents and
work with the "source" json files.  Once the source files are available all tasks above can be done and better
development practices can be followed.

## Supported Opperations

### Global Supported Arguments
- `-f` The filename of the report to import or the destination report to overwrite during export
- `-d` The output directory when importing or source directory when exporting
- `-m` The name of the DataModel file to use.  This allows swapping between a small DataModel for development and
  a larger more complete DataModel for testing and deployment.

### Import PBIX File
`pbi import -f <pbix-filename> -d <source-dir> -m <data-model>`
Where:
- pbix-filename [required]: the name of the file to import
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- data-model [optional="default"]: the alias to use for this data model 

Importing a PBIX file does the following:
- Creates the output directory if required (otherwise overwrites the existing directory).  This directory is specified 
    using the `-o` argument (defaults to `src`)
- Creates a `data` directory in the output directory if required.  This directory is used to store the reports DataModel files.
- Extracts the contents of the PBIX file to the output directory
- Formats all JSON files to make it easier to read
- Converts all files to UTF8 for better compatibility with modern editors
- Creates a .gitignore in the output directory (if required).  This file ignores the DataModel and SecurityBindings
  binary files
- Moves the DataModel binary file to the `data` directory and renames if to the name specified with the `-d` argument (defaults to `default`)

*Note:* Both import and export of pbix file is something that is expected to happen many times, so all operations are destructive byefault.  
I.e. importing a pbix will overwrite the contents of the output directory with what ever is in the pbix file.

### Export PBIX File
`pbi export -d <source-dir> -f <pbix-filename> -m <data-model>`
Where:
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- pbix-filename [required]: the name of the file to import
- data-model [optional="default"]: the data model to use

This command creates a `pbix` file from the specified `source-dir`.  This file can then be edited in Power BI Desktop.

*Note:* Both import and export of pbix file is something that is expected to happen many times, so all operations are destructive byefault.  
I.e. importing a pbix will overwrite the contents of the output directory with what ever is in the pbix file.

### Extract Data Model
`pbi data -f <pbix-filename> -d <source-dir> -d <data-model>`
Where:
- pbix-filename [required]: the name of the file to import
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- data-model [optional="default"]: the alias to use for this data model 

Extracts the `DataModel` file from the specified `pbix` file and saves it in the `source-dir\data` directory with the
specified `data-model` alias name.

### Analyze Running Report
`pbi analyze -d <source-dir>`
Where:
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into

Analyzes the current running report using the internal SSAS database and the specified `source-dir`.  This analysis includes:
- lists all measures
- lists unused measures
- lists duplicate measures

**Note**: The analyze task requires that a single running Power BI report be running.  It is the SSAS database
  of this running report that is analyzed.



### Debug PBIX File
`pbi debug -f <pbix-filename>`
This command will list the pbix file contents and some useful information about each file


## TODO
- The Layout json file (and perhaps other) have embedded json strings, find a better way of supporting this
- Automatically backup pbix files before any destructive command?  Could become very big, perhaps only keep x-days