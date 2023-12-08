# GetFolderSize
Get the size of files and subfolders in a folder. The files and folders are sorted in descending order by file size by default.

## Version
v1.3.0

## Environmental Requirements
Windows7/10/11, .NET 6.0

## How to use
Input the path in "Path" text box, and then click "ok" button to start getting.<br>
It is unable to click "ok" and "import" buttons during get or import.<br>
Click the folder item to enter the folder.<br>
Click "back" button to return to the parent folder.<br>
Click "root" button to return to the root folder.<br>
The files and folders are sorted in descending order by file size by default. If you need other sorting methods, you can click on a column header to sort by the column. Click the column header again to reverse the sorting.<br>
Click "export" button to export the data into a json file.<br>
Click "import" button to import data from a json file.<br>
Click "show in explorer" button to open the shown folder in explorer. If you choose a file or folder, it will be selected in the explorer.<br>
Input the name for search in "Search" text box, and then click "search" button to search the files and folders that match the requirement.
The search rule "include" will find files and folders whose name include the inputted text.
The search rule "same" will find files and folders whose name is the same as the inputted text.
The search rule "regular" will find files and folders whose name matches the inputted regular expression.<br>
If "search files" check box is checked, the search results include files.<br>
If "search folders" check box is checked, the search results include folders.<br>
If "recursive search" check box is checked, the search will be conducted recursively in the current folder and its subfolders. Otherwise, the search will be conducted only in the current folder.<br>
If you want to return to all gotten files and folders, please click "root" button.<br>
Put the mouse over a file or folder to show the full path of it.

## Alert
### loading
It is getting or importing. Please wait.
### folder not found
The folder is not found.
### export succeed
Export succeed.
### export failed
Export failed.
### import failed
Import failed.
### search text is empty
The text for search is empty. Please input the text for search into "Search" text box before click "search" button.
### nothing for search
Both "search files" and "search folders" check boxes are not checked. Please check at lease one of them before click "search" button.



## Update log
2023.12.8<br>
v1.3.0<br>
We added the function of search.<br>
We added the function of opening the current folder in explorer and select the chosen file or folder.<br>
We added the function of showing the full path when putting the mouse on a file or folder.<br>
We fixed some typographic and descriptive problems of readme.md.<br>
We added a readme.md in English.<br>
We removed compiled files from the source code.<br>

2022.6.10<br>
v1.2.0<br>
We added the functions of import and export.<br>
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

