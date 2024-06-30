<div align="center">

# GetFolderSize
[English](readme.md)|[简体中文](readme_zhs.md)

Get the size of files and subfolders in a folder. The files and folders are sorted in descending order by file size by default.
</div>

---

## Version
v2.0.0

---

## Environmental Requirements
Windows7/10/11, .NET 6.0

---

## How to use
### Get folder information
Input the path of folder in "Path" text box, or click "View" button and choose a folder, and then click "Get" button to start getting.  
It is able to get information about multiple folders by inputting multiple paths in "Path" text box. The paths should be separated by "|".  
It is unable to get, import or search during getting, importing or searching.  
It is able to click "Cancel" button to stop getting, importing or searching  
Click a folder item in the list to enter the folder.  
Put the mouse over a file or folder to show the full path of it.  
The files and folders are sorted in descending order by size by default. If you need other sorting methods, you can click on a column header to sort by the column. Click the column header again to reverse the sorting.  
Click "Root" button to return to the gotten (or imported) root folder.  
Click "Back" button to return to the parent folder.  
Click "Export" button to export the gotten (or imported) folder information into a json file.  
Click "Export current" button to export the currently displayed folder information into a json file.  
Click "Import" button to import folder information from a json file.  
Click "Show in explorer" button to open the displayed folder in explorer. If you select a file or folder, it will be selected in the explorer.  
Click "Refresh" button to reload the displayed folder.  

### Search
Input the keyword in "Search" text box, and then click "Search" button to search for the files and folders whose name matches the criteria.  
When the search result is displayed, and you click "Back" button, the file count of item "Search results" is the number of files or folders that match the criteria.
By default, the files and folders whose name contains the keyword (with case-insensitive) will be matched. For detailed search settings, see section "Setting".
If you want to return to all gotten (or imported) files and folders after searching, please click "root" button.  

---

## Setting
Click "Setting" button in the main from to enter the setting from. The components mentioned in this section are located in the setting form.  
Click "Ok" button or press Enter to apply the setting and close the setting form. Click "Apply" button to apply the setting and do not close the setting form. Click "Cancel" button or press ESC to close the setting form without applying the setting.  
Click "Save" button to save the currently applied setting to a json file.  
Click "Load" button to load setting from a json file.
Click "Default" button to load the default setting.  

Following are the specific setting items:  

### Language:

#### Language (Default: System language, or English if system language is not supported)

### Program:  

#### Autostart (Default: false)
If true, GetFolderSize program will autostart when the computer is started。  

#### Minimize when close (Default: true)
If true, the form will be minimized instead of be closed when close the form.   
If true, it is able to close the form by clicking "Exit" button at main form, or by clicking "Exit" button at the right-button menu of the tray icon.  
If the form is hidden, it is able to double-click the tray icon to show the form.  

#### Show file count when getting (Default: true, and Interval is 100 ms)
**Input constriction**: Interval should be a non-negative integer.      
If true, the gotten file count will be displayed during getting.  
Interval is the time interval for updating the count.  

#### Only one GetFolderSize process is allowed to exist (Default: false)
If true, when a GetFolderSize process is existing and run GetFolderSize.exe again, it will switch to the existing GetFolderSize from, instead of start a new GetFolderSize progress.

#### Export data when exit (Default: false, and Path is "ExportedData.json")
If true, the gotten (or imported) data will be exported to a json file when exiting program or shutting down.  

#### Get data when start (Default: false, and Path is all disks)
If true, it will get folder information by the path when starting the program.  

#### Import data when start (Default: false, and Path is "ExportedData.json")
If true, it will import folder information from a json filewhen starting the program.  
If both "Get data when start" and "Import data when start" are true, it will import when starting the program if the json file to import is existing; otherwise it will get when starting the program.   

### Batch loading:  

#### Enable batch loading (Default: true)
If true, when display a folder who contains a large number of files and folders, loading the files and folders in batches.
If false, the program will be frozen until all folders and files to display are loaded; If true, it may take more time to load all files and folders.

#### Batch loading threshold (Default: 1000)
**Input constriction**: This item should be a non-negative integer.  
When the file and folder count of the folder to display is not less than this item, it will enable batch loading.

#### Batch size (Default: 1000)
**Input constriction**: This item should be a non-negative integer.  
This item is the max count of file and folder in a batch in batch loading.   

#### Batch interval (Default: 100ms)
**Input constriction**: This item should be a non-negative integer. 
This item is the time interval between two batches in batch loading.

### Search:

#### Search rule (Default: Contain)
This item is the rule of search. Following are the specific items:  
1. **Contain**. Matching names that contain the keyword.
2. **Same**. Matching names that are the same as the keyword.
3. **Regular**. Using the keyword as regular expression to match.  
4. **Wildcard**. Using the keyword as wildcard to match, where `?` denotes an any character, and `*` denotes any number of any characters.    
5. **Extension**. Matching names that use keyword as extension.
6. **Combination**. Combining one or more criteria with above rules to match. Each criterion is formatted as `rule(case-insensative):keyword`, and multiple criteria are split using `|`. For example, `Extension:txt|Contain:abc` matches names that have extension `.txt` and contain `abc`. Notice that **DO NOT** add spaces that are not part of the keyword after `:` or before `|`. 

#### Search files (Default: true)  
**Input constriction**: At least one of "Search files" and "Search folders" should be true.  
If true, the search result contents files.

#### Search folders (Default: true)  
**Input constriction**: At least one of "Search files" and "Search folders" should be true.  
If true, the search result contents folders.

#### Match full name (Default: false)
If true, the names for matching are full paths of files and folders; if false, the names for matching are just the names.  
For example, for file `D:\folder1\a.txt`, if this item is true, the name for matching is `D:\folder1\a.txt`; if this item is false, the name for matching is `a.txt`. 

#### Recursive search (Default: true)
If true, searching the folder and its sub-folders recursively; otherwise, only search the folder and do not search the sub-folders.

#### Case sensitive (Default: false)
If true, the names and the keyword are case-sensitive.

#### Search from root (Default: false)
If true, search the gotten (or imported) root folder; otherwise, search the currently displayed folder.

### File size limit (Default: both lower and upper limits are empty)
**Input constriction**: Lower and upper limit should be empty or a non-negative integer with unit. If both upper and lower limits are not empty, the lower limit should be lower than or equal to the upper limit.   
This item is the size limit for files in the search result. Empty denotes no lower or upper limit.  
As the unit of size, `TB` (or `T`), `GB` (or `G`), `MB` (or `M`), `KB` (or `K`) and `B` with case-insensitive are supported. If the unit is not inputted, it is `B` by default.

### Folder size limit (Default: both lower and upper limits are empty)
**Input constriction**: Lower and upper limit should be empty or a non-negative integer with unit. If both upper and lower limits are not empty, the lower limit should be lower than or equal to the upper limit.   
This item is the size limit for folders in the search result. Empty denotes no lower or upper limit.  
As the unit of size, `TB` (or `T`), `GB` (or `G`), `MB` (or `M`), `KB` (or `K`) and `B` with case-insensitive are supported. If the unit is not inputted, it is `B` by default.

### File count limit (Default: both lower and upper limits are empty)
**Input constriction**: Lower and upper limit should be empty or a non-negative integer. If both upper and lower limits are not empty, the lower limit should be lower than or equal to the upper limit.   
This item is the file count limit for folders in the search result. Empty denotes no lower or upper limit.  

### Update data when folders or files change
#### Enable update gotten data (Default: false)
#### Enable update imported data (Default: false) 
If the corresponding item is true, when folders or files in the gotten or imported folder(s) are created, deleted, modified or renamed, the corresponding data in GetFolderSize will be updated.  
After the data are updated, click "Refresh" button to display the updated data.   

---

## Alert
### Getting...
It is getting. Please wait.
### Importing...
It is importing. Please wait.
### Searching...
It is searching. Please wait.
### The folder is not found
There is no folder whose path is the same as inputted path(or any one of inputted paths).
### Export succeed
Export succeed.
### Export failed
Export failed. Get folder information before exporting.
### Import failed
Import failed. The imported file may be in the wrong format
### The search text is empty
The text for search is empty. Please input the text for search into "Search" text box before click "search" button.
### The regular expression is incorrect
The regular for search is incorrect。

---

## Author
[FSPolarBear](https://github.com/FSPolarBear)

