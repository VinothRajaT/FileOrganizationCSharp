# C# File Organizer Script

## Introduction

This C# script automates the organization of files by moving them to categorized folders based on their file types. It helps keep your digital workspace clean and efficient, enhancing productivity and ensuring easy file management.

## Key Features

- **Command-Line Input:** Specify source and destination directories.
- **Support for Multiple File Types:** Documents, Images, Spreadsheets, Audio, Videos, Archives, and more.
- **Dry Run Mode:** Preview changes without moving files.
- **File Overwrite Handling:** Options to overwrite, skip, or rename existing files.
- **Progress Reporting:** Real-time updates on file processing.
- **Configuration File Support:** Customize file types and folders through a configuration file.

## How It Works

The script organizes files from a source directory into a destination directory, sorting them into subfolders based on their extensions.

### Usage

```sh
dotnet run <sourceDir> <destDir> [--dry-run] [--overwrite] [--config <configFile>]
```

- ** <sourceDir> **: Path to the source directory containing files to organize.
- **<destDir>**: Path to the destination directory where files will be organized.
- **--dry-run**: Optional flag to preview changes without moving files.
- **--overwrite**: Optional flag to overwrite existing files in the destination directory.
- **--config <configFile>**: Optional parameter to specify a configuration file for file types and folders.

## Configuration File
If you want to use a custom configuration file, create a text file with the following format:

```sh
Documents: .pdf, .docx, .txt, .doc, .odt, .rtf
Images: .jpg, .jpeg, .png, .gif, .bmp, .tiff, .svg
Spreadsheets: .xlsx, .xls, .csv, .ods
Presentations: .pptx, .ppt, .odp
Audio: .mp3, .wav, .flac, .aac, .ogg
Videos: .mp4, .avi, .mkv, .mov, .wmv, .flv
Archives: .zip, .rar, .7z, .tar, .gz
```

## Example
### Dry Run Mode

```sh
dotnet run sourceDir destDir --dry-run
```

### Output:
```sh
Dry Run: 'document.pdf' will move to 'Documents' folder.
Dry Run: 'image.jpg' will move to 'Images' folder.
Dry Run: 'spreadsheet.xlsx' will move to 'Spreadsheets' folder.
Dry Run: 'video.mp4' will move to 'Videos' folder.
Dry Run: 'archive.zip' will move to 'Archives' folder.
Progress: 5/5 files processed.
Dry run completed. No files were moved.
```

### Actual Run
```sh
dotnet run sourceDir destDir --overwrite
```

### Output:
```sh
Progress: 1/5 files processed.
Progress: 2/5 files processed.
Progress: 3/5 files processed.
Progress: 4/5 files processed.
Progress: 5/5 files processed.
Files have been organized successfully.
```

## Dependencies
- .NET SDK

