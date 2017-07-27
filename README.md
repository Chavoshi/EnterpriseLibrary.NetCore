# Enterprise Library for .Net Core 2.0

## Enterprise Library Overwiew

The Microsoft Enterprise Library is a collection of reusable software components (application blocks) designed to assist software developers with common enterprise development cross-cutting concerns (such as logging, validation, data access, exception handling, and many others). Application blocks are a type of guidance; they are provided as source code, test cases, and documentation that can be used "as is," extended, or modified by developers to use on complex, enterprise-level line-of-business development projects.

## Benefits of Enterprise Library

The design of application blocks encapsulates the Microsoft recommended and proven practices for .NET application development. These good practices are demonstrated in the overall design of the Enterprise Library, as well in the context-specific guidelines in the design of individual application blocks and QuickStarts. Software developers can add application blocks to .NET applications quickly and easily. For example, the Data Access Application Block provides access to the most frequently used features of ADO.NET, exposing them through easily used classes. In some cases, application blocks also add related functionality not directly supported by the underlying class libraries.

## Goals for Enterprise Library


* **Consistency:** All Enterprise Library application blocks feature consistent design patterns and implementation approaches.
* **Extensibility:** All application blocks include defined extensibility points that allow developers to customize the behavior of the application blocks by adding their own code.
* **Ease of use:** Enterprise Library offers numerous usability improvements, including a graphical configuration tool, a simpler installation procedure, and clearer and more complete documentation and samples.
* **Integration:** Enterprise Library application blocks are designed to work well together or individually.

## Ported Libraries and Application Blocks to .Net Core 2.0:

* [Common Infrastructure](https://www.nuget.org/packages/EnterpriseLibrary.Common.NetCore/)
* [Exception Handling Application Block](https://www.nuget.org/packages/EnterpriseLibrary.ExceptionHandling.NetCore/)
* [Logging Application Block](https://www.nuget.org/packages/EnterpriseLibrary.Logging.NetCore/)
* [Exception Handling Application Block Logging Handler](https://www.nuget.org/packages/EnterpriseLibrary.ExceptionHandling.Logging.NetCore/)
* [Validation Application Block](https://www.nuget.org/packages/EnterpriseLibrary.Validation.NetCore/)
* [Policy Injection Application Block](https://www.nuget.org/packages/EnterpriseLibrary.Logging.Database)
* [Data Access Application Block](https://www.nuget.org/packages/EnterpriseLibrary.Data.NetCore/)
* [Logging Application Block Database Provider](https://www.nuget.org/packages/EnterpriseLibrary.Logging.Database.NetCore/)
