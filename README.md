
# envirosense-web


## Prerequisites

A PostgreSQL instance is need in order to run service. To set up for development, you can use built-in docker compose file:
 
```bash
docker compose up -d
```

### SQLFLUFF

`sqlfluff` is a SQL linter which has auto-fix capability. It is used to lint and fix our SQL scripts.

To install tool on `ubuntu`, run:
```bash
pipx install sqlfluff
```

or follow official [instruction](https://docs.sqlfluff.com/en/stable/gettingstarted.html).

## Using Makefile

Before committing changes, run following commands to check code quality(.SQL and .NET):
```
make validate
```

or run following command to automatically fix linter issues(if possible)
```
make format
```

## Bruno API Collection


This project includes a [Bruno](https://www.usebruno.com/) collection located in the `bruno/` directory. Bruno is an open-source API client that allows you to easily test and interact with the `Envirosense.API`.

You can download Bruno from their official website and import the collection from the `bruno` folder to start making requests to the API endpoints.

