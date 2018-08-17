# LuizaDotNetCore

 - To run this application you need a AWS account and a dynamodb table called Employee created
 - You also need a file with the following content:
 ```
[default]
aws_access_key_id=EXAMPLEACESSKEY
aws_secret_access_key=EXAMPLESECRETKEY
region=EXAMPLE_REGION
```

- save the file and replace the **ProfilesLocation** property in appsettings.json file:

```
...
  "AWS": {
    "Profile": "default",
    "ProfilesLocation": "C:\\users\\yourusername\\.aws\\credentials",
    "Region": "sa-east-1"
  },
...

```
