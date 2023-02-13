# payment SDK netcore

> Ksher will shut down the API connection via .vip.ksher.net. That make new register merchant will unable to use system over .vip.ksher.net.
>
> Merchants are currently connected, Please change the API to connection http://api.ksher.net.

Ksher SDK .NET Core

## Requirement

- .NET Core 2.1 or higher

- Ksher Payment API Account
  - Requesting sandbox account please contact support@ksher.com
- API_URL
  - Along with a sandbox accout, you will be receiving a API_URL in this format: s[UNIQUE_NAME].vip.ksher.net
- API_TOKEN
  - Log in into API_URL using given sandbox account and get the token. see [How to get API Token](https://doc.vip.ksher.net/docs/howto/api_token)

## Nuget Packages

- This Ksher Nuget Packages build base by .NET Core 2.1, request minimum referenced by [.NET implementation support](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

- Please see [Ksher](https://www.nuget.org/packages/Ksher/) Nuget Packages.

## How to test

- Install [Ksher Nuget Packages](https://www.nuget.org/packages/Ksher/) or clone [this project](https://github.com/ksher-solutions/payment_sdk_netcore)

  - If you use Visual Studio, please see [Install and use a package in Visual Studio](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) from Microsft.
  - If you install nuget by cmd, please check at [dotnet nuget add source](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-add-source) from Microsft.

  - or you can see full file inside "ksherpay" project by clone [this project](https://github.com/ksher-solutions/payment_sdk_netcore)

- Change configuration data to your sandbox

```csharp
static string base_url = @"https://sandboxdoc.vip.ksher.net";
static string token = "your token";
```

- run project
  - netcore command

  ```shell
  . dotnet run --project ksherpay
  ```

  - or run in Visual Studio.
