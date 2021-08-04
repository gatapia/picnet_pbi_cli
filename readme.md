# PicNet Power BI Command Line Tools

This is a set of tools to make working collaboratively on Power BI projects simpler.

## Overview

Unfortunately the Power BI `pbix` file format is a binary file format making it incompatible with:
- version control tools
- diff/merge tools
- search/replace
- general text editors

However, the file itself is just a zip file.  Using this fact we can simply extract the file contents and
work with the "source" json files.

## Supported Arguments

- `-f` The filename of the report to import
- `-o` The output directory (when importing) or filename (when exporting)
- `-d` The name of the DataModel file to use.  This is good two swap between a small DataModel for development and
  a larger more complete DataModel for testing.

## Supported Opperations

### Import PBIX File
`pbi import -f <pbix-filename> -o <source-dir> -d <data-model>`
Where:
- pbix-filename [required]: the name of the file to import
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- data-model [optional="default"]: the alias to use for this data model 

Importing a PBIX file does the following:
- Creates the output directory if required.  This is specified using the `-o` argument (defaults to `src`)
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
`pbi export -f <source-dir> -o <pbix-filename> -d <data-model>`
Where:
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- pbix-filename [required]: the name of the file to import
- data-model [optional="default"]: the data model to use

This command creates a `pbix` file from the specified `source-dir`.  This file can then be edited in Power BI Desktop.

*Note:* Both import and export of pbix file is something that is expected to happen many times, so all operations are destructive byefault.  
I.e. importing a pbix will overwrite the contents of the output directory with what ever is in the pbix file.

### Extract Data Model
`pbi data -f <pbix-filename> -o <source-dir> -d <data-model>`
Where:
- pbix-filename [required]: the name of the file to import
- source-dir [optional="src"]: the name of the directory to extract the pbix contents into
- data-model [optional="default"]: the alias to use for this data model 

Extracts the `DataModel` file from the specified `pbix` file and saves it in the `source-dir\data` directory with the
specified `data-model` alias name.


### Debug PBIX File
`pbi debug -f <pbix-filename>`
This command will list the pbix file contents and some useful information about each file