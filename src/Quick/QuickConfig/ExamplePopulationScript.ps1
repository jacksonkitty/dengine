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