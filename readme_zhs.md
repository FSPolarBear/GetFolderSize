<div align="center">

# GetFolderSize
[English](readme.md)|[简体中文](readme_zhs.md)

获取文件夹中文件和子文件夹的大小，默认以大小的降序排列。
</div>

---

## 版本
v2.0.0

---

## 环境要求
Windows7/10/11, .NET 6.0

---

## 使用方法
### 获取文件夹信息
在"路径"文本框中输入文件夹路径，或点击"浏览文件夹"按钮并选择文件夹，然后点击"获取"按钮开始获取。  
可在"路径"文本框中输入多个文件夹的路径以同时获取多个文件夹的信息。多个路径间使用"|"分隔。  
获取、导入、搜索过程中无法获取、导入、搜索。  
获取、导入、搜索过程中可点击"取消"按钮取消正在进行的获取、导入、搜索。  
点击列表中的文件夹项以进入该文件夹。  
将鼠标置于文件或文件夹时显示完整路径  
文件和文件夹默认以大小降序排序。如果需要其他排序方法，可点击列标题以对应列排序，第二次点击同一列标题则为反向排序。  
点击"根文件夹"按钮返回获取(或导入)的根文件夹。  
点击"返回上级"按钮返回上级文件夹。  
点击"导出"按钮将获取(或导入)的文件夹信息导出为json文件。  
点击"导出当前页"按钮将当前显示的文件夹信息导出为json文件。  
点击"导入"按钮从json文件导入文件夹信息。  
点击"在资源管理器中显示"按钮在资源管理器中打开当前显示的文件夹。若选中了文件或文件夹，则在资源管理器中指向被选中的文件或文件夹。  
点击"刷新"按钮重新加载当前显示的文件夹。

### 搜索
在"搜索"文本框中输入关键字，并点击"搜索"按钮以搜索名字符合条件的文件或文件夹。  
在搜索结果页面点击"返回上级"后，"搜索结果"的文件数为符合搜索条件的文件或文件夹数。  
默认匹配名字包含关键字(不区分大小写)的文件和文件夹。详细的搜索设置参见"设置"节。  
搜索结束后，如需返回获取(或导入)的所有数据，可点击"根文件夹"按钮。  

---

## 设置
点击主界面的"设置"按钮进入设置界面。本节提到的组件位于设置界面。  
点击"确定"按钮或按下回车键应用当前设置并关闭设置界面，点击"应用"按钮应用当前设置并留在设置界面，点击"取消"按钮或按下ESC键关闭设置界面并不应用当前设置。  
点击"保存"按钮将当前应用设置保存至json文件。  
点击"读取"按钮从json文件中读取设置。  
点击"默认"按钮恢复默认设置。  

以下是具体设置项：

### 语言：

#### 语言 (默认值：系统语言，若不支持则为英语)

### 程序：

#### 开启时自动启动程序 (默认值：否)
此项为"是"时会在开机时自动启动GetFolderSize程序。

#### 关闭时最小化窗口 (默认值：是)
此项为"是"时，点击叉号关闭程序时改为将窗口最小化。  
此项为"是"时，可点击主界面的"退出"按钮或托盘图标右键菜单中的"退出"按钮退出程序。  
窗口最小化后可双击托盘图标以显示窗口。  

#### 获取时显示文件数 (默认值：是，间隔100毫秒)
**输入要求**：间隔应为非负整数。   
此项为"是"时，获取过程中显示已获取文件数。  
"间隔"是更新已获取文件数的时间间隔。

#### 只允许启动一个GetFolderSize程序 (默认值：否)
此项为"是"时，若已经存在一个GetFolderSize进程，再次运行GetFolderSize.exe时会切换至已打开的GetFolderSize窗口，而非启动一个新的GetFolderSize进程。

#### 退出程序时导出数据 (默认值：否，路径为"ExportedData.json")
此项为"是"时，退出程序或关机时将获取(或导入)的数据导出至指定json文件。  

#### 启动程序时获取数据 (默认值：否，路径为所有盘符)
此项为"是"时，启动程序时获取指定路径的文件夹信息。  

#### 启动程序时导入数据 (默认值：否，路径为"ExportedData.json")
此项为"是"时，启动程序时从指定json文件导入文件夹信息。  
当"启动程序时获取数据"和"启动程序时导入数据"均为"是"时，若需导入的数据文件存在则再启动程序时导入，否则在启动程序时获取。  

### 分批加载：

#### 启用分批加载 (默认值：是)
此项为"是"时，若需要显示包含很多文件和文件夹的文件夹，分批加载显示的文件和文件夹。  
此项为"否"时，在完成加载所有文件和文件夹前程序会无响应；此项为"是"时，可能需要花费更长的时间展示全部文件和文件夹。

#### 分批加载阈值 (默认值：1000)
**输入要求**：此项应为非负整数。
当显示的文件夹包含的文件和文件夹数不小于此项时，启用分批加载。  

#### 批大小 (默认值：1000)
**输入要求**：此项应为非负整数。
此项是分批加载时，每批最大加载数量为此项的文件或文件夹。 

#### 批间隔 (默认值：100毫秒)
**输入要求**：此项应为非负整数。
此项是分批加载时，两批之间的间隔时间。

### 搜索：

#### 搜索规则 (默认值：包含)
此项是搜素的规则，具体如下：
1. **包含**。匹配包含关键字的名字。  
2. **相同**。匹配与关键字相同的名字。 
3. **正则**。将关键字作为正则表达式以进行匹配。 
4. **通配符**。将关键字作为通配符进行匹配，其中`?`表示一个任意字符，`*`表示任意数量个任意字符。  
5. **扩展名**。匹配以关键字作为扩展名的名字。
6. **组合条件**。组合一个或多个以上规则的条件进行匹配。每个条件的格式为`规则：关键字`，多个条件使用`|`进行分割。例如，`扩展名：txt|包含：abc`匹配扩展名为`.txt`且包含`abc`的名字。注意**不要**在`：`后或`|`前添加不属于关键字的空格。  

#### 搜素文件 (默认值：是)
**输入要求**："搜索文件"和"搜索文件夹"中至少应有一个"是"。
此项为"是"时，搜索结果中包含文件。

#### 搜素文件夹 (默认值：是)
**输入要求**："搜索文件"和"搜索文件夹"中至少应有一个"是"。
此项为"是"时，搜索结果中包含文件夹。

#### 匹配全路径 (默认值：否)
此项为"是"时，匹配文件或文件夹的全路径；此项为否时，仅匹配名字。  
例如，对于文件`D:\folder1\a.txt`，当此项为"是"时匹配`D:\folder1\a.txt`，当此项为"否"时匹配`a.txt`。   

#### 递归搜索 (默认值：是)
此项为"是"时，在文件夹及其子文件夹中进行递归搜素；此项为否时不搜索子文件夹。

#### 区分大小写 (默认值：否)
此项为"是"时，名字和关键字区分大小写。

#### 从根文件夹中搜索 (默认值：否)
此项为"是"时，搜索获取(或导入)的根文件夹；此项为"否时"，搜索当前显示的文件夹。

#### 文件大小限制 (默认值：上下限均为空)
**输入要求**：上下限应为空，或包含单位的非负整数；上下限均不为空时下限应小于或等于上限。  
此项是搜索结果中文件的大小限制。空表示不设上限或下限。  
大小的单位支持`TB`(或`T`)、`GB`(或`G`)、`MB`(或`M`)、`KB`(或`K`)、`B`，不区分大小写。不填写单位则默认为`B`。

#### 文件夹大小限制 (默认值：上下限均为空)
**输入要求**：上下限应为空，或包含单位的非负整数；上下限均不为空时下限应小于或等于上限。  
此项是搜索结果中文件夹的大小限制。空表示不设上限或下限。  
大小的单位支持`TB`(或`T`)、`GB`(或`G`)、`MB`(或`M`)、`KB`(或`K`)、`B`，不区分大小写。不填写单位则默认为`B`。

#### 包含文件数限制 (默认值：上下限均为空)
**输入要求**：上下限应为空，或非负整数；上下限均不为空时下限应小于或等于上限。  
此项是搜索结果中文件夹的包含文件数。空表示不设上限或下限。  

### 文件或文件夹变化时更新数据：
#### 更新获取的数据 (默认值：否)
#### 更新导入的数据 (默认值：否)
若对应项为"是"，当获取或导入的文件夹中创建、删除、修改、重命名文件或文件夹时，更新GetFolderSize中的对应数据。   
数据更新后，点击"刷新"按钮以显示更新后的数据。  

---

## 提示
### 正在获取...
正在获取，请等待。
### 正在导入...
正在导入，请等待。
### 正在搜索...
正在搜索，请等待。
### 未找到文件夹
不存在路径为输入路径(或输入的多个路径中的其中任意一个)的文件夹。
### 导出成功
导出成功。
### 导出失败
导出失败。导出前应先获取文件夹信息。
### 导入失败
导入失败。导入文件格式可能有误。
### 搜索内容为空
搜索的文本为空。应在"搜索"文本框输入文本后点击"搜索"按钮。
### 正则表达式不正确
用于搜索的正则表达式不正确。

---

## 作者
[FSPolarBear](https://github.com/FSPolarBear)

