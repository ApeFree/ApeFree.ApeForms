# ApeFree.ApeForms

[![NuGet](https://img.shields.io/nuget/v/ApeFree.ApeForms.svg?style=flat-square)](https://www.nuget.org/packages/ApeFree.ApeForms)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)

**ApeFree.ApeForms** 是一套功能强大、现代化、开箱即用的 **Windows Forms (WinForms)** UI 增强组件库与扩展工具集。致力于为工业自动化、上位机系统、测试测量工具及桌面生产力应用提供扁平现代（Metro/Flat）风格的高颜值控件、强大易用的对话框与气泡通知体系，以及模块化嵌套窗体模板。

本项目已完成重构合并（版本号 `1.0.2.1-alpha260907`），完全遵循 .NET 命名与设计规范，支持跨框架多目标编译与高 DPI 渲染。

---

## 🌟 核心特性

- 🎨 **丰富现代的 WinForms 控件集（按 .NET 规范分类与命名）**：
  - **Buttons（按钮族）**：`SimpleButton`（扁平/圆角/图标/主题色支持）、`ImageButton`、`ShapeButton`、`TabButton`、`StateButton`
  - **Cards（卡片与看板）**：`CardView`、`SimpleCard`、`Magnet`（磁贴看板）
  - **Containers（容器与布局）**：`AlignCenterBox`、`GridLayoutPanel`、`SlideBox`、`ScrollablePanel`
  - **DatePickers（日期时间选择）**：`DateTimePicker`、`DatePicker`、`TimePicker`
  - **Grids（网格与数据展示）**：`GridView`、`DataMatrixView`
  - **Labels & TextBoxes（文本与输入）**：`TallerLabel`、`PromptTextBox`、`RealTimeTextBox`
  - **ListBoxes & TabBoxes（列表与标签页）**：`ControlListBox`、`SlideTabControl`、`BadgeTabControl`
  - **TitleBars & Views（标题栏与视图）**：`GradualTitleBar`、`DriveBrowserView`（文件系统浏览器）
- 💬 **全套 Dialog 对话框系统**：
  - 无缝对接 `ApeFree.ApeDialogs` 标准层，完整提供 19 种原生 WinForms 对话框实现：
    - **消息与提示**：`ApeFormsMessageDialog`、`ApeFormsPromptDialog`、`ApeFormsTextDialog`、`ApeFormsToastDialog`
    - **输入与录入**：`ApeFormsInputDialog`（单行/多行）、`ApeFormsPasswordDialog`、`ApeFormsComboBoxInputDialog`、`ApeFormsNumberInputDialog`、`ApeFormsDataEntrySheetDialog`（支持 10 种表单字段类型与动态验证）
    - **选择与提取**：`ApeFormsSelectionDialog<T>`（单选）、`ApeFormsMultipleSelectionDialog<T>`（多选）、`ApeFormsDateTimeDialog`、`ApeFormsColorDialog`
    - **调节与进度**：`ApeFormsSliderDialog`（实时滑块调节）、`ApeFormsRangeSliderDialog`（双滑块范围调节）、`ApeFormsProgressDialog`（异步进度展示）
    - **文件与目录**：`ApeFormsOpenFileDialog`、`ApeFormsOpenFolderDialog`、`ApeFormsSaveFileDialog`
- 🔔 **通知与弹窗系统**：
  - `Toast` 悬浮气泡提示（支持复用模式、排队模式与自定义停留时长）
  - `NotificationBox` / `NotificationForm` 桌面右下角侧滑通知
- 🏗 **模板嵌套窗体架构**：
  - `TemplateForm`：支持模块化页面切换、侧边栏导航、Tab 标签页嵌入等预设上位机框架
  - `SideForm`：侧边抽屉式浮动窗体
  - `FramelessForm`：Cake2D 渲染引擎驱动的现代无边框窗体
- 🔄 **广泛的运行时兼容**：
  - 全面支持 `.NET Framework 4.5.2 / 4.6.1+` 以及现代 `.NET 8.0 / 10.0-windows`。

---

## 📦 安装方式

通过 .NET CLI 命令行安装：

```bash
dotnet add package ApeFree.ApeForms
```

或在 Visual Studio 的 NuGet 包管理器中搜索 `ApeFree.ApeForms` 进行安装。

---

## 🚀 快速上手

### 1. 使用美化按钮与卡片控件

在 Visual Studio 工具箱中拖拽或在代码中直接创建：

```csharp
using System.Drawing;
using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Cards;

// 1. 创建现代扁平按钮
var btn = new SimpleButton
{
    Text = "开始采集",
    Size = new Size(120, 40),
    BackColor = Color.FromArgb(0, 122, 204),
    ForeColor = Color.White
};

// 2. 创建状态卡片
var card = new SimpleCard
{
    Title = "设备运行状态",
    Description = "PLC 通信正常，当前节拍 120ms"
};
```

### 2. 弹出气泡通知 (Toast)

```csharp
using ApeFree.ApeForms.Forms.Notifications;

// 在屏幕右下角或指定容器上弹出快速消息提示
Toast.Show("参数配置已成功保存！", 2000);
```

### 3. 使用 DialogProvider 弹出对话框

```csharp
using System;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Dialogs;

var provider = new ApeFormsDialogProvider();

// 弹出带有选项与校验的输入框
var dialog = provider.CreateInputDialog(new InputDialogSettings
{
    Title = "重命名",
    Content = "请输入新的设备名称：",
    DefaultText = "Station_01",
    Validator = text => string.IsNullOrWhiteSpace(text)
        ? ValidationResult.Failure("名称不能为空")
        : ValidationResult.Success
});

var result = dialog.Show();
if (!result.IsCanceled)
{
    Console.WriteLine($"用户输入：{result.Value}");
}
```

---

## 🔗 相关项目与文档

- 📚 **项目专栏与教程**：[CSDN 专栏：ApeForms 教程与最佳实践](https://blog.csdn.net/lgj123xj/category_11811822.html)
- 🐙 **源码仓库**：[GitHub - ApeFree/ApeFree.ApeForms](https://github.com/ApeFree/ApeFree.ApeForms)

---

## 🧩 扩展生态

| 包名 | 说明 |
| :--- | :--- |
| **`ApeFree.ApeDialogs`** | 跨平台 Dialog 对话框标准契约层 |
| **`ApeFree.Cake2D`** | 轻量级图层化 2D 绘图与交互引擎 |
| **`ApeFree.Cake2D.Gdi`** | Cake2D GDI+ 绘图渲染器适配库 |
| **`STTech.CodePlus`** | 通用工具与基础代码增强扩展库 |

---

## 📄 开源许可证

本项目基于 [Apache-2.0](https://www.apache.org/licenses/LICENSE-2.0) 协议开源。
