# ChuyuSuperDownloader 天文软件下载器
## 写在前面
这是一个简单的工具，用于下载和安装与天文相关的软件。该程序支持使用 `aria2` 和 `wget` 来进行文件下载，具有快速、稳定的下载能力。
本程序的开发旨在让刚入坑天文的小白们快速获取与之相关的程序与主流驱动，起到极大的便利，同时，由于程序中多数链接采用实时解析，能够确保下载的软件是最新版本，也能够便利老手对软件和驱动的更新。
## 为什么选择这款下载器呢？

- **多线程下载**：通过 `aria2` 实现多线程并行下载，提升下载速度。
- **手动下载**：您可以把它当作简易的aria2下载器。
- **自动恢复**：支持下载中断后自动恢复，避免因网络问题造成下载失败。
- **自动安装**：下载完成后，自动启动安装程序或解压软件包。
- **支持常见天文软件**：例如 NINA、PIPP、Registax、Stellarium、SharpCap 等软件。
- **大文件利用对象存储缓存**：D80等astap数据库缓存在我们的存储桶中，能够解决下载需要挂tz的困难，一键实现快速下载。
- **使用ghproxy**：使用gh-proxy临时进行github下载代理，若代理失败，也能转为原速下载。
- **实时解析**：程序中多数链接采用实时解析，能够确保下载的软件是最新版本。
- **奇怪功能**：利用wub屏蔽Windows更新，一键打开siril、nina的目录，木星拍摄时间计算工具。以后还会添加更多！

## 使用
提示：安装器会被360等杀毒软件报毒，可以使用绿色版或手动添加信任！
1. 启动 `ChuyuSuperDownloader.exe`。
2. 从程序界面选择你需要下载的软件。
3. 程序将自动开始下载，并且显示下载进度。
4. 下载完成后，程序会自动启动安装程序或解压文件。

## 许可证

本项目遵循 [GNU 通用公共许可证 v3.0](https://www.gnu.org/licenses/gpl-3.0.html)。

## 贡献

如果你希望为本项目贡献代码或修复bug，欢迎提交Pull Request。

## 更新日志
2025/2/21 

第一个包含基础功能的alpha版本发布

2025/3/2

更新了：

1.天文摄影计算器

2.增加了鸣谢列表

修复了：

1.关于页面链接无法点开的问题

2.Siril下载安装因传参错误导致的无法启动安装程序


### 下个版本更新内容 Pending
添加NINA插件和Siril脚本的一键安装功能

## 注意事项

- 本工具需要 `.NET Framework` 环境支持，确保你的系统中安装了最新版本的 `.NET`。
- 部分软件可能需要额外的安装步骤，具体请参考软件的官方文档。
  ![3a87f630-3fc7-48a2-a6e9-0b732e1860bb](https://github.com/user-attachments/assets/4ab2c791-5cc6-47f6-b6a7-48dece436d8a)
  【【⚡开源⚡】天文主流软件与驱动一键下载工具】 https://www.bilibili.com/video/BV1CqPce3ELE/?share_source=copy_web&vd_source=42a7223a5d4114a74fe0f62161760376
