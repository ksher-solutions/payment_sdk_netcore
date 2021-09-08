# payment sdk netcore

ksher payment sdk dotnet core

## Requirement

- .netcore 3.1

- Ksher Payment API Account
  - Requesting sandbox account please contact support@ksher.com
- API_URL
  - Along with a sandbox accout, you will be receiving a API_URL in this format: s[UNIQUE_NAME].vip.ksher.net
- API_TOKEN
  - Log in into API_URL using given sandbox account and get the token. see (How to get API Token) [https://doc.vip.ksher.net/docs/howto/api_token]

## How to test

change configuration data to your sandbox

```csharp
static string base_url = @"https://sandboxdoc.vip.ksher.net";
static string token = "your token";
```

run command

```shell
dotnet run --project ksherpay
```

or run in visual studio

