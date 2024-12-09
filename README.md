# Home Challenge 
Henri Bolschakov

## How to run the project

### Follow steps below to run the project
```bash
#Starting the project
docker compose up -d

# Executing tests
cd test-app
npm install
npm run test
```


## Tests
Current solution contains simple set of tests for Calendar Api controller. 
It is using Testcontainer to run the tests in docker container.
So each will contain same set of data and real database. 
For test DB container was used same dockerfile as for main project.

### Follow steps below to run tests (in solution root folder).
```bash
dotnet test 
```