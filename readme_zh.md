# GetFolderSize
查询文件夹中文件和子文件夹的大小，默认以大小的降序排列。

## 版本
v1.3.0

## 环境要求
Windows7/10/11, .NET 6.0

## 使用方法
在"Path"文本框中输入路径，点击"ok"按钮开始查询。<br>
查询或导入过程中无法点击"ok"和"import"按钮。<br>
点击列表中的文件夹条目行以进入该文件夹。<br>
点击"back"按钮返回上级文件夹。<br>
点击"root"按钮返回查询的根文件夹。<br>
默认以文件大小降序排序。如果需要其他排序方法，可点击列标题以对应列排序，第二次点击同一列标题则为反向排序。<br>
点击"export"按钮将查询的信息导出为json文件。<br>
点击"import"按钮从json文件导入查询的信息。<br>
点击"show in explorer"按钮在资源管理器中打开当前显示的文件夹。若选中了文件或文件夹，则在资源管理器中指向被选中的文件或文件夹。<br>
在"Search"文本框中输入需要搜索的文件、文件夹名，点击"search"按钮在当前展示的文件、文件夹中搜索符合条件的文件或文件夹。
Search rule为include时搜索名字包含输入的文本的文件或文件夹；
Search rule为same时搜索名字与输入的文本相同的文件或文件夹；
Search rule为regular时搜索名字与输入的正则表达式匹配的文件或文件夹。<br>
若"search files"选择框被选中，则搜索结果包含文件。<br>
若"search folders"选择框被选中，则搜索结果包含文件夹。<br>
若"recursive search"选择框被选中，则在当前文件夹及其子文件夹中进行递归搜索。若未选中，则仅在当前文件夹搜索。<br>
搜索结束后，如需返回查询到的所有数据，可点击"root"按钮。<br>
将鼠标置于文件或文件夹时显示完整路径<br>
## 提示
### loading
正在查询或导入，请等待
### folder not found
未找到文件夹
### export succeed
导出成功
### export failed
导出失败
### import failed
导入失败
### search text is empty
搜索的文本为空。应在"Search"文本框输入文本后点击"search"按钮
### nothing for search
"search files"和"search folders"选择框均未被选中。应至少选中其中之一后点击"search"按钮


## 更新记录
2023.12.8<br>
v1.3.0<br>
添加搜索文件、文件夹功能。<br>
添加在资源管理器中打开当前文件夹并指向选中文件的功能。<br>
添加将鼠标置于文件或文件夹上时显示完整路径的功能。<br>
修复readme.md中的排版问题和部分描述。<br>
添加英语readme.me。<br>
从源代码中移除了编译后的文件。<br>

2022.6.10<br>
v1.2.0<br>
添加了导入和导出功能。<br>
优化了界面。<br>

2022.6.9<br>
v1.1.0<br>
添加点击列标题以对应列排序的功能。<br>
修复未查找到新文件夹时仍显示旧文件夹路径的bug。<br>
优化了文件结构。<br>

2022.6.8<br>
v1.0.0<br>
这是GetFolderSize的第一个发布版本。<br>

## 作者
[FSPolarBear](https://github.com/FSPolarBear)

