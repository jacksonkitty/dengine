# QuickConfig

This is an early draft.
Quick notes for a quick config

## Instructions

(mostly to self)

### Container

 ```
docker build --tag quickconfig:v0_1 -f .\src\QuickConfig\Dockerfile .`
docker run --name containerName --network networkName -d --restart unless-stopped  -p 8410:4410 -p 8411:4411 quickconfig
 ```

### Populate

 ```
 $Examples = @{
     'Item1' = @{
         'App' = 'example'
         'Env' = 'example'
         'Key' = 'scripted'
         'Value' = 'From Powershell Script'
         'IsValid' = $true
     }
 }

$Body = $Examples.Item1 | ConvertTo-Json
$Params = @{
    Method = "Post"
    Uri = "https://localhost:8411/secrets"
    Body = $Body
    ContentType = "application/json"
}
Invoke-RestMethod @Params
```

