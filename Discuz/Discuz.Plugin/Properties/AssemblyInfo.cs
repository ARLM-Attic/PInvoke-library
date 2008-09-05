﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Discuz.Common;

// 有关程序集的常规信息通过下列属性集
// 控制。更改这些属性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("Discuz!NT Plugin DLL")]
[assembly: AssemblyDescription("Discuz!NT 插件类库")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Comsenz Inc.")]
#if NET1
[assembly: AssemblyProduct("Discuz!NT 2.5 (.NET Framework 1.1)")]
#else
[assembly: AssemblyProduct("Discuz!NT 2.5 (.NET Framework 2.0/3.x)")]
#endif
[assembly: AssemblyCopyright("2008, Comsenz Inc.")]
[assembly: AssemblyTrademark("Discuz!NT")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 属性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("27095af0-151d-4a08-83d6-4ccb5c0d8d9b")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“修订号”和“内部版本号”的默认值，
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion(Utils.ASSEMBLY_VERSION)]
[assembly: AssemblyFileVersion(Utils.ASSEMBLY_VERSION)]
