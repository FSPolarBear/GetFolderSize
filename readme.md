# GetFolderSize
Get the size of files and subfolders in a folder. The files and folders are sorted in descending order by file size by default.

## Version
v1.4.1

## Environmental Requirements
Windows7/10/11, .NET 6.0

## How to use
### Get folder information
Input the path in "Path" text box, and then click "Get" button to start getting.<br>
It is able to get information about multiple folders by inputting multiple paths in "Path" text box. The paths should be separated by "|".<br>
It is unable to get, import or search again during getting, importing or searching.<br>
Click "Import" button to import folder information from a json file.<br>
Click "Export" button to export the folder information into a json file.<br>
Click a folder item in the list to enter the folder.<br>
Put the mouse over a file or folder to show the full path of it.
Click "Back" button to return to the parent folder.<br>
Click "Root" button to return to the root folder.<br>
The files and folders are sorted in descending order by size by default. If you need other sorting methods, you can click on a column header to sort by the column. Click the column header again to reverse the sorting.<br>
Click "Show in explorer" button to open the displayed folder in explorer. If you select a file or folder, it will be selected in the explorer.<br>
Click "Refresh" button to reload the displayed folder.<br>

### Search
Input the key word in "Search" text box, and then click "search" button to search the files and folders that match the criteria in the current folder.<br>
When the search result is displayed and you click "Back" button, the file count of item "Search results" is the number of files or folders that match the criteria.
By default, the files and folders whose name includes the key word (with case-insensitive) will be matched. For detailed search settings, see section "Setting".
If you want to return to all gotten files and folders after searching, please click "root" button.<br>

### Setting
Click "Setting" button in the main from to enter the setting from. The components mentioned in this section are located in the setting form.<br>
Click "Ok" button to save the setting. Click "Cancel" button to close the setting form without saving the setting.<br>
Click "Default" button to load the default setting.<br>
If "Enable batch loading" check box is checked, the items will be loaded in batches when a large number of items are displayed, which will result in a longer time to load all the items. Otherwise, a large number of items may cause the program to become unresponsive during loading.<br>
"Batch loading threshold" should be a non-negative integer. Batch loading is used when the number of displayed items is not less than the batch loading threshold.<br>
"Batch size" should be a positive integer. Batch size is the number of items in a batch.<br>
"Batch interval (ms)" should be a non-negative integer. Batch interval is the time interval between two batches.<br>
When the search rule is "Include", files and folders whose name includes the key word will be matched.<br>
When the search rule is "Same", files and folders whose name is the same as the key word will be matched.<br>
When the search rule is "Regular", the key word is seemed as a regular expression to match the name of files and folders.<br>
If "Search files" check box is checked, files will be included in the search result.<br>
If "Search folders" check box is checked, folders will be included in the search result.<br>
At least one of "Search files" and "Search folders" check boxes should be checked.<br>
If "recursive search" check box is checked, the search will be conducted recursively in the current folder and its subfolders. Otherwise, the search will be conducted only in the current folder.<br>
If "Case Sensitive" check box is checked, it will be case-sensitive in the search.<br>
Lower and upper limit of "File size limit" should be empty or a non-negative integer with unit (B、KB、MB、GB、TB) such as "1KB". File size limits are the lower and upper limit of size of matched files. Empty means no lower or upper limit.<br>
Lower and upper limit of "Folder size limit" should be empty or a non-negative integer with unit (B、KB、MB、GB、TB) such as "1KB". Folder size limits are the lower and upper limit of size of matched folders. Empty means no lower or upper limit.<br>
Lower and upper limit of "File count limit" should be empty or a non-negative integer. File count limits are the lower and upper limit of file count of matched folders. Empty means no lower or upper limit.<br>

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



## Update log
2023.12.27<br>
v1.4.1<br>
Now, a file or folder is displayed only once in a search, even if it is contained in multiple gotten folders.<br>
We fixed a bug that the program crashed after searching, clicking "Back" button and searching again.


2023.12.21<br>
v1.4.0<br>
We added Simplified Chinese UI and the function of language switch.<br>
We added the function of displaying the last write time of files and folders.<br>
We added the function of getting multiple folders once.<br>
We added the function of case-insensitive search.<br>
We added the options of file size, folder size and file count limits for search.<br>
We added the setting form, and moved the setting options to the setting form, and added more setting options.<br>
We updated the text.<br>
We optimized the sort to solve the problem of stack overflow when sorting a large number of items.<br>
We optimized the display of size. When the unit of size is "B", we use an integer instead of a decimal to indicate the size.<br>
We fixed a bug that items of the old page might be loaded to the new page when change the page during batch loading.<br>
We fixed a bug that a wrong path was shown when put the mouse over "Search result" item.<br>


2023.12.13<br>
v1.3.1<br>
We added the function of batch loading, in order to solve the problem that the problem may become unresponsive during loading a large number of items.<br>
We added the function of refreshing.<br>
We updated the alert text.<br>


2023.12.8<br>
v1.3.0<br>
We added the function of searching.<br>
We added the function of opening the current folder in explorer and select the chosen file or folder.<br>
We added the function of showing the full path when putting the mouse on a file or folder.<br>
We fixed some typographic and descriptive problems of readme.md.<br>
We added a readme.md in English.<br>
We removed compiled files from the source code.<br>

2022.6.10<br>
v1.2.0<br>
We added the functions of importing and exporting.<br>
We optimized the UI.<br>

2022.6.9<br>
v1.1.0<br>
We added the function of sorting by a column.<br>
We fixed a bug that when we tried to get a new folder but failed, it still showed the path of the old folder.<br>
We optimized the file structure.<br>

2022.6.8<br>
v1.0.0<br>
This is the first release of GetFolderSize.<br>


## Author
[FSPolarBear](https://github.com/FSPolarBear)

