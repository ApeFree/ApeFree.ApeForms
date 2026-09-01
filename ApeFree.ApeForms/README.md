# ApeFree.ApeForms

[![NuGet](https://img.shields.io/nuget/v/ApeFree.ApeForms.svg?style=flat-square)](https://www.nuget.org/packages/ApeFree.ApeForms)
[![License](https://img.shields.io/badge/license-Apache%202.0-blue.svg)](LICENSE)

**ApeFree.ApeForms** 是一套功能强大、开箱即用的 **Windows Forms (WinForms)** UI 增强组件库与扩展工具集。致力于为工业自动化、上位机系统、工具软件及桌面生产力应用提供现代扁平（Metro/Flat）风格的高颜值控件、丰富的提示弹窗体系与模块化嵌套窗体模板。

---

## 🌟 核心特性

- 🎨 **丰富现代的 WinForms 控件库**：
  - 按钮与交互：`SimpleButton`、`ImageButton`、`ShapeButton`、`TabButton`、`StateButton`
  - 卡片与磁贴：`CardView`、`Magnet`、`SimpleCard`
  - 容器与布局：`AlignCenterBox`、`GridLayoutPanel`、`SlideTabControl`、`ScrollablePanel`
  - 数据与视觉：`ControlField`、`NotificationBox`、`CircleProgressBar`、`WaveProgressBar`
- 💬 **强大易用的提示系统**：
  - 悬浮通知 (`Toast`、`NotificationForm`)
  - 便捷输入与确认对话框
- 🏗 **模板嵌套窗体架构**：提供模块化页面切换、侧边栏导航宿主等预设架构模板，助你极速搭建工业上位机框架。
- 🔄 **广泛的运行时兼容**：全面兼容 `.NET Framework 4.5.2/4.6.1+` 以及现代 `.NET 8.0/10.0-windows`。

---

## 📦 安装

通过 NuGet 命令行安装：

```bash
dotnet add package ApeFree.ApeForms
```

---

## 🚀 快速上手

### 1. 使用美化按钮与卡片控件

在 Visual Studio 工具箱中直接拖拽或在代码中初始化控件：

```csharp
using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Cards;

// 1. 创建现代化扁平按钮
var btn = new SimpleButton
{
    Text = "开始采集",
    Size = new Size(120, 40),
    BackColor = Color.SeaGreen,
    ForeColor = Color.White
};

// 2. 创建卡片视图
var card = new SimpleCard
{
    Title = "设备运行状态",
    Description = "PLC 通信正常，当前节拍 120ms"
};
```

### 2. 弹出快速气泡通知 (Toast)

```csharp
using ApeFree.ApeForms.Forms.Notifications;

// 在屏幕右下角或指定父窗口弹出消息提示
Toast.Show("参数保存成功！", ToastType.Success);
```

---

## 📄 开源许可证

本项目基于 [Apache-2.0](https://www.apache.org/licenses/LICENSE-2.0) 协议开源。