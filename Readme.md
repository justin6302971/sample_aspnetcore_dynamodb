# sample application for dotnetcore and dynamodb



## prerequisites
* aspnetcore
* aws dynamodb
* docker

## steps
1. run dynamodb in docker with following command and can check this repo for this part [dynamodb with docker](https://github.com/justin6302971/docker_dynamodb_local)
``` bash
# clone this repo and navigate to the directory
docker-compose up -d
```
2. download gui tool(nosql workbench) or using aws cli to check if it is running
3. create table with sample model, this sample using employee model for demostration


## references
1. [running aws dynamodb locally for .net core app](https://www.stevejgordon.co.uk/running-aws-dynamodb-locally-for-net-core-developers)
2. [aws doc for object persistence framework](https://aws.amazon.com/tw/articles/using-amazon-dynamodb-object-persistence-framework-an-introduction/)
3. [endpoint error validation response](https://kevsoft.net/2020/02/09/adding-errors-to-model-state-and-returning-bad-request-within-asp-net-core-3-1.html)
4. [error handling](https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api)
5. [AWS DynamoDB with .Net Core](https://www.linkedin.com/pulse/aws-dynamodb-net-core-senthil-kumaran)
6. [AWS DynamoDB with .Net Core - 1](https://www.rahulpnath.com/blog/aws-dynamodb-net-core/)
7. [sample dynamodb query](https://dynobase.dev/dynamodb-csharp-dotnet/)
8. [basic concept doc for dynamodb](https://www.dynamodbguide.com/secondary-indexes)